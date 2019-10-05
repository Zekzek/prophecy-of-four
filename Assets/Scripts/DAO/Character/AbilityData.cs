using System;
using UnityEngine;

[Serializable]
public class AbilityData
{
    [SerializeField]
    private string id;
    public string Id { get { return id; } }

    [SerializeField]
    private string displayName;
    public string DisplayName { get { return displayName; } }

    [SerializeField]
    private string description;
    public string Description { get { return description; } }

    [SerializeField]
    private ResourceCostData[] resourceCost;
    public ResourceCostData[] ResourceCost { get { return resourceCost; } }

    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }

    [SerializeField]
    private Util.DamageType[] damageTypes;
    public Util.DamageType[] DamageTypes { get { return damageTypes; } }

    [SerializeField]
    private StatData[] stats;
    public StatData[] Stats { get { return stats; } }

    public override string ToString() { return JsonUtility.ToJson(this, true); }
}