using AYellowpaper.SerializedCollections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelTabManager : MonoBehaviour
{

    public SerializedDictionary<Tabs, bool> panelStatus;
    public SerializedDictionary<Tabs, GameObject> panels;

    //public bool IsActive { get {  return panelStatus.Values.Any(status => status); } }

    public bool IsActive()
    {
        return panelStatus[Tabs.Rules] || panelStatus[Tabs.Signs];
    }


    public enum Tabs
    {
        Rules,
        Signs
    }

    private void Start()
    {
        foreach (var panel in panelStatus.Keys)
        {
            panelStatus[panel] = false;
        }
        UpdatePanels();
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
        for (int i = 0; i <= panels.Count; i++)
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
