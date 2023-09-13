using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    

    public GameObject block;
    private int[][] blockArray;
    //private int spawnOffsetX = 1;
    //private int spawnOffsetY = 1;

    void Start()
    {
       //generateLevel();
    }

    // 0 0.5 1
    // Unoptimized - Tris: 14.0k    Verts: 15.6k - for 15 Blocks - 248k, 215k
    // Optimized   - Tris: 6.4k    Verts: 11.7k  - for 15 Blocks - 95k, 137k


    void Update()
    {
        
    }

    void generateLevel()
    {
        /*
        Spawning starts from -2,2,0
        On the first row there are 5 Blocks
         */

        Debug.Log("Generating Level...");
        for(float i = -2; i <= 2; i++)
        {
            for(float j = 2; j > -2; j--)
            {
                GameObject gameObject = Instantiate(block,
                new Vector3(i, j, 0f),
                Quaternion.identity);
            }
        }
    }
}
