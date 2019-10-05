using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainBlock : MonoBehaviour
{

    [SerializeField]
    private int cubeSize = 1;
    private int lastCubeSize;

    [SerializeField]
    private new Renderer renderer;
    [SerializeField]
    private new Collider collider;

    private const int MIN_X = -10;
    private const int MAX_X = 10;
    private const int MIN_Z = -3;
    private const int MAX_Z = 3;
    public static readonly List<TerrainBlock> blocks = new List<TerrainBlock>();

    void Awake()
    {
        blocks.Add(this);
        //Generate(Vector3.left, 1);
        //Generate(Vector3.right, 1);
        //Generate(Vector3.forward, 1);
        //Generate(Vector3.back, 1);
    }

    void Update()
    {
        SetCubeSize(cubeSize);
    }

    private static bool InBounds(Vector3 position)
    {
        return position.x >= MIN_X && position.x <= MAX_X && position.z >= MIN_Z && position.z <= MAX_Z;
    }

    private static bool BlocksContain(Vector3 position)
    {
        foreach (TerrainBlock block in blocks)
            if (block.collider.bounds.Contains(position))
                return true;
        return false;
    }

    private void Generate(Vector3 offset, int size)
    {
        Vector3 position = transform.position + offset;
        Debug.Log(position + " in bounds: " + InBounds(position) + "  already contained: " + BlocksContain(position));
        if (InBounds(position) && !BlocksContain(position))
        {
            GameObject go = Instantiate(gameObject, position, Quaternion.identity);
            TerrainBlock block = go.GetComponent<TerrainBlock>();
            block.SetCubeSize(size);
        }
    }

    private void SetCubeSize(float size)
    {
        if (cubeSize != lastCubeSize)
        {
            lastCubeSize = cubeSize;
            transform.localScale = new Vector3(size, size / 4, size);
            renderer.sharedMaterial.mainTextureScale = new Vector2(size, size);
        }
    }
}
