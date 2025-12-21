using AYellowpaper.SerializedCollections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelTabManager : MonoBehaviour
{

    public SerializedDictionary<Tabs, bool> panelStatus;
    public SerializedDictionary<Tabs, GameObject> panels;

    public static PanelTabManager Instance;

    //public bool IsActive { get {  return panelStatus.Values.Any(status => status); } }

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsActive()
    {
        return panelStatus[Tabs.Rules] || panelStatus[Tabs.Signs];
    }

    public void CloseAll()
    {
        foreach (var tab in panels)
        {
            panelStatus[tab.Key] = false;
        }
        UpdatePanels();
    }

    public enum Tabs
    {
        Rules,
        Signs
    }

    private void Start()
    {
        //CloseAll();
    }

    // For unity button
    public void Toggle(int i)
    {
        Toggle((Tabs)i);
    }

    // Really, Panel status can just be an enum instead of a dictionary of bools
    // since only one is active at a time
    public void Toggle(Tabs tab)
    {
        panelStatus[tab] = !panelStatus[tab];
        UpdatePanels();
    }

    void UpdatePanels()
    {
        // Disable all PanelStatus
        foreach (var panel in panelStatus.Keys)
        {
            panels[panel].GetComponentInChildren<IPanel>().ClosePanel();
        }

        // Activate the selected ones
        for (int i = 0; i < panels.Count; i++)
        {
            var panel = panels.Keys.ToList()[i];

            if (panelStatus[panel])
            {
                panels[panel].GetComponentInChildren<IPanel>().OpenPanel();
            }
            else
            {
                panels[panel].GetComponentInChildren<IPanel>().ClosePanel();
            }
        }
    }
}
