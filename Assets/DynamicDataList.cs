
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class DynamicDataList : MonoBehaviour
{

    [SerializeField]
    private UIDocument _uiDocument;

    [SerializeField]
    private VisualTreeAsset _uiDocumentItem;

    private VisualElement image;
    private TextField textField;

    public void SetData(Sprite s)
    {
        Debug.Log(s);
        image.style.backgroundImage = new StyleBackground(s);
        textField.RegisterCallback<ChangeEvent<string>>((evt) =>
        {
            Debug.Log($"User translation changed: {evt.newValue}");
        });

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            print("Pressed");
            ToggleTranslations();
        }
    }

    bool isVisible = false;

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
            Show();
        }
        else
        {
            Hide();
        }
    }


    private void Show()
    {
        GameManager.Instance.UIBlockingInput = true;
        // Notify GameManager that new words have been seen
        GameManager.Instance.SeenNewWords();

        _uiDocument.enabled = true;

        // Get the gestures that the player has seen from 'player translations'
        //List<Gesture.GestureEnum> knownGestures = GameManager.Instance.playerTranslations.Keys.ToList();
        List<Gesture.GestureEnum> knownGestures = new();
        
        // Add new gestures player hasn't seen
        knownGestures.AddRange(GameManager.Instance.newGestures); // All gestures

        // Add gestures already seen
        knownGestures.AddRange(GameManager.Instance.playerTranslations.Keys.ToList());

        // Clear list view
        var listView = _uiDocument.rootVisualElement.Q("List") as ScrollView;
        listView.Clear();

        // Then get their corresponding sprites from the GameManager
        foreach (var gesture in knownGestures)
        {
            var item = _uiDocumentItem.Instantiate();

            item.style.width = 250;
            item.style.height = 400;

            image = item.Q("Image") as VisualElement;
            image.style.width = 200;
            image.style.height = image.style.width;

            textField = item.Q("UserTranslation") as TextField;
            var textFieldText = textField.Q("unity-text-input");
            
            textFieldText.style.marginBottom = 0;
            textFieldText.style.marginLeft = 0;
            textFieldText.style.marginRight = 0;
            textFieldText.style.marginTop = 0;


            textField.style.width = 200;
            textField.style.marginTop = 10;

            var sprite = GameManager.Instance.signSprites[gesture];

            image.style.backgroundImage = new StyleBackground(sprite);

            // Create a new translation entry if the sign you're displaying doesn't exist
            if (!GameManager.Instance.playerTranslations.ContainsKey(gesture))
            {
                GameManager.Instance.playerTranslations.Add(gesture, "");
            }

            textField.value = GameManager.Instance.playerTranslations[gesture];
            textField.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                Debug.Log($"User translation changed: {evt.newValue}");
                GameManager.Instance.playerTranslations[gesture] = evt.newValue;
            });

            listView.Add(item);
        }

        /*
        listView.makeItem = () =>
        {
            var listItem = _uiDocumentItem.Instantiate();

            var data = new DynamicDataListItem(listItem);

            listItem.userData = data;

            return listItem;
        };

        listView.fixedItemHeight = 300;

        listView.bindItem = (item, index) =>
        {
            (item.userData as DynamicDataListItem)?.SetData(
                signSprites[index]
            );
        };

        listView.itemsSource = signSprites;
        */
    }

    private void Hide()
    {
        // Clear list view
        var listView = _uiDocument.rootVisualElement.Q("List") as ScrollView;
        listView.Clear();

        GameManager.Instance.UIBlockingInput = false;
        _uiDocument.enabled = false;
    }

}
