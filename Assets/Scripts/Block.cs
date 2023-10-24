using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int health;
    public TextMeshPro hpText;
    private ParticleSystem particle;
    private Renderer[] rendArray;
    private Renderer rendSingle;
    private BoxCollider boxCollider;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    void Start()
    {
        switch (gameObject.tag)
        {
            case "Dirt":
                health = 2;
                setHPText();
                rendArray = GetComponentsInChildren<Renderer>();
                Debug.Log("Dirt block found!");
                break;
            case "Stone":
                health = 5;
                setHPText();
                rendSingle = GetComponentInChildren<Renderer>();
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
            //Destroy(gameObject);
            StartCoroutine(breakBlock());
        }
    }

    private IEnumerator breakBlock()
    {
        particle.Play();
        boxCollider.enabled = false;
        hpText.SetText("");

        if (rendSingle)
        {
            rendSingle.enabled = false;
        }
        else if (rendArray.Length > 0)
        {
            // The last child was the particle system, which destroyed that too
            for (int i = 0; i < rendArray.Length - 1; i++)
            {
                rendArray[i].enabled = false;
            }
        }

        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}