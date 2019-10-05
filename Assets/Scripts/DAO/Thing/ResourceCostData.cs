using System;
using UnityEngine;

[Serializable]
public class ResourceCostData
{
    [SerializeField]
    private string resource;
    public string Resource { get { return resource; } }

    [SerializeField]
    private float cost;
    public float Cost { get { return cost; } }

    public ResourceCostData(string resource, float cost)
    {
        this.resource = resource;
        this.cost = cost;
    }
}
