using UnityEngine;

public class BlindspotCorrect : MonoBehaviour
{
    public int Blindspot = 0;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TopDownCamera.Instance.InBlindspot = true;
            TopDownCamera.Instance.BlindSpotValue = Blindspot;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TopDownCamera.Instance.InBlindspot = false;
            TopDownCamera.Instance.BlindSpotValue = 0;
        }
    }
}
