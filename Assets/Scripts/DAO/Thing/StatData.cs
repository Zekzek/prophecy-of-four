using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatData
{
    [SerializeField]
    private string type;
    public string Type { get { return type; } }

    [SerializeField]
    private List<string> tags;
    public List<string> Tags { get { return tags; } }

    [SerializeField]
    public float value;
    public float Value { get { return value; } }

    public StatData(string type, List<string> tags, float value)
    {
        this.type = type;
        this.tags = tags;
        this.value = value;
    }
}
