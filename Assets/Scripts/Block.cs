using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] int health;
    public TextMeshPro hpText;


    void Start()
    {   

        switch (gameObject.tag){
            case "Dirt":
                health = 2;
                setHPText();
                Debug.Log("Dirt block found!");
                break;
            case "Stone":
                health = 5;
                setHPText();
                Debug.Log("Stone block found!");
                break;
            default:
                break;
        }
    }

    void Update()
    {
        
    }


    private void setHPText()
    {
        hpText.SetText("HP: " + health.ToString());
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Block Trigger");
        health--;
        Debug.Log("HP-");
        setHPText();
        if (health == 0)
        {
            Destroy(gameObject);
        }

    }

}
