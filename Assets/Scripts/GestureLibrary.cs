using UnityEngine;
using AYellowpaper.SerializedCollections;
using static Gesture.GestureEnum;
using static Util;
using static HandSign.HandShape;
public class GestureLibrary {
    [SerializedDictionary("Gesture Name", "Gesture")]
    public static SerializedDictionary<Gesture.GestureEnum, Gesture> gestureLibrary = new()
    {
        {
            Heaven, new Gesture(
                new[]
                {
                    new HandSign(
                        PointFront,
                        new(-1.5f, 0.5f),
                        0f,
                        false,
                        null
                        ),
                    new HandSign(
                        PointFront,
                        new(1.5f, 0.5f),
                        0f,
                        true,
                        null
                        )
                }
            )
        },
        {
            Hell, new Gesture(
                new[]
                {
                    new HandSign(
                        PointBack,
                        new(-1.5f, -0.5f),
                        180f,
                        false,
                        null
                        ),
                    new HandSign(
                        PointBack,
                        new(1.5f, -0.5f),
                        -180f,
                        true,
                        null
                        )
                }
            )
        },
        {
            Yes, new Gesture(
                new[]
                {
                    new HandSign(
                        PeaceFront,
                        new(-2f, 0f),
                        0f,
                        false,
                        null
                        ),
                    new HandSign(
                        PeaceFront,
                        new(2f, 0f),
                        0f,
                        true,
                        null
                        )
                }
            )
        },
        {
            No, new Gesture(
                new[]
                {
                    null,
                    new HandSign(
                        PalmFront,
                        new(1f, 0f),
                        0f,
                        true,
                        Oscillate(
                            Vector2.left * 0.3f,
                            Vector2.right * 0.3f,
                            3f
                            )
                        )
                }
            )
        },
        {
            Dragon, new Gesture(
                new[]
                {
                    new HandSign(
                        Index,
                        new(-1f, 3f),
                        0f,
                        false,
                        Circle(
                            new Vector2(1f, -1f) * 0.25f,
                            new Vector2(-1f, 1f) * 0.25f,
                            8f
                            )
                        ),
                    new HandSign(
                        Index,
                        new(1f, 3f),
                        0f,
                        true,
                        Circle(
                            new Vector2(1f, -1f) * 0.25f,
                            new Vector2(-1f, 1f) * 0.25f,
                            -8f
                            )
                        )
                }
            )
        },
        {
            Cat, new Gesture(
                new[]
                {
                    new HandSign(
                        PeaceBack,
                        new(-1.5f, 1f),
                        -90f,
                        false,
                        Circle(
                            new Vector2(1f, -1f) * 0.25f,
                            new Vector2(-1f, 1f) * 0.25f,
                            8f
                            )
                        ),
                    new HandSign(
                        PeaceBack,
                        new(1.5f, 1f),
                        90f,
                        true,
                        Circle(
                            new Vector2(1f, -1f) * 0.25f,
                            new Vector2(-1f, 1f) * 0.25f,
                            -8f
                            )
                        )
                }
            )
        },
        {
            Murder, new Gesture(
                new[]
                {
                    new HandSign(
                        PeaceBack,
                        new(0f, 1f),
                        -60f,
                        false,
                        Oscillate(
                            new Vector2(0.5f, -1f),
                            new Vector2(-0.5f, 1f),
                            4f
                            )
                        ),
                    null
                }
            )
        },
        {
            Victim, new Gesture(
                new[]
                {
                    null,

                    new HandSign(
                        PalmFront,
                        new(0f, 1f),
                        60f,
                        false,
                        Oscillate(
                            new Vector2(-0.5f, -1f),
                            new Vector2(0.5f, 1f),
                            2f
                            )
                        )
                }
            )
        },
        {
            You, new Gesture(
                new[]
                {
                    new HandSign(
                        PointFront,
                        new(-2f, 0f),
                        30f,
                        false,
                        null
                        ),
                    null
                }
            )
        },
        {
            Me, new Gesture(
                new[]
                {
                    new HandSign(
                        PointBack,
                        new(-1f, 0f),
                        -30f,
                        false,
                        Oscillate(
                            new Vector2(0.5f, 1f) * 0.25f,
                            new Vector2(-0.5f, -1f) * 0.25f,
                            5f
                            )
                        ),
                    null
                }
            )
        },
        {
            Opposite, new Gesture(
                new[]
                {
                    new HandSign(
                        Index,
                        new(-0.5f, 0f),
                        -30f,
                        false,
                        null
                        ),
                    new HandSign(
                        Index,
                        new(0.5f, 0f),
                        30f,
                        true,
                        Oscillate(
                            new Vector2(0f, 0f),
                            new Vector2(1f, 0f),
                            3f
                            )
                        ),
                }
            )
        },
        {
            Rest, new Gesture(
                new HandSign[]
                {
                    null,
                    null
                }
            )
        }
    };
}
