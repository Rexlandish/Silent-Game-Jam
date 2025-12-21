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

        public bool Interacting = false;

        public bool canInteract = false;
        public bool InBlindspot = false;
        public Image TranslationsImage;

        public int characterID;
        
        public static NPCHandler instance;

        // Start is called
        // before the first frame update
        void Start()
        {
            PopupParticle = Popup.GetComponent<ParticleSystem>();
            //TranslationsImage.enabled = false;
            
        }

        void Update()
        {
            if (canInteract == true && Interacting == false)
            {
               if (PanelTabManager.Instance.IsActive() == false)
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // DialogueBox.SetActive(true);
                        Debug.Log("Interacted");
                        Interacting = true;
                        Popup.active = false;
                        //TranslationsImage.enabled = true;
                        

                        CharacterManager.Instance.StartDialogue(this);
                        SpriteMovement.Instance.CanWalk = false;

                    }
                
            }
            
            
        }

        public void PlayerWithinRange()
        {
            PlayerInsideCollider = true;
            Debug.Log("Within Interaction Range");
            Popup.active = true;
            canInteract = true;
            //Instantiate(PingSFX, transform.position, Quaternion.identity);
        }
        
        

        public void PlayerUnreachable()
        {
            PlayerInsideCollider = false;
            Debug.Log("N/A");
            Popup.active = false;
            //PopupParticle.Clear();
            //PopupParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            canInteract = false;
        }
    }
