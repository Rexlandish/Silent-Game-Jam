using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RawImage RedText;
    public RawImage BlackText;

    void Start()
    {
        RedText.enabled = false;
        BlackText.enabled = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BlackText.enabled = false;
        RedText.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BlackText.enabled = true;
        RedText.enabled = false;
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
}