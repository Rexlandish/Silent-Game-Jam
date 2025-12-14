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

    public void Perform(HandSign handSign)
    {
        sr.sprite = handPositions[handSign.name];
        isLeftHand = handSign.isLeftHand;
        targetPosition = handSign.position;
        targetRotation = handSign.rotation;
        movementFunction = handSign.movementFunction;
    }
}
