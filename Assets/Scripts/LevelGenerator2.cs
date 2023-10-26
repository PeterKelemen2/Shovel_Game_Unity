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
    public int shovelDMG = 1;

    /*
     Blocks are spawned apart from each other by '1'
     This being on the X and Y axis
     */
    private int startSpawnX = -2;

    private int startSpawnY = 0;

    private int hideOnRow = 0;
    //private int spawnOffsetX = 0;
    //private int spawnOffsetY = 0;

    // ReSharper disable Unity.PerformanceAnalysis
    int pickBlock()
    {
        var rnd = Random.Range(0, blocks.Length);
        //Debug.Log("Picked block with ID=" + rnd);
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


    
    private void placeBlock(float x, float y)
    {
        GameObject blockObject = Instantiate(blocks[pickBlock()],
            new Vector3(x, y, 0f),
            Quaternion.identity);
        SpawnedObjects.Add(blockObject.gameObject);
        //Debug.Log("SP count after adding to it: " + SpawnedObjects.Count);
        lastItemIndex = SpawnedObjects.Count - 1;
    }

    private void generateTopLayer()
    {
        for (int i = 0; i < 5; i++)
        {
            placeBlock(startSpawnX + i, startSpawnY);
        }

        //Debug.Log("SP Count after generating top layer: " + SpawnedObjects.Count);
    }

    // camera Y = 1
    // last block Y = -2
    //
    // ReSharper disable Unity.PerformanceAnalysis
    private void generateLevel()
    {
        cameraY = camera.transform.position.y;
        if (Math.Abs(SpawnedObjects[SpawnedObjects.Count - 1].transform.position.y) - 
            Math.Abs(cameraY) < 4f)
        {
            startSpawnY--;
            for (int i = 0; i < 5; i++)
            {
                placeBlock(startSpawnX + i, startSpawnY);
            }

            //Debug.Log("SP Count after spawning a row: " + SpawnedObjects.Count);
        }

        if (Math.Abs(cameraY) - Math.Abs(SpawnedObjects[hideOnRow].transform.position.y) > 6f)
        {
            // for (int i = 0; i < 5; i++)
            // {
            //     Destroy(SpawnedObjects[i]); // + hideOnRow
            //     SpawnedObjects.RemoveAt(i); // + hideOnRow
            // }
            //
            // Debug.Log("SpawnedObjects count after removal: " + SpawnedObjects.Count);
            //
            // //hideOnRow += 5;
            destroyFirstRow();
        }
    }

    private void destroyFirstRow()
    {
        for (int i = 0; i < 5; i++)
        {
            Destroy(SpawnedObjects[0]);
            SpawnedObjects.RemoveAt(0);
        }
        //Debug.Log("Removed first unseen row");
    }

    public void setDamageTakenOfBlocks()
    {
        foreach (GameObject blockObject in SpawnedObjects)
        {
            // If run first time, all the blocks, else only the last 5
        }
    }

    public void receiveDamageValueFromUI(int dmg)
    {
        shovelDMG = dmg;
        Debug.Log("Damage value received from UI Controller: " + shovelDMG);
        setDamageTakenOfBlocks();
    }
}