using System;
using UnityEngine;

#nullable enable
public class HandSign
{
    public string name;
    public Vector2 position;
    public float rotation;
    public bool isLeftHand;
    public Func<float, Vector2>? movementFunction;

    public HandSign(string name, Vector2 position, float rotation, bool isLeftHand, Func<float, Vector2> movementFunction)
    {
        this.name = name;
        this.position = position;
        this.rotation = rotation;
        this.isLeftHand = isLeftHand;
        this.movementFunction = movementFunction;
    }
}
