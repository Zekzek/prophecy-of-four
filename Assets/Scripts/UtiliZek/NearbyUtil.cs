using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtiliZek
{
    public class NearbyUtil
    {
        public const string THING_KEY = "thing";
        public const string RESOURCE_KEY = "resource";
        public const string ENEMY_1_KEY = "enemy1";
        public const string ENEMY_2_KEY = "enemy2";
        public const string ENEMY_3_KEY = "enemy3";
        public const string ENEMY_4_KEY = "enemy4";
        public const string HURT_PLAYER_KEY = "hurtplayer";
        public const string PLAYER_1_KEY = "player1";
        public const string PLAYER_2_KEY = "player2";
        public const string PLAYER_3_KEY = "player3";
        public const string PLAYER_4_KEY = "player4";

        private static NearbyUtil instance;
        public static NearbyUtil Instance { get { return instance == null ? (instance = new NearbyUtil()) : instance; } }
        private NearbyUtil() { }

        private Dictionary<string, List<GameObject>> allThings;
        private Dictionary<string, List<GameObject>> AllThings { get { return allThings == null ? (allThings = new Dictionary<string, List<GameObject>>()) : allThings; } }

        private void InitTacking(string key)
        {
            if (!AllThings.ContainsKey(key))
                AllThings[key] = new List<GameObject>();
        }

        public void Track(string key, GameObject go)
        {
            InitTacking(key);
            if (!allThings[key].Contains(go))
                allThings[key].Add(go);
        }

        public void Untrack(string key, GameObject go)
        {
            InitTacking(key);
            if (allThings[key].Contains(go))
                allThings[key].Remove(go);
        }

        public GameObject FindClosest(string key, Vector3 position)
        {
            InitTacking(key);
            GameObject closest = null;
            float closestSquareDistance = float.MaxValue;

            foreach (GameObject thing in allThings[key])
            {
                float sqrDistance = (thing.transform.position - position).sqrMagnitude;
                if (sqrDistance > 0 && sqrDistance < closestSquareDistance)
                {
                    closest = thing;
                    closestSquareDistance = sqrDistance;
                }
            }

            return closest;
        }

        public GameObject FindClosestInRange(string key, Vector3 position, float range)
        {
            InitTacking(key);
            GameObject closest = null;
            float closestSquareDistance = float.MaxValue;
            float sqrRange = range * range;

            foreach (GameObject thing in allThings[key])
            {
                float sqrDistance = (thing.transform.position - position).sqrMagnitude;
                if (sqrDistance > 0 && sqrDistance <= sqrRange && sqrDistance < closestSquareDistance)
                {
                    closest = thing;
                    closestSquareDistance = sqrDistance;
                }
            }

            return closest;
        }

        public List<GameObject> FindAllInRange(string key, Vector3 position, float range)
        {
            InitTacking(key);

            List<GameObject> nearbyObjects = new List<GameObject>();
            float sqrRange = range * range;

            foreach (GameObject thing in allThings[key])
                if ((thing.transform.position - position).sqrMagnitude < sqrRange)
                    nearbyObjects.Add(thing);

            return nearbyObjects;
        }

        public GameObject FindWeightedClosestInRange(string key, Vector3 position, float range)
        {
            InitTacking(key);

            float sqrRange = range * range;
            float totalDistance = 0;

            foreach (GameObject thing in allThings[key])
                if ((thing.transform.position - position).sqrMagnitude < sqrRange)
                    totalDistance += sqrRange - (thing.transform.position - position).sqrMagnitude;

            float selectorValue = Random.value * totalDistance;
            foreach (GameObject thing in allThings[key])
            {
                if ((thing.transform.position - position).sqrMagnitude < sqrRange)
                {
                    selectorValue -= (sqrRange - (thing.transform.position - position).sqrMagnitude);
                    if (selectorValue <= 0)
                    {
                        return thing;
                    }
                }
            }

            return null;
        }
    }
}