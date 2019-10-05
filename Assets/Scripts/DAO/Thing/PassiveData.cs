using System;
using UnityEngine;

[Serializable]
public class PassiveData
{
    public string Id { get { return type + "" + level; } }

    [SerializeField]
    private string type;
    public string Type { get { return type; } }

    [SerializeField]
    private int level = 1;
    public int Level { get { return level; } }

    [SerializeField]
    private float changePerTick = 0;
    public float ChangePerTick { get { return changePerTick; } }

    [SerializeField]
    private float percentChangePerTick = 0;
    public float PercentChangePerTick { get { return percentChangePerTick; } }

    [SerializeField]
    private float tickPeriod = 3;
    public float TickPeriod { get { return tickPeriod; } }

    override public string ToString()
    {
        return "Ability: {" +
            " Id: " + Id +
            ", Type: " + Type +
            ", Level: " + Level +
            ", ChangePerTick: " + ChangePerTick +
            ", PercentChangePerTick: " + PercentChangePerTick +
            ", TickPeriod: " + TickPeriod + " }";
    }

}
