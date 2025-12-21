using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{



    public Level[] levels;
    public int currentLevelIndex;

    public bool loadLevelOnStart = true; // Will Load currentLevel

    public static LevelManager Instance;
    public GameObject TranslationsUI;
    public GameObject NPCPrefab;

    public Level CurrentLevel { get { return levels[currentLevelIndex]; } }
    public GameObject lockInJudgementsButton;

    [Space]

    public CanvasGroup fadeCanvas;

    List<GameObject> spawnedNPCs = new();

    public GameObject resultsScreen;


    bool continueLoad = false;
    public IEnumerator LoadLevel(int index = -1, bool fade = true)
    {

        // Initiate variable to control the mid fade pause
        continueLoad = false;

        print("LOADING LEVEL!!!!");

        // Fade out
        // Destroy NPCs, disable control

        if (fade)
        {
            for (float i = 0; i < 1; i += Time.deltaTime)
            {
                fadeCanvas.alpha = i;
                yield return null;
            }
            fadeCanvas.alpha = 1;
        }


        for (int i = 0; i < spawnedNPCs.Count; i++)
        {
            Destroy(spawnedNPCs[i]);
            Debug.Log(spawnedNPCs[i], spawnedNPCs[i]);
        }
        spawnedNPCs.Clear();

        // Create new NPCs
        
        // If no level given, use the currentLevelIndex
        // Otherwise update currentLevelIndex with index
        if (index != -1)
        {
            currentLevelIndex = index;
        }

        var level = levels[index];

        print("understood");

        // Set all player guesses in gameobject to None
        for (int i = 0; i < level.characters.Length; i++)
        {
            level.characters[i].playerGuessDestination = Level.Destination.None;
        }

        print("reset");
        for (int i = 0; i < level.characters.Length; i++)
        {

            // Spawn the characters in the right positions,
            // and assign their character ID
            var npcData = level.characters[i];
            var npc = Instantiate(NPCPrefab, npcData.spawnPosition, Quaternion.identity);
            var npcHandler = npc.GetComponent<NPCHandler>();
            npcHandler.characterID = i;

            npcHandler.Translations = GameObject.FindWithTag("TranslationsID");
            
            spawnedNPCs.Add(npc);
        }
        
        AllSpawnedNPCs();
        print("spawned");
        // Wait for continue
        
        
        CheckIfPlayerCanLockInJudgements();

        resultsScreen.SetActive(true);
        print("okay");
        if (fade)
        {
            yield return new WaitUntil(() => continueLoad);
        }
        resultsScreen.SetActive(false);

        print("fading");
        // fade in
        if (fade)
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                fadeCanvas.alpha = 1 - i;
                yield return null;
            }
            fadeCanvas.alpha = 0;
        }


    }

    public void ContinueLevel()
    {
        continueLoad = true;
    }

    public void CheckIfPlayerCanLockInJudgements()
    {
        if (CurrentLevel.characters.All(character => character.playerGuessDestination != Level.Destination.None))
        {
            lockInJudgementsButton.SetActive(true);
        }
        else
        {
            lockInJudgementsButton.SetActive(false);
        }
    }

    public void LockInJudgements()
    {
        int score = 0;

        int maxScore = CurrentLevel.characters.Length;

        for (int i = 0; i < CurrentLevel.characters.Length; i++)
        {
            var currentChar = CurrentLevel.characters[i];
            if (currentChar.destination == currentChar.playerGuessDestination)
            {
                print($"{i} correct!");
                score += 1;
            }
            else
            {
                print($"{i} WRONG!");
            }
        }

        GameManager.Instance.score = score;
        GameManager.Instance.maxScore = maxScore;

        print(score);

        if (score >= maxScore)
        {
            // Load next level
            StartCoroutine(LoadLevel(currentLevelIndex + 1, true));
        }
        else
        {
            StartCoroutine(LoadLevel(currentLevelIndex, true));
        }
    }

    void AllSpawnedNPCs()
    {
        PanelTabManager.Instance.CloseAll();
    }
    
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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockInJudgementsButton.SetActive(false);

        if (loadLevelOnStart)
        {
            StartCoroutine(LoadLevel(currentLevelIndex, false));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
