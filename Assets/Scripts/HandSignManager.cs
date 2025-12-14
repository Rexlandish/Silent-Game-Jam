using UnityEngine;
using AYellowpaper.SerializedCollections;
using System;
using static Util;
public class HandSignManager : MonoBehaviour
{
    public HandManager hand1;
    public HandManager hand2;

    Gesture[] gestureList;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Example data
        LoadData();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadData()
    {

        Gesture g1 = new Gesture(

            new HandSign[]
            {
                new(
                    "point",
                    new Vector2(-1f, 0f),
                    -90f,
                    false,
                    new Func<float, Vector2>((time) =>
                    {
                        return new Vector2(Mathf.Sin(time * 8f) * 0.5f, 0f);
                    })
                ),
                new(
                    "point",
                    new Vector2(1f, 0f),
                    90f,
                    true,
                    new Func<float, Vector2>((time) =>
                    {
                        return new Vector2(Mathf.Sin(time * 8f + Mathf.PI) * 0.5f, 0f);
                    })
                )
            }
        );

        Gesture g2 = new Gesture(

            new HandSign[]
            {
                new(
                    "thumb",
                    new Vector2(-1f, 0f),
                    0f,
                    false,
                    Util.Oscillate(
                        new Vector2(-0.5f, -0.1f),
                        new Vector2(0.5f, 0f),
                        4
                        )
                ),
                new(
                    "open",
                    new Vector2(2f, 1.5f),
                    0f,
                    true,
                    null
                )
            }
        );

        Gesture g3 = new Gesture(

            new HandSign[]
            {
                new(
                    "pinkie",
                    new Vector2(0f, 0f),
                    0f,
                    false,
                    null
                ),
                new(
                    "point",
                    new Vector2(1f, 1f),
                    90f,
                    true,
                    Circle(
                        new Vector2(0.25f, -0.25f),
                        new Vector2(-0.25f, 0.25f), 
                        7f
                    )
                )
            }
        );

        Gesture g4 = new Gesture(

            new HandSign[]
            {
                new(
                    "middle",
                    new Vector2(-2f, 0.5f),
                    -30f,
                    false,
                    Oscillate(
                        Vector2.up,
                        Vector2.down,
                        3f
                        )
                ),
                new(
                    "middle",
                    new Vector2(2f, 0.5f),
                    30f,
                    true,
                    Oscillate(
                        Vector2.down,
                        Vector2.up,
                        3f
                        )
                )
            }
        );

        Gesture g5 = new Gesture(

            new HandSign[]
            {
                new(
                    "open",
                    new Vector2(0f, -6f),
                    0f,
                    false,
                    null
                ),
                new(
                    "open",
                    new Vector2(1f, 1f),
                    0f,
                    false,
                    Oscillate(
                        new Vector2(-1f, 0),
                        new Vector2(2f, 0),
                        3f
                    )
                )
            }
        );

        Gesture g6 = new Gesture(

            new HandSign[]
            {
                new(
                    "open",
                    new Vector2(-3f, -3f),
                    0f,
                    false,
                    null
                ),
                new(
                    "open",
                    new Vector2(3f, -3f),
                    0f,
                    true,
                    null
                )
            }
        );

        gestureList = new Gesture[] { g1, g2, g3, g4, g5, g6 };
    }

    int gestureIndex = 0;
    public void DoNextGesture()
    {
        DoHandSign(gestureList[gestureIndex]);
        gestureIndex = (gestureIndex + 1) % gestureList.Length;
    }

    public void DoHandSign(Gesture gesture)
    {
        hand1.Perform(gesture.handSigns[0]);
        hand2.Perform(gesture.handSigns[1]);
    }
}
