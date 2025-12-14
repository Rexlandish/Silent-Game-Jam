using System;
using UnityEngine;

public class Util : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static Func<float, Vector2> Circle(Vector2 topLeft, Vector2 bottomRight, float speed = 1)
    {
        return (time) =>
        {
            float x = Mathf.Lerp(
                topLeft.x, 
                bottomRight.x, 
                (Mathf.Sin(time * speed) + 1f) / 2f);

            float y = Mathf.Lerp(
                topLeft.y, 
                bottomRight.y, 
                (Mathf.Cos(time * speed) + 1f) / 2f);
            return new Vector2(x, y);
        };
    }

    public static Func<float, Vector2> Oscillate(Vector2 start, Vector2 end, float speed = 1)
    {
        return (time) =>
        {
            float x = Mathf.Lerp(
                start.x,
                end.x,
                (Mathf.Sin(time * speed * Mathf.PI) + 1f) / 2f);
            float y = Mathf.Lerp(
                start.y,
                end.y,
                (Mathf.Sin(time * speed * Mathf.PI) + 1f) / 2f);

            return new Vector2(x, y);
        };
    }
}
