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

    private Vector3 velocity = Vector3.zero;

    Vector3 targetPosition;
    CinemachineCamera CC;

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
        

        Vector3 pos = new Vector3();
        pos.x = player.position.x + xoffset;
        pos.z = player.position.z - zoffset;
        pos.y = player.position.y + height;

        //targetPosition = Vector3.SmoothDamp(targetPosition, pos, ref velocity, smooth);


        targetPosition = pos;

        transform.LookAt(player);
        CC.ForceCameraPosition(targetPosition, transform.rotation);


    }

}