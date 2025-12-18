using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterObject", menuName = "Scriptable Objects/CharacterObject")]
public class CharacterObject : ScriptableObject
{
    public Sprite body;
    [Space]

    [SerializedDictionary("Handshape", "Sprite")]
    public SerializedDictionary<HandSign.HandShape, HandSignSprite> handSprites;
    public Color exteriorColor;
    public Color palmColor;
    public Color clawColor;
    [Space]

    [SerializedDictionary("Question", "Responses")]
    public SerializedDictionary<string, Gesture.GestureEnum[]> responses;
}
