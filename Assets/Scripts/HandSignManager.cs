using AYellowpaper.SerializedCollections;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Gesture;
using static Util;
using Random = UnityEngine.Random;
public class HandSignManager : MonoBehaviour
{
    public HandManager hand1;
    public HandManager hand2;

    Gesture[] gestureList;

    public TextMeshProUGUI translationText;

    public string defaultTranslation = "?";

    GestureEnum currentGesture;
    bool isMouseOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        translationText.enabled = isMouseOver && currentGesture != GestureEnum.Rest;
    }


    private void OnMouseEnter() // Accessed by character
    {
        isMouseOver = true;
    }

    public void UpdateUserTranslationText()
    {

        // Put this in it's own class later to allow for fancy visuals etc.

        // Don't show the translation if the enum is REST

        if (currentGesture == GestureEnum.Rest)
        {
            return;
        }


        // If a player translation doesn't exist in the gamemanager translations dictionary, use the default translation
        string translation;
        if (GameManager.Instance.playerTranslations.ContainsKey(currentGesture))
        {
            translation = GameManager.Instance.playerTranslations[currentGesture];
        }
        else
        {
            translation = "";
        }

        if (translation == "")
        {
            translation = defaultTranslation;
        }

        translationText.text = $" \"{translation}\" ";
        translationText.alpha = 1;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }

    public void HideUserTranslation()
    {
        // Put this in it's own class later to allow for fancy visuals etc.
        translationText.alpha = 0;
    }

    int gestureIndex = 0;
    public void DoNextGesture()
    {
        DoHandSign(gestureList[gestureIndex]);
        gestureIndex = (gestureIndex + 1) % gestureList.Length;
    }

    // Never call this directly! It loses GestureEnum data
    void DoHandSign(Gesture gesture)
    {
        hand1.Perform(gesture.handSigns[0]);
        hand2.Perform(gesture.handSigns[1]);
    }

    public void DoHandSign(GestureEnum gestureEnum)
    {
        // Call gamemanager's CheckIfNewWord to see if the character is performing a new word
        GameManager.Instance.CheckIfNewWord(gestureEnum);


        currentGesture = gestureEnum;
        Gesture gesture = GestureLibrary.gestureLibrary[gestureEnum];
        DoHandSign(gesture);

        UpdateUserTranslationText(); // This is a little inefficient as it reads from a variable in memory instead of passing it directly, but it's fine for now
    }
}
