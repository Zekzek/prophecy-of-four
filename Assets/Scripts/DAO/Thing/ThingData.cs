using System;
using UnityEngine;

[Serializable]
public class ThingData
{
    [SerializeField]
    private string id;
    public string Id { get { return id; } }

    [SerializeField]
    private string templateId;
    public string TemplateId { get { return templateId; } }

    [SerializeField]
    private string displayName;
    public string DisplayName { get { return displayName; } }

    [SerializeField]
    private Vector3 position;
    public Vector3 Position { get { return position; } }

    [SerializeField]
    private ResourceData[] resources;
    public ResourceData[] Resources { get { return resources; } }

    [SerializeField]
    private StatData[] stats;
    public StatData[] Stats { get { return stats; } }

    [SerializeField]
    private string[] passives;
    public string[] Passives { get { return passives; } }

    [SerializeField]
    private bool inventoryable;
    public bool Inventoryable { get { return inventoryable; } }

    [SerializeField]
    private bool flammable;
    public bool Flammable { get { return flammable; } }

    [SerializeField]
    private bool lockable;
    public bool Lockable { get { return lockable; } }

    public ThingData(string id, string templateId, string displayName, Vector3 position, ResourceData[] resources,
            StatData[] stats, string[] passives)
    {
        this.id = id;
        this.templateId = templateId;
        this.displayName = displayName;
        this.position = position;
        this.resources = resources;
        this.stats = stats;
        this.passives = passives;
    }

    public override string ToString() { return JsonUtility.ToJson(this, true); }
}
