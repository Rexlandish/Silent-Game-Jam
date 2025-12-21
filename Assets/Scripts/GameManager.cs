using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using static Gesture;

public class GameManager : MonoBehaviour
{

    [SerializedDictionary("Enum", "Sign Sprite")]
    public SerializedDictionary<GestureEnum, Sprite> signSprites = new(); // Set in inspector in a prefab

    [SerializedDictionary("Enum", "Player Guess")]
    public SerializedDictionary<GestureEnum, string> playerTranslations = new();

    public List<GestureEnum> newGestures = new();

    public bool UIBlockingInput = false;
    public Material handMaterial;

    public CinemachineCamera playerCamera;
        
    public static GameManager Instance;

    public int score;
    public int maxScore;

    // Move all the new words into playerTranslations with empty strings
    public void SeenNewWords()
    {
        foreach (GestureEnum gesture in newGestures)
        {
            if (!playerTranslations.ContainsKey(gesture))
            {
                playerTranslations[gesture] = "";
            }
            else
            {
                Debug.LogWarning("Gesture already exists in player translations, this shouldn't happen! Check if this is being filtered out by GameManager.CheckIfNewWord: " + gesture.ToString());
            }
        }

        newGestures.Clear();
    }

    

    // Is called by Character if any new words are performed
    public void CheckIfNewWord(GestureEnum gesture)
    {
        // If the player hasn't translated this gesture yet, and it's not already in newGestures, and it's not Rest
        // Add it to new gestures
        if (!playerTranslations.ContainsKey(gesture) && !newGestures.Contains(gesture) && gesture != GestureEnum.Rest)
        {
            newGestures.Add(gesture);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Don't do this!!
    // These add all the translations as blank,
    // they are meant to only be added if the player sees a new sign!
    void CompletePlayerTranslations()
    {
        foreach (GestureEnum gestureEnum in Enum.GetValues(typeof(GestureEnum)))
        {
            if (!playerTranslations.ContainsKey(gestureEnum))
            {
                playerTranslations[gestureEnum] = "";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
