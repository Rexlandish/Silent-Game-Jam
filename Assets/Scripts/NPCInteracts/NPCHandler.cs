using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCHandler : MonoBehaviour
{
    public bool PlayerInsideCollider = false;
    public GameObject Popup;
    public ParticleSystem PopupParticle;
    public BoxCollider InteractionRange;
    public AudioSource PingSFX;
    public GameObject DialogueBox;
    
    public bool canInteract = false;

    // Start is called
    // before the first frame update
    void Start()
    {
        PopupParticle = Popup.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (canInteract == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                //DialogueBox.SetActive(true);
                Debug.Log("Interacted");

            }
        }
    }

    public void PlayerWithinRange()
    {
        PlayerInsideCollider = true;
        Debug.Log("Within Interaction Range");
        //PopupParticle.Play();
        canInteract = true;
        //Instantiate(PingSFX, transform.position, Quaternion.identity);
    }

    public void PlayerUnreachable()
    {
        PlayerInsideCollider = false;
        Debug.Log("N/A");
        //PopupParticle.Clear();
        //PopupParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        canInteract = false;
    }
}
