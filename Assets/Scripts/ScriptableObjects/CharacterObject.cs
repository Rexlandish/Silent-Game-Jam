using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterObject", menuName = "Scriptable Objects/CharacterObject")]
public class CharacterObject : ScriptableObject
{
    public Sprite body;

    [SerializedDictionary("Handshape", "Sprite")]
    public SerializedDictionary<HandSign.HandShape, Sprite> handSprites;
    
    [SerializedDictionary("Question", "Responses")]
    public SerializedDictionary<string, Gesture.GestureEnum[]> responses;
}
