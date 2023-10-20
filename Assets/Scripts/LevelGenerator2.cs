using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class LevelGenerator2 : MonoBehaviour
{
    public GameObject[] blocks;
    public GameObject shovel;
    public GameObject camera;
    private float cameraY;
    private List<GameObject> SpawnedObjects = new List<GameObject>();
    private int lastItemIndex;


    /*
     Blocks are spawned apart from each other by '1'
     This being on the X and Y axis
     */
    private int startSpawnX = -2;

    private int startSpawnY = 0;
    //private int spawnOffsetX = 0;
    //private int spawnOffsetY = 0;

    // ReSharper disable Unity.PerformanceAnalysis
    int pickBlock()
    {
        var rnd = Random.Range(0, blocks.Length);
        Debug.Log("Placing block with ID=" + rnd);
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
        lastItemIndex = SpawnedObjects.Count - 1;
    }

    private void generateTopLayer()
    {
        for (int i = 0; i < 5; i++)
        {
            placeBlock(startSpawnX + i, startSpawnY);
        }
    }

    // camera Y = 1
    // last block Y = -2
    //
    private void generateLevel()
    {
        cameraY = camera.transform.position.y;
        if (Math.Abs(SpawnedObjects[lastItemIndex].transform.position.y) - Math.Abs(cameraY) < 4f)
        {
            startSpawnY--;
            for (int i = 0; i < 5; i++)
            {
                placeBlock(startSpawnX + i, startSpawnY);
            }
        }
        
        // TODO: Destroy objects that are not visible anymore
    }
}