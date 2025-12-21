using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SignCanvas : MonoBehaviour, IPanel
{

    public static SignCanvas Instance;

    public GameObject signPrefab;
    public GameObject container;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            print("Pressed");
            ToggleTranslations();
        }
    }



    public bool isVisible = false;

    // QoL Functions

    // Handles switching state
    public void ToggleTranslations()
    {
        SetVisible(!isVisible);
    }

    public void SetVisible(bool vis)
    {
        isVisible = vis;
        if (isVisible)
        {
            OpenPanel();
        }
        else
        {
            ClosePanel();
        }
    }

    List<GameObject> spawnedSigns = new();
    public void OpenPanel()
    {
        GameManager.Instance.UIBlockingInput = true;
        container.SetActive(true);

        // Notify GameManager that new words have been seen
        GameManager.Instance.SeenNewWords();

        // Get the gestures that the player has seen from 'player translations'
        //List<Gesture.GestureEnum> knownGestures = GameManager.Instance.playerTranslations.Keys.ToList();
        List<Gesture.GestureEnum> knownGestures = new();

        // Add new gestures player hasn't seen
        knownGestures.AddRange(GameManager.Instance.newGestures); // All gestures

        // Add gestures already seen
        knownGestures.AddRange(GameManager.Instance.playerTranslations.Keys.ToList());

        // Create icons
        
        foreach (var gesture in knownGestures)
        {

            var spawnedSignPrefab = Instantiate(signPrefab, container.transform).GetComponent<SignIcon>();

            spawnedSigns.Add(spawnedSignPrefab.gameObject);
            var sprite = GameManager.Instance.signSprites[gesture];

            spawnedSignPrefab.SetSignIcon(gesture);

            // Create a new translation entry if the sign you're displaying doesn't exist
            if (!GameManager.Instance.playerTranslations.ContainsKey(gesture))
            {
                GameManager.Instance.playerTranslations.Add(gesture, "");
            }

        }


    }

    public void ClosePanel()
    {
        // Clear list view
        container.SetActive(false);
        for (int i = 0; i < spawnedSigns.Count; i++)
        {
            Destroy(spawnedSigns[i]);
        }
        spawnedSigns.Clear();

        GameManager.Instance.UIBlockingInput = false;
    }
}
