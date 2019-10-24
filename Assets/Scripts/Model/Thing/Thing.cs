using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UtiliZek;

[Serializable]
public class Thing
{
    public readonly string id;
    private readonly Thing template;

    protected string displayName;
    protected Vector3 position;
    protected Dictionary<string, ThingResource> resources;
    protected List<Stat> stats;
    protected List<PassiveData> passives = new List<PassiveData>();

    private GameObject prefab;
    protected ThingBehaviour behaviour;

    public Thing(ThingData data, GameObject prefab)
    {
        id = data.Id;
        displayName = data.DisplayName;
        position = data.Position;
        resources = Array.ConvertAll(data.Resources, r => new ThingResource(r)).ToDictionary(r => r.Type);
        stats = Array.ConvertAll(data.Stats, s => new Stat(s)).ToList();
        passives = Array.ConvertAll(data.Passives, p => FileManager.GetPassiveData(p)).ToList().FindAll(p => p != null);

        if (!string.IsNullOrEmpty(data.TemplateId)) template = FileManager.GetCharacter(data.TemplateId);

        this.prefab = prefab;
    }

    public ThingData AsThingData()
    {
        return new ThingData(id,
            template == null ? null : template.id,
            displayName,
            position,
            resources.Values.ToList().ConvertAll(r => r.AsResourceData()).ToArray(),
            stats.ConvertAll(s => s.AsStatData()).ToArray(),
            passives.FindAll(p => p != null).ConvertAll(p => p.Id).ToArray()
        );
    }

    public void Instantiate(Transform parent)
    {
        GameObject go = GameObject.Instantiate(prefab, parent);
        go.transform.position = position;
        InitBehaviour(go);
    }

    protected virtual void InitBehaviour(GameObject go)
    {
        behaviour = go.GetComponent<ThingBehaviour>();
        behaviour.DisplayName = displayName;
    }

    public virtual void Tick()
    {
    }

    public bool Alive { get { return resources.ContainsKey(ThingResource.HEALTH_ID) && !resources[ThingResource.HEALTH_ID].IsEmpty; } }
    public string DisplayName
    {
        get { return (string.IsNullOrEmpty(displayName) && template != null) ? template.DisplayName : displayName; }
        set
        {
            displayName = (template != null && template.DisplayName == value) ? null : value;
            if (behaviour != null)
                behaviour.DisplayName = displayName;
        }
    }

    public bool HasResource(ResourceCostData resourceCost)
    {
        return resources.ContainsKey(resourceCost.Resource) && resources[resourceCost.Resource].Current >= resourceCost.Cost;
    }

    public void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }

    public void AddOrReplacePassive(string passiveId)
    {
        PassiveData passive = FileManager.GetPassiveData(passiveId);
        passives.RemoveAll(p => p.Type == passive.Type);
        passives.Add(passive);
    }

    public PassiveData GetPassiveByType(string passiveType)
    {
        PassiveData passive = passives.Find(p => p.Type == passiveType);
        if (passive != null)
            return passive;
        if (template != null)
            return template.GetPassiveByType(passiveType);
        return null;
    }

    public override string ToString()
    {
        return AsThingData().ToString() + (template == null ? "" : "\n  -- Based on " + template.id + "--\n" + template);
    }

    /*
        public float TimeSinceDamageTaken { get; private set; }

        public virtual void Start()
        {
            DefenseStats = GetComponent<DefenseStats>();

            shield.onEmpty += (remainingDamage) => life.Current -= remainingDamage;
            life.onEmpty += Die;

            Conditional.IsActive aliveCondition = Conditional.CreateIsActiveForAlive(this);
            Conditional.IsActive safeCondition = Conditional.CreateIsActiveForSafe(this);
            Conditional.IsActive aliveAndSafeCondition = Conditional.CreateIsActive(aliveCondition, safeCondition);

            GameState.Instance.Add(this);

            ApplyPassive(life, LIFE_REGEN, 4, aliveCondition);
            ApplyPassive(shield, SHIELD_REGEN, 1, aliveAndSafeCondition);
        }
    */
}