using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UtiliZek;

public class WorldTile
{
    public readonly int x;
    public readonly int y;
    protected List<Thing> things;
    protected List<Character> characters;

    private GameObject gameObject;

    public bool Instantiated { get { return gameObject != null; } }

    public WorldTile(WorldTileData data)
    {
        x = data.X;
        y = data.Y;
        things = Array.ConvertAll(data.ThingIds, id => FileManager.GetThing(id)).ToList();
        characters = Array.ConvertAll(data.CharacterIds, id => FileManager.GetCharacter(id)).ToList();
    }

    public WorldTileData AsWorldTileData()
    {
        return new WorldTileData(x, y,
            things.ConvertAll(t => t.id).ToArray(),
            characters.ConvertAll(c => c.id).ToArray()
        );
    }

    public bool InstantiateGameObject()
    {
        if (gameObject == null)
        {
            gameObject = new GameObject();
            gameObject.transform.position = new Vector3(x, 0, y);
            gameObject.name = "WorldTile_" + x + "_" + y;

            foreach (Thing thing in things)
                thing.Instantiate(gameObject.transform);
            foreach (Character character in characters)
                character.Instantiate(gameObject.transform);

            return true;
        }
        return false;
    }

    public bool DestroyGameObject()
    {
        if (gameObject != null)
        {
            GameObject.Destroy(gameObject);
            gameObject = null;

            return true;
        }
        return false;
    }

    public void Tick()
    {
        foreach (Thing thing in things)
            thing.Tick();
        foreach (Character character in characters)
            character.Tick();
    }

    public override string ToString()
    {
        return AsWorldTileData().ToString();
    }
}