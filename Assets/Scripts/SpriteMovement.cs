using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpriteMovement : MonoBehaviour
{
    public float movementSpeed;
    public float slowDownAmount = 1;

    Vector3 startPosition;
    float initialDuration;
    
    public SpriteRenderer spriteRenderer;
    public List<Sprite> nSprites;
    public List<Sprite> neSprites;
    public List<Sprite> eSprites;
    public List<Sprite> seSprites;
    public List<Sprite> sSprites;
    
    public float idleTime;
    public float frameRate;
    public bool IsHoldingLeft = false;
    public bool IsHoldingRight = false;
    
    public bool IsMovingLeft = false;
    public bool IsMovingRight = false;
    public bool IsMovingUp = false;
    public bool IsMovingDown = false;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //player movement
        float VerticalAxis = Input.GetAxis("Vertical");
        float HorizontallAxis = Input.GetAxis("Horizontal");

    }

    void HandleSpriteFlip()
    {
        float VerticalAxis = Input.GetAxis("Vertical");
        float HorizontalAxis = Input.GetAxis("Horizontal");
        
        if (HorizontalAxis != 0)
            spriteRenderer.flipX = HorizontalAxis < 0;
    }

    List<Sprite> GetSpriteDirection()
    {
        
        float VerticalAxis = Input.GetAxis("Vertical");
        float HorizontallAxis = Input.GetAxis("Horizontal");
        
        List<Sprite> selectedSprites = null;
        if (VerticalAxis > 0)
        {
            //north
            if (Mathf.Abs(HorizontallAxis) > 0)
            {
                //east or west
                selectedSprites = neSprites;
            }
            else //neutral
            {
                selectedSprites = nSprites;
            }
        }
        else if (VerticalAxis < 0)
        {
            if (Mathf.Abs(HorizontallAxis) > 0)
            {
                //east or west
                selectedSprites = seSprites;
            }
            else //neutral horizontal
            {
                selectedSprites = sSprites;
            }
        }
        else // neutral overall
        {
            if (Mathf.Abs(HorizontallAxis) > 0)
            {
                //east or west
                selectedSprites = eSprites;
            }
        }
        
        return selectedSprites;
    }

    async void Update()
    {
        HandleSpriteFlip();
        SetSprite();

        IsMovingRight = Input.GetKey(KeyCode.D);
        IsMovingLeft  = Input.GetKey(KeyCode.A);
        IsMovingUp    = Input.GetKey(KeyCode.W);
        IsMovingDown  = Input.GetKey(KeyCode.S);

        void SetSprite()
        {
            List<Sprite> directionSprites = GetSpriteDirection();
            if (directionSprites != null)
            {
                float playTime = Time.time - idleTime; // time since we started walking 
                int totalFrames = (int)(playTime * frameRate); // (total frames)
                int frame = totalFrames % directionSprites.Count; // (current frame)


                spriteRenderer.sprite = directionSprites[frame];
            }
            else
            {
                //holds nothing, neutral key
                idleTime = Time.time;
            }
        }

        //player movement

        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }

    public void Die()
    {
        print("You're Dead");
        Destroy(this.gameObject);
        //healthText.text = ("0");
        //Instantiate(deathParticles, transform.position, Quaternion.identity);
        //Instantiate(deathSound, transform.position, Quaternion.identity);
        //SceneManager.LoadScene("Menu");
    }
}