using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager instance;
    private List<Block> blockObjects = new List<Block>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void registerBlock(Block block)
    {
        blockObjects.Add(block);
    }

    public void unregisterBlock(Block block)
    {
        blockObjects.Remove(block);
    }

    public void SendDisableSignal()
    {
        foreach (Block block in blockObjects)
        {
            if (!block.gameObject.activeSelf)
            {
                //block.SendDisableSignalToUIController();
            }
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
