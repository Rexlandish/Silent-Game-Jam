using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        TopDownMovementHandle = PlayerObj.GetComponent<SpriteMovement>();
    }

    //Methods
    void Update()
    {

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
        

        Vector3 pos = new Vector3();
        pos.x = player.position.x + xoffset;
        pos.z = player.position.z - zoffset;
        pos.y = player.position.y + height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
    }

}