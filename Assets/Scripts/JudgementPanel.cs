using TMPro;
using UnityEngine;

public class JudgementPanel : MonoBehaviour
{
    public static JudgementPanel Instance;
    public TextMeshProUGUI judgementText;


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

    public void SetJudgementText(int judgement)
    {
        SetJudgementText((Level.Destination)judgement);
    }

    public void SetJudgementText(Level.Destination destination)
    {

        CharacterManager.Instance.SetGuessForCurrentPlayer(destination);
        string destinationText;

        switch (destination)
        {
            case Level.Destination.Heaven:
                destinationText = $"<color=#00ffff>HEAVEN</color>";
                break;
            case Level.Destination.Hell:
                destinationText = $"<color=red>HELL</color>";
                break;
            default:
                destinationText = "???";
                break;
        }

        judgementText.text = $"Current Judgment:<br><size=30><b>{destinationText}</b></size>";
    }
}
