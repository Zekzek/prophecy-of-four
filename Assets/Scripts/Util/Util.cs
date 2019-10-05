using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Util
{
    public enum DamageType { Physical, Magical, Melee, Ranged, Crushing, Piercing, Slashing, Fire, Cold, Lightning }

    private const float CLOSE_ENOUGH = 0.001f;

    private static Util instance;
    private static Util Instance { get { return instance == null ? instance = new Util() : instance; } }

    private Dictionary<string, SongData> songs = new Dictionary<string, SongData>();
    private Dictionary<string, AbilityData> abilities = new Dictionary<string, AbilityData>();
    private Dictionary<string, PassiveData> passives = new Dictionary<string, PassiveData>();
    private Dictionary<string, EquipmentData> equipments = new Dictionary<string, EquipmentData>();
    private Dictionary<string, WorldTile> worldTiles = new Dictionary<string, WorldTile>();
    private Dictionary<string, Thing> things = new Dictionary<string, Thing>();
    private Dictionary<string, Character> characters = new Dictionary<string, Character>();
    private GameObject thingPrefab;
    private GameObject characterPrefab;

    private TYPE LoadData<TYPE>(string path, string id)
    {
        string filepath = path + "/" + id;
        if (PlayerPrefs.HasKey(filepath))
        {
            Debug.Log("Loading " + filepath + " from PlayerPrefs");
            TYPE data = JsonUtility.FromJson<TYPE>(PlayerPrefs.GetString(filepath));
            return data;
        }
        else
        {
            TextAsset json = Resources.Load<TextAsset>(filepath);
            if (json != null && !String.IsNullOrEmpty(json.text))
            {
                Debug.Log("Loading " + filepath + " from Resources");
                TYPE data = JsonUtility.FromJson<TYPE>(json.text);
                Resources.UnloadAsset(json);
                return data;
            }
        }

        Debug.LogWarning("Unable to find content for " + filepath);
        return default(TYPE);
    }

    public static Thing GetThing(string id)
    {
        if (!Instance.things.ContainsKey(id))
        {
            if (Instance.thingPrefab == null)
                Instance.thingPrefab = Resources.Load<GameObject>("Prefabs" + Path.DirectorySeparatorChar + "Thing");
            Instance.things[id] = new Thing(Instance.LoadData<ThingData>("Things", id), Instance.thingPrefab);
        }
        return Instance.characters[id];
    }

    public static Character GetCharacter(string id)
    {
        if (!Instance.characters.ContainsKey(id))
        {
            if (Instance.characterPrefab == null)
                Instance.characterPrefab = Resources.Load<GameObject>("Prefabs" + Path.DirectorySeparatorChar + "Character");
            Instance.characters[id] = new Character(Instance.LoadData<CharacterData>("Characters", id), Instance.characterPrefab);
        }
        return Instance.characters[id];
    }

    public static WorldTile GetWorldTile(int x, int y)
    {
        string id = x + "_" + y;
        if (!Instance.worldTiles.ContainsKey(id))
        {
            WorldTileData data = Instance.LoadData<WorldTileData>("WorldTiles", id);
            if (data == null)
                return null;
            Instance.worldTiles[id] = new WorldTile(data);
        }
        return Instance.worldTiles[id];
    }

    public static SongData GetSongData(string id)
    {
        if (!Instance.songs.ContainsKey(id))
            Instance.songs[id] = Instance.LoadData<SongData>("Music", id);
        return Instance.songs[id];
    }

    public static PassiveData GetPassiveData(string id)
    {
        if (!Instance.passives.ContainsKey(id))
            Instance.passives[id] = Instance.LoadData<PassiveData>("Passives", id);
        return Instance.passives[id];
    }

    public static AbilityData GetAbilityData(string id)
    {
        if (!Instance.abilities.ContainsKey(id))
            Instance.abilities[id] = Instance.LoadData<AbilityData>("Abilities", id);
        return Instance.abilities[id];
    }

    public static EquipmentData GetEquipmentData(string id)
    {
        if (!Instance.equipments.ContainsKey(id))
            Instance.equipments[id] = Instance.LoadData<EquipmentData>("Equipments", id);
        return Instance.equipments[id];
    }

    private void SaveData<TYPE>(TYPE data, string path, string id)
    {
        string filepath = path + Path.DirectorySeparatorChar + id;
        PlayerPrefs.SetString(filepath, data.ToString());
    }

    public static void SaveCharacter(Character character)
    {
        Instance.SaveData(character.AsCharacterData(), "Characters", character.id);
        Instance.characters[character.id] = character;
    }

    public static void SaveSongJson(string json, string filename)
    {
        Instance.SaveData(json, "Music", filename);
    }

    public static int GetDamage(AbilityData ability, Character attacker, Thing defender)
    {
        throw new NotImplementedException();
        /*
        float damageBonus = AbilityModifier.GetAverageModifier(attacker.DamageModifier, ability.DamageTypes);
        float damageResist = AbilityModifier.GetAverageModifier(defender.DamageModifier, ability.DamageTypes);
        float netDamageBonus = Mathf.Max(1 + damageBonus, 0) * Mathf.Max(1 - damageResist, 0);

        float piercingBonus = AbilityModifier.GetSumModifier(attacker.PiercingModifier, ability.DamageTypes);
        float piercingResist = AbilityModifier.GetSumModifier(defender.PiercingModifier, ability.DamageTypes);
        float netPiercingBonus = Mathf.Max(piercingBonus - piercingResist, 0) * UnityEngine.Random.Range(0.5f, 1.5f);

        float offenseBonus = AbilityModifier.GetSumModifier(attacker.OffenseModifier, ability.DamageTypes);
        float offenseResist = AbilityModifier.GetSumModifier(defender.OffenseModifier, ability.DamageTypes);
        offenseResist = Mathf.Max(offenseResist - netPiercingBonus, 0);
        float netOffenseBonus = offenseBonus > offenseResist ? Mathf.Sqrt(offenseBonus - offenseResist) : -Mathf.Sqrt(offenseResist - offenseBonus);
        netOffenseBonus = Mathf.Max(1 + netOffenseBonus / 100, 0) * UnityEngine.Random.Range(0.5f, 1.5f); ;

        float critBonus = AbilityModifier.GetSumModifier(attacker.CritModifier, ability.DamageTypes);
        float critResist = AbilityModifier.GetSumModifier(defender.CritModifier, ability.DamageTypes);
        float netCritBonus = Mathf.Max(1 + critBonus, 0) * Mathf.Max(1 - critResist, 0) * UnityEngine.Random.Range(0.5f, 1.5f); ;

        int totalDamage = (int)(ability.Damage * netDamageBonus * netOffenseBonus);

        return totalDamage;
        */
    }

    public static bool CloseEnough(float a, float b) { return a + CLOSE_ENOUGH > b && a - CLOSE_ENOUGH < b; }
}