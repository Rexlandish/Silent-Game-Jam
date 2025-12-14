using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float breatheRate = 2f;
    public float breatheAmplitude = 0.1f;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one + new Vector3(0f, 1f, 0f) * (Mathf.Sin(Time.time * breatheRate) * breatheAmplitude);
    }
}