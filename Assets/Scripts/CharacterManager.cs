using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CharacterManager : MonoBehaviour
{

    public CharacterObject characterObject;
    public bool loadCharacterObjectOnStart;
    [Space]

    public SpriteRenderer characterSprite;
    public HandSignManager handSignManager;
    [Space]
    public SerializedDictionary<string, Gesture.GestureEnum[]> responses = new();
    [Space]
    public GameObject buttonContainer; // Gameobject that buttons will be placed under
    public GameObject buttonPrefab; // Prefab for buttons
    [Space]
    public TextMeshProUGUI judgementText;
    [Space]
    public GameObject characterInterviewObject;
    public GameObject backButton;

    // Performing gestures
    bool isPerforming = false;
    string currentQuestion;
    int currentIndex = 0; // Index of current gesture that is being performed

    public static CharacterManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    NPCHandler focusedNPC;
    bool hasStartedDialogue = false;
    float cinemachineEaseDelay = 0.5f;
    public void StartDialogue(NPCHandler npc)
    {
        if (hasStartedDialogue) return;
        hasStartedDialogue = true;

        // Get currentCharacterData from NPC and LevelManager
        var currentLevel = LevelManager.Instance.levels[LevelManager.Instance.currentLevel];
        var currentCharacterData = currentLevel.characters[npc.characterID];
        var currentCharacter = currentCharacterData.character;

        focusedNPC = npc;

        // vvv
        npc.characterCameraObject.SetActive(true);

        // Start the local coroutine which waits, then does the camera/sprite/Load/position work.
        StartCoroutine(WaitForCinemachineEase());


        IEnumerator WaitForCinemachineEase()
        {
            yield return new WaitForSeconds(cinemachineEaseDelay);

            characterInterviewObject.SetActive(true);
            transform.position = currentCharacterData.spawnPosition + Vector3.up * 1.5f;

            npc.sprite.enabled = false;

            Load(currentCharacter);
            UpdateJudgementPanel();
        }

    }

    public void UpdateJudgementPanel()
    {
        // Surely this can be stored in a variable somewhere
        JudgementPanel.Instance.SetJudgementText(
            LevelManager.Instance.levels[LevelManager.Instance.currentLevel]
            .characters[focusedNPC.characterID]
            .playerGuessDestination
        );
    }

    public void SetGuessForCurrentPlayer(Level.Destination destination)
    {

        var lm = LevelManager.Instance;
        int currentLevelIndex = lm.currentLevel;
        var currentLevel = lm.levels[currentLevelIndex];
        var characterEntry = currentLevel.characters[focusedNPC.characterID];

        characterEntry.playerGuessDestination = destination;

    }

    public void StopDialogue()
    {
        characterInterviewObject.SetActive(false);
        focusedNPC.characterCameraObject.SetActive(false);
        hasStartedDialogue = false;
        focusedNPC.sprite.enabled = true;

        characterSprite.sprite = null;
        characterSprite.color = Color.white;
    }

    public void SetCurrentCharacterGuess(Level.Destination destination)
    {
        print($"Set to {destination}");
        throw new NotImplementedException();
        // GameManager.Instance.SetCurrentCharacterGuess(destination)

        // Set this in the LevelManager
    }

    public void SetCurrentCharacterGuess(int destination)
    {
        SetCurrentCharacterGuess((Level.Destination) destination);
    }

    // ADD A WAY TO HANDLE HSV COLOURS BETTER, REWRITE!!!!!
    void RandomiseColours(CharacterObject CO)
    {
        // exterior
        // palm
        // claw
        // body

        Vector3 exteriorHSV = new(
            Random.Range(0f, 1f),
            Random.Range(0.75f, 1f),
            Random.Range(0.75f, 1f)
        );

        Vector3 palmHSV = new(
            (exteriorHSV.x + Random.Range(-0.1f, 0.1f)) % 1, // rotate hue slightly
            Random.Range(0.25f, 0.5f),
            Random.Range(0.75f, 1f)
        );

        CO.exteriorColor = Color.HSVToRGB(exteriorHSV.x, exteriorHSV.y, exteriorHSV.z);
        CO.palmColor = Color.HSVToRGB(palmHSV.x, palmHSV.y, palmHSV.z);

        CO.bodyColor = CO.exteriorColor;
    }

    List<GameObject> spawnedButtons = new();
    public void Load(CharacterObject CO)
    {
        /*
        if (CO.randomiseColoursOnCreate)
        {
            RandomiseColours(CO);
        }
        */

        // Don't reach through handSignManager to set hand positions,
        // add a function to HandSignManager later ideally
        handSignManager.SetHandColours(CO);

        handSignManager.hand1.handPositions = CO.handSprites;
        handSignManager.hand2.handPositions = CO.handSprites;

        responses = CO.responses;

        characterSprite.sprite = CO.body;
        characterSprite.color = CO.bodyColor;

        handSignManager.hand1.exteriorColor = CO.exteriorColor;

        // Delete existing buttons
        
        foreach (GameObject button in spawnedButtons)
        {
            Destroy(button);
        }

        // Spawn buttons for each response
        foreach (var key in responses.Keys)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer.transform);
            spawnedButtons.Add(newButton);
            Button button = newButton.GetComponent<Button>();
            newButton.SetActive(true);
            button.GetComponentInChildren<TextMeshProUGUI>().text = key;
            button.onClick.AddListener(() =>
            {
                StartCoroutine(StartPerformingGesture(key));
            });

        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (loadCharacterObjectOnStart)
        {
            Load(characterObject);
        }
    }

    // The stored response for a given question


    IEnumerator StartPerformingGesture(string question)
    {
        // hide button container
        if (isPerforming) yield break;
        currentQuestion = question;
        buttonContainer.SetActive(false);
        isPerforming = true;

        backButton.SetActive(false);

        // Wait for a delay
        yield return new WaitForSeconds(0.2f);
        DoNextGestureHSM();
    }

    void PerformNextGesture()
    {
        // Go to next response in loop
        handSignManager.DoHandSign(responses[currentQuestion][currentIndex]);
        currentIndex += 1;

    }

    IEnumerator StopPerformingGesture()
    {
        // show button container
        handSignManager.DoHandSign(Gesture.GestureEnum.Rest);
        currentIndex = 0;
        yield return new WaitForSeconds(0.2f);

        backButton.SetActive(true);
        buttonContainer.SetActive(true);
        isPerforming = false;

    }

    void DoNextGestureHSM() // HSM: Hand Sign Manager
    {
        if (currentIndex < responses[currentQuestion].Length)
        {
            PerformNextGesture();
        }
        else
        {
            StartCoroutine(StopPerformingGesture());
        }
    }

    void CheckForGestureAdvance()
    {
        if (GameManager.Instance.UIBlockingInput) return;

        if (
            isPerforming &&
            !EventSystem.current.IsPointerOverGameObject() &&
            (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            )
        {
            DoNextGestureHSM();
        }
    }

    // Update is called once per frame

    void Update()
    {
        CheckForGestureAdvance();
    }
}
