using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignIcon : MonoBehaviour
{

    public Image signIcon;
    public TMP_InputField inputField;

    Gesture.GestureEnum currentGesture;

    void UpdatePlayerTranslation(Gesture.GestureEnum gesture, string translation)
    {
        currentGesture = gesture;
        GameManager.Instance.playerTranslations[gesture] = translation;
    }



    public void SetSignIcon(Gesture.GestureEnum gestureEnum)
    {

        print(gestureEnum);
        signIcon.sprite = GameManager.Instance.signSprites[gestureEnum];

        inputField.text = GameManager.Instance.playerTranslations[gestureEnum];

        inputField.onValueChanged.RemoveAllListeners();
        inputField.onValueChanged.AddListener(value => UpdatePlayerTranslation(gestureEnum, value));
    }

}
