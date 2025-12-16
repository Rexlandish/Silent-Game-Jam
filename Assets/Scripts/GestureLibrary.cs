using UnityEngine;
using AYellowpaper.SerializedCollections;
using static Gesture.GestureEnum;
using static Util;

public class GestureLibrary {
    [SerializedDictionary("Gesture Name", "Gesture")]
    public static SerializedDictionary<Gesture.GestureEnum, Gesture> gestureLibrary = new()
    {
        {
            Heaven, new Gesture(
                new[]
                {
                    new HandSign(
                        "point-front",
                        new(-1.5f, 0.5f),
                        0f,
                        false,
                        null
                        ),
                    new HandSign(
                        "point-front",
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
                        "point-back",
                        new(-1.5f, -0.5f),
                        180f,
                        false,
                        null
                        ),
                    new HandSign(
                        "point-back",
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
                        "peace-front",
                        new(-2f, 0f),
                        0f,
                        false,
                        null
                        ),
                    new HandSign(
                        "peace-front",
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
                        "palm-front",
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
                        "index-front",
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
                        "index-front",
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
                        "peace-back",
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
                        "peace-back",
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
            Murderer, new Gesture(
                new[]
                {
                    new HandSign(
                        "peace-back",
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
                        "palm-front",
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
                        "point-front",
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
                        "point-back",
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
                        "index-front",
                        new(-0.5f, 0f),
                        -30f,
                        false,
                        null
                        ),
                    new HandSign(
                        "index-front",
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
