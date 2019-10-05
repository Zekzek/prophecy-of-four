using System;
using UnityEngine;

[Serializable]
public class EquipmentData : ThingData
{
    //TODO: Add equipment specific properties

    public EquipmentData(string id, string templateId, string displayName, Vector3 position, ResourceData[] resources,
        StatData[] stats, string[] passives)
        : base(id, templateId, displayName, position, resources, stats, passives)
    { }

    public override string ToString() { return JsonUtility.ToJson(this, true); }
}