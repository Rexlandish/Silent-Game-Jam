using UnityEngine;
using AYellowpaper.SerializedCollections;
using System;
using static Util;
using static Gesture;
using UnityEngine;
using Random = UnityEngine.Random;
public class HandSignManager : MonoBehaviour
{
    public HandManager hand1;
    public HandManager hand2;

    Gesture[] gestureList;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Example data
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoHandSign(Random.Range(0, 11));
        }
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

    public void DoHandSign(GestureEnum gestureEnum)
    {
        Gesture gesture = GestureLibrary.gestureLibrary[gestureEnum];
        DoHandSign(gesture);
    }

    // For unity button
    public void DoHandSign(int i)
    {
        DoHandSign((GestureEnum)i);
    }
}
