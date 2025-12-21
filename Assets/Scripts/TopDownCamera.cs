using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    //Variables
    public Transform player;
    public GameObject PlayerObj;
	
    public float smooth = 0.3f;
    public float xoffset;
    public SpriteMovement TopDownMovementHandle;
    public float zoffset;

    public float height;
    public bool InBlindspot = false;
    public int BlindSpotValue = 0;

    private Vector3 velocity = Vector3.zero;

    Vector3 targetPosition;
    CinemachineCamera CC;
    
    public static TopDownCamera Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        CC = GetComponent<CinemachineCamera>();
        targetPosition = transform.position;

        transform.LookAt(player);
    }

    //Methods
    void Update()
    {
        /*
        if (TopDownMovementHandle.IsMovingLeft == true)
        {
            xoffset = -2f;
            zoffset = 10f;
        }
		
        if (TopDownMovementHandle.IsMovingRight == true)
        {
            xoffset = 2f;
            zoffset = 10f;
        }
        
        if (TopDownMovementHandle.IsMovingUp == true)
        {
            zoffset = 8f;
            xoffset = 0f;
        }
        
        if (TopDownMovementHandle.IsMovingDown == true)
        {
            zoffset = 12f;
            xoffset = 0f;
        }
        */

        if (InBlindspot == true)
        {
            BlindspotFix();
        }
        else if (InBlindspot == false)
        {
            zoffset = 17f;
            height = 5f;
        }

        Vector3 pos = new Vector3();
        pos.x = player.position.x + xoffset;
        pos.z = player.position.z - zoffset;
        pos.y = player.position.y + height;

        //targetPosition = Vector3.SmoothDamp(targetPosition, pos, ref velocity, smooth);


        targetPosition = Vector3.SmoothDamp(targetPosition, pos, ref velocity, smooth);

        transform.LookAt(player);
        CC.ForceCameraPosition(targetPosition, transform.rotation);


    }

    void BlindspotFix()
    {

        switch (BlindSpotValue)
        {
            case 0:
                zoffset = 17f;
                height = 5f;
                break;
            case 1:
                zoffset = 12f;
                height = 10f;
                break;
            case 2:
                zoffset = 15f;
                height = 17f;
                break;
        }
        
    }

}