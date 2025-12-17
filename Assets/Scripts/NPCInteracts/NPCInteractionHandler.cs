using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionHandler : MonoBehaviour
{
    public NPCHandler Handle;
    public Camera cam;
    public float FovSpeed = 4f;
    public float normalFOV = 40f;
    public float targetFOV = 35f;
    public bool playerexit = false;
    public bool playerinside = false;

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
            while (cam.fieldOfView > targetFOV + 0.1f)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * FovSpeed);
                yield return null;
            }

            cam.fieldOfView = targetFOV; // snap cleanly
            playerinside = false;
        }
    }
    
    private System.Collections.IEnumerator FOVEffectExit()
    {
        if (playerexit)
        {
            while (cam.fieldOfView < normalFOV - 0.1f)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, Time.deltaTime * FovSpeed);
                yield return null;
            }

            cam.fieldOfView = normalFOV; // snap cleanly
            playerexit = false;
            StopAllCoroutines();
        }
    }
}
