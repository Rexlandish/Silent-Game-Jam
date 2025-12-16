using System;
using UnityEngine;

#nullable enable
public class HandSign
{
    public HandShape shape;
    public Vector2 position;
    public float rotation;
    public bool isLeftHand;
    public Func<float, Vector2>? movementFunction;

    public enum HandShape
    {
        Index,
        PointFront,
        PointBack,
        PeaceFront,
        PeaceBack,
        PalmFront
    }

    public HandSign(HandShape shape, Vector2 position, float rotation, bool isLeftHand, Func<float, Vector2> movementFunction)
    {
        this.shape = shape;
        this.position = position;
        this.rotation = rotation;
        this.isLeftHand = isLeftHand;
        this.movementFunction = movementFunction;
    }
}
