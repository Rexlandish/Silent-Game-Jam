using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
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

    // Performing gestures
    bool isPerforming = false;
    string currentQuestion;
    int currentIndex = 0; // Index of current gesture that is being performed
    
    public void Load(CharacterObject CO)
    {
        // Don't reach through handSignManager to set hand positions,
        // add a function to HandSignManager later ideally
        handSignManager.SetHandColours(CO);

        handSignManager.hand1.handPositions = CO.handSprites;
        handSignManager.hand2.handPositions = CO.handSprites;

        responses = CO.responses;

        characterSprite.sprite = CO.body;
        characterSprite.color = CO.bodyColor;

        handSignManager.hand1.exteriorColor = CO.exteriorColor;

        // Spawn buttons for each response
        foreach (var key in responses.Keys)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer.transform);
            Button button = newButton.GetComponent<Button>();
            newButton.SetActive(true);
            button.GetComponentInChildren<TextMeshProUGUI>().text = key;
            button.onClick.AddListener(() =>
            {
                StartPerformingGesture(key);
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


    void StartPerformingGesture(string question)
    {
        // hide button container
        if (isPerforming) return;
        currentQuestion = question;
        buttonContainer.SetActive(false);
        isPerforming = true;
        DoNextGestureHSM();
    }

    void PerformNextGesture()
    {
        // Go to next response in loop
        handSignManager.DoHandSign(responses[currentQuestion][currentIndex]);
        currentIndex += 1;

    }

    void StopPerformingGesture()
    {
        // show button container
        handSignManager.DoHandSign(Gesture.GestureEnum.Rest);
        currentIndex = 0;
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
            StopPerformingGesture();
        }
    }

    void CheckForGestureAdvance()
    {
        if (GameManager.Instance.UIBlockingInput) return;

        if (
            isPerforming &&
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
