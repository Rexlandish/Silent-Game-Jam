using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{



    public Level[] levels;
    public int currentLevel;

    public bool loadLevelOnStart = true; // Will Load currentLevel

    public static LevelManager Instance;
    public GameObject NPCPrefab;

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

        var level = levels[currentLevel];

        // Set all player guesses in gameobject to None
        for (int i = 0; i < level.characters.Length; i++)
        {
            level.characters[i].playerGuessDestination = Level.Destination.None;
        }


        for (int i = 0; i < level.characters.Length; i++)
        {

            // Spawn the characters in the right positions,
            // and assign their character IDs

            var npcData = level.characters[i];
            var npc = Instantiate(NPCPrefab, npcData.spawnPosition, Quaternion.identity);
            var npcHandler = npc.GetComponent<NPCHandler>();
            npcHandler.characterID = i;

            spawnedNPCs.Add(npc);
        }
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
        if (loadLevelOnStart)
        {
            LoadLevel(currentLevel, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
