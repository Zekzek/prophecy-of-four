using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                WorldTile tile = Util.GetWorldTile(x, y);
                if (tile != null)
                {
                    tile.InstantiateGameObject();
                }
            }
        }
    }

    void Update()
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                WorldTile tile = Util.GetWorldTile(x, y);
                if (tile != null)
                {
                    tile.Tick();
                }
            }
        }
    }

    private void ChangeCharacter(ref Character character)
    {
        character.DisplayName = "Changed Name";
    }
}
