using System;
using UnityEngine;

[Serializable]
public class CharacterData : ThingData
{
    [SerializeField]
    private string[] abilities;
    public string[] Abilities { get { return abilities == null ? abilities = new string[0] : abilities; } }

    [SerializeField]
    private RelationshipData[] relationships;
    public RelationshipData[] Relationships { get { return relationships == null ? relationships = new RelationshipData[0] : relationships; } }

    public CharacterData(string id, string templateId, string displayName, Vector3 position, ResourceData[] resources,
            StatData[] stats, string[] passives, string[] abilities, RelationshipData[] relationships)
            : base(id, templateId, displayName, position, resources, stats, passives)
    {
        this.abilities = abilities;
        this.relationships = relationships;
    }

    public override string ToString() { return JsonUtility.ToJson(this, true); }
}
