using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{

    [TextArea]
    public string[] rules;

    public enum Destination
    {
        Heaven,
        Hell,
        None
    }

    [System.Serializable]
    public class CharacterData
    {
        public CharacterObject character;
        public Destination destination;
        public Destination playerGuessDestination = Destination.None;
        public Vector3 spawnPosition;
    }

    
    public CharacterData[] characters;

}
