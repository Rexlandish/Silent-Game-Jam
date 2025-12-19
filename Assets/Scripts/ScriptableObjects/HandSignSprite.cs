using UnityEngine;

[CreateAssetMenu(fileName = "HandSignSprite", menuName = "Scriptable Objects/HandSignSprite")]
public class HandSignSprite : ScriptableObject
{
    public Sprite sprite;
    public Texture exteriorMask;
    public Texture palmMask;
    public Texture clawMask;
}
