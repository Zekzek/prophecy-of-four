using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : Thing
{
    private readonly Character template;
    private List<AbilityData> abilities = new List<AbilityData>();
    private List<RelationshipData> relationships;

    public Character(CharacterData data, GameObject prefab) : base(data, prefab)
    {
        abilities = Array.ConvertAll(data.Abilities, a => Util.GetAbilityData(a)).ToList().FindAll(a => a != null);
        relationships = data.Relationships.ToList();

        if (!string.IsNullOrEmpty(data.TemplateId)) template = Util.GetCharacter(data.TemplateId);
    }

    public CharacterData AsCharacterData()
    {
        return new CharacterData(id,
            template == null ? null : template.id,
            displayName,
            position,
            resources.Values.ToList().ConvertAll(r => r.AsResourceData()).ToArray(),
            stats.ConvertAll(s => s.AsStatData()).ToArray(),
            passives.FindAll(p => p != null).ConvertAll(p => p.Id).ToArray(),
            abilities.FindAll(a => a != null).ConvertAll(a => a.Id).ToArray(),
            relationships.ToArray()
        );
    }

    protected override void InitBehaviour(GameObject go)
    {
        behaviour = go.GetComponent<CharacterBehaviour>();
        behaviour.DisplayName = displayName;
    }

    public override void Tick()
    {
        base.Tick();
        if (behaviour != null)
            ((CharacterBehaviour)behaviour).Goal = CalcGoal();
    }

    public void SpendResourcesFor(AbilityData ability)
    {
        foreach (ResourceCostData costData in ability.ResourceCost)
        {
            resources[costData.Resource].Current -= costData.Cost;
        }
    }

    private GameObject CalcGoal()
    {
        string id = relationships.First()?.CharacterId;
        if (!string.IsNullOrEmpty(id))
            return Util.GetCharacter(id).behaviour.gameObject;
        return null;
    }

    public override string ToString()
    {
        return AsCharacterData().ToString() + (template == null ? "" : "\n  -- Based on " + template.id + "--\n" + template);
    }
}