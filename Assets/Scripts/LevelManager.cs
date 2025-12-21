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

    List<GameObject> spawnedNPCs = new();

    

    public void LoadLevel(int index, bool fade = true)
    {

        


        // Fade out
        // Destroy NPCs, disable control

        for (int i = 0; i < spawnedNPCs.Count; i++)
        {
            Destroy(spawnedNPCs[i]);
        }
        spawnedNPCs.Clear();

        // Create new NPCs
        // Fade in

        // Fade only if fade = true

        var level = levels[currentLevelIndex];

        // Set all player guesses in gameobject to None
        for (int i = 0; i < level.characters.Length; i++)
        {
            level.characters[i].playerGuessDestination = Level.Destination.None;
        }


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
    }

    public void CheckIfPlayerCanLockInJudgements()
    {
        if (CurrentLevel.characters.All(character => character.playerGuessDestination != Level.Destination.None))
        {
            lockInJudgementsButton.SetActive(true);
        }
    }

    public void LockInJudgements()
    {
        int score = 0;

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


        if (score >= 4)
        {
            LoadLevel(1, true);
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
            LoadLevel(currentLevelIndex, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
