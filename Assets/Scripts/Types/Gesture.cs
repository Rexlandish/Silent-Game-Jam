public class Gesture
{
    public enum GestureEnum
    {
        Heaven,
        Hell,
        Yes,
        No,
        Dragon,
        Cat,
        Murderer,
        Victim,
        You,
        Me,
        Opposite,
        Rest
    }


    public HandSign[] handSigns;

    public Gesture(HandSign[] handSigns)
    {
        this.handSigns = handSigns;
    }
}