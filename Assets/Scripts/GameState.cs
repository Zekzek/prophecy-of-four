using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public delegate float GetScore<T>(T thing) where T : MonoBehaviour;

    private List<ThingBehaviour> things = new List<ThingBehaviour>();
    //private List<ResourceFountain> resources = new List<ResourceFountain>();

    public void Add(ThingBehaviour thing)
    {
        things.Add(thing);
    }

    /*
    public void Add(ResourceFountain resource)
    {
        resources.Add(resource);
    }
    */

    public ThingBehaviour GetClosestThing(Vector3 position)
    {
        return CalcClosestThing(position, things, (thing) => { return 100f; });
    }

    public T CalcClosestThing<T>(Vector3 position, List<T> list, GetScore<T> GetScore) where T : MonoBehaviour
    {
        T bestItem = null;
        float hiScore = float.NegativeInfinity;

        foreach (T item in list)
        {
            float score = GetScore(item) / (position - item.transform.position).sqrMagnitude;
            if (score > hiScore)
            {
                hiScore = score;
                bestItem = item;
            }
        }

        return bestItem;
    }

    public Vector3 CalculateDesiredDirection(Vector3 position, Dictionary<string, float> priorities)
    {
        Dictionary<string, Vector3> map = new Dictionary<string, Vector3>();
        /*
        foreach (ResourceFountain resource in resources)
        {
            Vector3 resourcePosition = resource.transform.position;
            Vector3 deltaPosition = resourcePosition - position;
            string resourceType = resource.Resource.Type.ToString().ToLower();
            map[resourceType] = deltaPosition / deltaPosition.sqrMagnitude + (map.ContainsKey(resourceType) ? map[resourceType] : Vector3.zero);
        }
        */

        Vector3 target = Vector3.zero;
        foreach (KeyValuePair<string, Vector3> pair in map)
            if (priorities.ContainsKey(pair.Key))
                target += pair.Value.normalized * priorities[pair.Key];

        return target.sqrMagnitude < 1 ? target : target.normalized;
    }

    public void Clear()
    {
        instance = null;
    }

    // Singleton
    private static GameState instance;
    public static GameState Instance
    {
        get
        {
            if (instance == null)
                instance = new GameState();
            return instance;
        }
    }
}