using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using Random = System.Random;
using Vector3 = UnityEngine.Vector3;

public class ShovelController : MonoBehaviour
{
    //public GameObject prefab;
    private bool isPlaying = true;
    private float upForce = 5f;
    private float moveSpeed = 7f;
    private float horizontalInput;
    private Rigidbody rb;
    // public List<Material> materialList = new();

    private Dictionary<String, Material> materialDictionary = new();

    // public GameObject prefab;
    // private GameObject instantiatedObject;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        audioClips["Jump1"] = Resources.Load<AudioClip>("Audio/DM-CGS-21");
        audioClips["Jump2"] = Resources.Load<AudioClip>("Audio/DM-CGS-32");

        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        setUpMaterialDict();
    }

    public void setShovelMaterial(String material)
    {
        MeshRenderer rend = GetComponentInChildren<MeshRenderer>();
        var materialsCopy = rend.materials;
        switch (material)
        {
            case "Red":
                materialsCopy[1] = materialDictionary["Red"];
                break;
            case "Blue":
                materialsCopy[1] = materialDictionary["Blue"];
                break;
            case "Green":
                materialsCopy[1] = materialDictionary["Green"];
                break;
            case "Yellow":
                materialsCopy[1] = materialDictionary["Yellow"];
                break;
            default:
                break;
        }

        rend.materials = materialsCopy;
    }

    private void setUpMaterialDict()
    {
        materialDictionary["Blue"] = Resources.Load<Material>("Shovel_Blue");
        materialDictionary["Red"] = Resources.Load<Material>("Shovel_Red");
        materialDictionary["Green"] = Resources.Load<Material>("Shovel_Green");
        materialDictionary["Yellow"] = Resources.Load<Material>("Shovel_Yellow");
    }

    void FixedUpdate()
    {
        MovePlayer();
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private bool _canTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("MainCamera"))
        {
            if (!_canTrigger) return;
            //Debug.Log("Collided");

            _canTrigger = false;
            StartCoroutine(TriggerAfterDelay());

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if (isPlaying)
            {
                rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
                playJumpSound();
            }
        }
    }

    private void playJumpSound()
    {
        System.Random rnd = new System.Random();
        int randomNumber = rnd.Next();
        bool myBool = false;
        if (randomNumber % 2 == 0)
        {
            myBool = true;
        }
        else
        {
            myBool = false;
        }

        switch (myBool)
        {
            case true:
                playSound("Jump1");
                break;
            case false:
                playSound("Jump2");
                break;
        }
    }

    public void playSound(String sound)
    {
        audioSource.clip = audioClips[sound];
        audioSource.Play();
    }

    private Dictionary<String, AudioClip> audioClips = new();


    public void setPlayingStatus(bool status)
    {
        if (!status)
        {
            isPlaying = false;
        }
        else
        {
            isPlaying = true;
        }
    }

    public void movePlayerLeft()
    {
        if (isPlaying)
        {
            var moveDirection = new Vector3(horizontalInput, 0, 0);
            if (!Physics.Raycast(transform.position, moveDirection, out _, 0.2f))
            {
                var velocity = rb.velocity;
                velocity = new Vector3(moveDirection.x * moveSpeed,
                    velocity.y, velocity.z);
                rb.velocity = velocity;
            }
        }
    }
    
    private void MovePlayer()
    {
        if (isPlaying)
        {
            var moveDirection = new Vector3(horizontalInput, 0, 0);

            if (!Physics.Raycast(transform.position, moveDirection, out _, 0.2f))
            {
                // If no obstacles are in the way, move the object.
                var velocity = rb.velocity;
                velocity = new Vector3(moveDirection.x * moveSpeed,
                    velocity.y, velocity.z);
                rb.velocity = velocity;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
        }
    }

    private IEnumerator TriggerAfterDelay()
    {
        // This is used so it doesn't add additional force when two blocks are hit
        yield return new WaitForSeconds(0.1f);

        // Re-enable the trigger
        _canTrigger = true;
    }
}