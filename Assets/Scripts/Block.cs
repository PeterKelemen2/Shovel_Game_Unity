using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] int health;

    void Start()
    {   

        switch (gameObject.tag){
            case "Dirt":
                health = 2;
                Debug.Log("Dirt block found!");
                break;
            case "Stone":
                health = 5;
                Debug.Log("Stone block found!");
                break;
            default:
                break;
        }
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        health--;
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    
}
