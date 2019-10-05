using System;
using UnityEngine;

[Serializable]
public class ResourceData
{
    [SerializeField]
    private string type;
    public string Type { get { return type; } }

    [SerializeField]
    private float min;
    public float Min { get { return min; } }

    [SerializeField]
    private float max;
    public float Max { get { return max; } }

    [SerializeField]
    private float current;
    public float Current { get { return current; } }

    public ResourceData(string type, float min, float max, float current)
    {
        this.type = type;
        this.min = min;
        this.max = max;
        this.current = current;
    }
}
