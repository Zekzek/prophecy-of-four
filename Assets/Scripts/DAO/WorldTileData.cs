using System;
using UnityEngine;

[Serializable]
public class WorldTileData
{
    [SerializeField]
    private int x;
    public int X { get { return x; } }

    [SerializeField]
    private int y;
    public int Y { get { return y; } }

    [SerializeField]
    private string[] thingIds;
    public string[] ThingIds { get { return thingIds ?? new string[0]; } }

    [SerializeField]
    private string[] characterIds;
    public string[] CharacterIds { get { return characterIds ?? new string[0]; } }

    public WorldTileData(int x, int y, string[] thingIds, string[] characterIds)
    {
        this.x = x;
        this.y = y;
        this.thingIds = thingIds;
        this.characterIds = characterIds;
    }

    public override string ToString() { return JsonUtility.ToJson(this, true); }
}