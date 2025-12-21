    using System.Collections;
    using System.Collections.Generic;
    using Unity.Cinemachine;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class NPCHandler : MonoBehaviour
    {
        public bool PlayerInsideCollider = false;
        public GameObject Popup;
        public ParticleSystem PopupParticle;
        public BoxCollider InteractionRange;
        public AudioSource PingSFX;
        public GameObject DialogueBox;
        public GameObject characterCameraObject;
        public SpriteRenderer sprite;
        
        public GameObject Translations;

        public bool canInteract = false;
        public bool InBlindspot = false;

        public int characterID;

        // Start is called
        // before the first frame update
        void Start()
        {
            PopupParticle = Popup.GetComponent<ParticleSystem>();
            
        }

        void Update()
        {
            if (canInteract == true && Translations.gameObject.activeInHierarchy == false)
            {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // DialogueBox.SetActive(true);
                        Debug.Log("Interacted");

                        CharacterManager.Instance.StartDialogue(this);

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
