using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator2 : MonoBehaviour
{
    public GameObject[] blocks;
    public GameObject shovel;
    public GameObject camera;
    private float cameraY;
    private List<GameObject> SpawnedObjects = new List<GameObject>();

    private bool isGenerated = false;

    /*
     Blocks are spawned apart from each other by '1'
     This being on the X and Y axis
     */
    private int startSpawnX = -2;
    private int startSpawnY = 2;
    private int spawnOffsetX = 0;
    private int spawnOffsetY = 0;

    // ReSharper disable Unity.PerformanceAnalysis
    int pickBlock()
    {
        var rnd = Random.Range(0, blocks.Length);
        Debug.Log("Placing block with ID " + rnd);
        return rnd;
    }

    void Start()
    {
        cameraY = camera.transform.position.y;
        generateTopLayer();
    }


    void Update()
    {
        generateLevel();
    }


    void placeBlock(float x, float y)
    {
        GameObject blockObject = Instantiate(blocks[pickBlock()],
            new Vector3(x, y, 0f),
            Quaternion.identity);
        SpawnedObjects.Add(blockObject.gameObject);
    }

    private void generateTopLayer()
    {
        for (int i = 0; i < 5; i++)
        {
            placeBlock(-2 + i, 2);
        }
    }

    private void generateLevel()
    {
        if (SpawnedObjects[SpawnedObjects.Count - 1].transform.position.y > cameraY - 3f)
        {
            spawnOffsetY--;
            for (int i = 0; i < 5; i++)
            {
                placeBlock(startSpawnX + i, spawnOffsetY);
            }
        }
    }
}