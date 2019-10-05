using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RelationshipFeelingData
{
    [SerializeField]
    private string type;
    public string Type { get { return type; } }

    [SerializeField]
    private int value;
    public int Value { get { return value; } }

    [SerializeField]
    private int recent;
    public int Recent { get { return recent; } }
}
