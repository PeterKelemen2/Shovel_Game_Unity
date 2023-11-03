using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Playables;

public class Block : MonoBehaviour
{
    [SerializeField] int health;
    public int blockValue = 0;
    public int damageTaken = 1;
    public TextMeshPro hpText;
    public TextMeshPro pointsText;
    private ParticleSystem particle;
    private Renderer[] rendArray;
    private List<Renderer> rendererList = new List<Renderer>();
    private Renderer rendSingle;
    private BoxCollider boxCollider;

    private Animator animator;
    private Animation anim;


    private void Awake()
    {
        //particle = GetComponentInChildren<ParticleSystem>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }


    void Start()
    {
        switch (gameObject.tag)
        {
            case "Dirt":
                health = 2;
                blockValue = 2;
                setHPText();
                //rendArray = GetComponentsInChildren<Renderer>();
                //Debug.Log("Dirt block found!");
                break;
            case "Stone":
                health = 5;
                blockValue = 5;
                setHPText();
                //rendSingle = GetComponentInChildren<Renderer>();
                //Debug.Log("Stone block found!");
                break;
            default:
                break;
        }

        setRenderer();
        setParticles();
        setAnimatorAndAnimation();
        pointsText.SetText("");
    }


    private void setRenderer()
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child.CompareTag("Block_Piece"))
            {
                Renderer renderer = child.GetComponent<Renderer>();

                if (renderer != null)
                {
                    rendererList.Add(renderer);
                }
            }
        }
    }

    private void setAnimatorAndAnimation()
    {
        animator = GetComponentInChildren<Animator>();
        Debug.Log(animator);
    }

    private void setParticles()
    {
        Transform childWithParticles = transform.Find("Particle System");
        if (childWithParticles != null)
        {
            particle = childWithParticles.GetComponent<ParticleSystem>();
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
        //Debug.Log("Entered Block Trigger");
        health -= damageTaken;
        //Debug.Log("HP-");
        setHPText();
        if (health <= 0)
        {
            //Destroy(gameObject);
            StartCoroutine(breakBlock());
        }
    }


    // ReSharper disable Unity.PerformanceAnalysis
    private void sendBlockValue()
    {
        //BlockManager.instance.SendDisableSignal();
        UIController uiController = FindObjectOfType<UIController>();
        if (uiController != null)
        {
            uiController.receiveBlockValue(blockValue);
        }
    }


    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator breakBlock()
    {
        pointsText.SetText("+" + blockValue + "$");
        animator.SetTrigger("PointsTrigger");
        

        particle.Play();
        boxCollider.enabled = false;
        hpText.SetText("");

        foreach (Renderer rend in rendererList)
        {
            rend.enabled = false;
        }

        sendBlockValue();

        //particle.main.startLifetime.constantMax
        yield return new WaitForSeconds(0.7f);
        pointsText.SetText("");
        gameObject.SetActive(false);
    }

    public void receiveDamageTaken(int shovelDmg)
    {
        damageTaken = shovelDmg;
    }
}