using UnityEngine;
using System;

[Serializable]
public class SerializableColor
{
    public float r = 0.0f;
    public float g = 0.0f;
    public float b = 0.0f;
    
    public static SerializableColor white = new SerializableColor(1,1,1);

    public SerializableColor(float r, float g, float b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }

    public static implicit operator Color(SerializableColor color)
    {
        return new Color(color.r, color.g, color.b, 1.0f);
    }

    public static implicit operator SerializableColor(Color color)
    {
        return new SerializableColor(color.r, color.g, color.b);
    }

}
