using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator2 : MonoBehaviour
{
    public GameObject[] blocks;
    public GameObject shovel;
    private List<GameObject> SpawnedObjects = new List<GameObject>();

    private bool isGenerated = false;
    //private int spawnOffsetX = 1;
    //private int spawnOffsetY = 1;

    int rndOf(int beg, int end)
    {
        int rnd = Random.Range(beg, end);
        Debug.Log("Placing block with ID " + rnd);
        return rnd;
    }

    int pickBlock()
    {
        int rnd = Random.Range(0, blocks.Length);
        Debug.Log("Placing block with ID " + rnd);
        return rnd;
    }

    void Start()
    {
        generateLevel();
        // spawnShovel();
    }

    private void spawnShovel()
    {
        GameObject shovelObject = Instantiate(shovel,
            new Vector3(0f, 4f, 0f),
            Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            generateLevel();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            destroyEverything();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            destroyEverything();
            generateLevel();
            Debug.Log("Regenerated!");
        }
    }

    void destroyEverything()
    {
        while (SpawnedObjects.Count > 0)
        {
            Destroy(SpawnedObjects[0].gameObject);
            SpawnedObjects.RemoveAt(0);
        }

        Debug.Log("Destroyed everything");
        isGenerated = false;
    }

    void placeBlock(float x, float y)
    {
        GameObject blockObject = Instantiate(blocks[pickBlock()],
            new Vector3(x, y, 0f),
            Quaternion.identity);
        SpawnedObjects.Add(blockObject.gameObject);
    }

    void placeTopLayer(float x, float y)
    {
        GameObject blockObject = Instantiate(blocks[0],
            new Vector3(x, y, 0f),
            Quaternion.identity);
        SpawnedObjects.Add(blockObject.gameObject);
    }

    void generateLevel()
    {
        /*
        Spawning starts from -2,2,0
        On the first row there are 5 Blocks
         */
        int nrOfBlocks = new int();
        if (!isGenerated)
        {
            Debug.Log("Generating Level...");
            for (float i = -2; i <= 2; i++)
            {
                for (float j = 2; j > -2; j--)
                {
                    /*
                    if (i == -2)
                    {
                        placeTopLayer(-2, j);
                    }
                    else
                    {
                        placeBlock(i, j);
                    }
                    */
                    placeBlock(i, j);
                    nrOfBlocks++;
                }
            }

            Debug.Log("Generated " + nrOfBlocks + " blocks");
            Debug.Log("Size of SpawnedObjects: " + SpawnedObjects.Count);
            isGenerated = true;
        }
        else
        {
            Debug.Log("Is already generated!");
            Debug.Log("Size of SpawnedObjects: " + SpawnedObjects.Count);
        }
    }
}