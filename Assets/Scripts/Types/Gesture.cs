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
        Murder,
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