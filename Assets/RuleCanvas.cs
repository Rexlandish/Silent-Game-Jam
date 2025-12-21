using System.Linq;
using TMPro;
using UnityEngine;

public class RuleCanvas : MonoBehaviour, IPanel
{

    public GameObject ruleCanvas;
    public TextMeshProUGUI ruleText;

    void Start()
    {

    }

    public void OpenPanel()
    {
        string[] rules = LevelManager.Instance.CurrentLevel.rules;
        ruleText.text = string.Join("<br/>", rules.Select((rule, index) => $"{index + 1}. {rule}"));

        print("Open");
        ruleCanvas.SetActive(true);
    }

    public void ClosePanel()
    {
        print("Closed");
        ruleCanvas.SetActive(false);
    }


}
