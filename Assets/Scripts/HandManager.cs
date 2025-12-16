using AYellowpaper.SerializedCollections;
using System;
using Unity.Burst;
using UnityEngine;
using Random = UnityEngine.Random;

#nullable enable
public class HandManager : MonoBehaviour
{

    public SerializedDictionary<string, Sprite> handPositions;

    public Vector2 targetPosition;
    public float targetRotation;
    public float offsetStrength = 0.5f;
    public float moveSpeed;
    public float rotationSpeed;
    public bool isLeftHand;
    public Func<float, Vector2>? movementFunction;

    [Space]
    public bool initialTransformIsTargetPosition;
    public bool hideInitially;

    SpriteRenderer sr;

    Vector2 movementPositionOffset;
    Vector2 positionNoise;
    Vector2 perlinNoiseOffset;

    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (hideInitially)
        {
            targetOpacity = 0;
            Perform(null);
        }

        perlinNoiseOffset = new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f));
        if (initialTransformIsTargetPosition) targetPosition = transform.position;
    }

    public void SetSprite(Sprite sprite)
    {
        sr.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        // Update opacity
        currentOpacity = Mathf.Lerp(currentOpacity, targetOpacity, 10f * Time.deltaTime);
        sr.color = new Color(1f, 1f, 1f, currentOpacity);

        movementPositionOffset = movementFunction?.Invoke(Time.time) ?? Vector2.zero;

        sr.flipX = isLeftHand;
        positionNoise = new Vector2(
            Mathf.PerlinNoise(Time.time + perlinNoiseOffset.x, 0f),
            Mathf.PerlinNoise(0f, Time.time + perlinNoiseOffset.y)
        );
        
        transform.localPosition = Vector2.Lerp(
            transform.localPosition,
            targetPosition + movementPositionOffset + positionNoise * offsetStrength, 
            Time.deltaTime * moveSpeed
        );

        transform.rotation = Quaternion.Lerp(
            transform.rotation, 
            Quaternion.Euler(0f, 0f, targetRotation), 
            Time.deltaTime * rotationSpeed
        );
    }

    float targetOpacity = 1;
    float currentOpacity = 1;
    public void Perform(HandSign? handSign)
    {
        if (handSign == null)
        {
            targetOpacity = 0;
            movementFunction = null;

            // Set position to resting
            targetPosition = transform.localPosition;
            targetPosition.y = 0;

            movementFunction = null;

            return;
        }
        else
        {
            targetOpacity = 1;
        }

        sr.sprite = handPositions[handSign.name];
        isLeftHand = handSign.isLeftHand;
        targetPosition = handSign.position;
        targetRotation = handSign.rotation;
        movementFunction = handSign.movementFunction;
    }
}
