using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class ShovelController : MonoBehaviour
{
    private float upForce = 5f;
    private float moveSpeed = 7f;
    private float horizontalInput;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        /*
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 oldVelocity = rb.velocity;
            rb.velocity = (Vector2)new Vector3(1 * moveSpeed, oldVelocity.y, oldVelocity.z);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 oldVelocity = rb.velocity;
            rb.velocity = (Vector2)new Vector3(-1 * moveSpeed, oldVelocity.y, oldVelocity.z);
        }

        */
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector3.left * (moveSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.D))
        {
            // transform.Translate(Vector3.right * (moveSpeed * Time.deltaTime));
        }
    }

    private bool _canTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!_canTrigger) return;
        Debug.Log("Collided");

        _canTrigger = false;
        StartCoroutine(TriggerAfterDelay());

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
    }

    private void MovePlayer()
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

    private IEnumerator TriggerAfterDelay()
    {
        // This is used so it doesn't add additional force when two blocks are hit
        yield return new WaitForSeconds(0.1f);

        // Re-enable the trigger
        _canTrigger = true;
    }
}