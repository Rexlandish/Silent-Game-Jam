    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Unity.Cinemachine;

    public class NPCInteractionHandler : MonoBehaviour
    {
        public NPCHandler Handle;
        public float FovSpeed = 4f;
        public float normalFOV = 40f;
        public float targetFOV = 35f;
        public bool playerexit = false;
        public bool playerinside = false;

        public CinemachineCamera cam;

        private void Start()
        {
            cam = GameObject.Find("PlayerCamera").GetComponent<CinemachineCamera>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            { 
                playerinside = true;
                StartCoroutine(FOVEffectEnter());
                Handle.PlayerWithinRange();
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerexit = true;
                Handle.PlayerUnreachable();
            }
        }

        void Update()
        {
            if (playerexit)
            {
                StopCoroutine(FOVEffectEnter());
                StartCoroutine(FOVEffectExit());
            }
        }
        
        
        
        private System.Collections.IEnumerator FOVEffectEnter()
        {
            if (playerinside)
            {
                // Step 1: Increase quickly to dashFOV
                while (cam.Lens.FieldOfView > targetFOV + 0.1f)
                {
                    cam.Lens.FieldOfView = Mathf.Lerp(cam.Lens.FieldOfView, targetFOV, Time.deltaTime * FovSpeed);
                    yield return null;
                }

                cam.Lens.FieldOfView = targetFOV; // snap cleanly
                playerinside = false;
            }
        }
        
        private System.Collections.IEnumerator FOVEffectExit()
        {
            if (playerexit)
            {
                while (cam.Lens.FieldOfView < normalFOV - 0.1f)
                {
                    cam.Lens.FieldOfView = Mathf.Lerp(cam.Lens.FieldOfView, normalFOV, Time.deltaTime * FovSpeed);
                    yield return null;
                }

                cam.Lens.FieldOfView = normalFOV; // snap cleanly
                playerexit = false;
                StopAllCoroutines();
            }
        }
    }
