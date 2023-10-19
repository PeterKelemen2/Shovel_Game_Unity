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
    [SerializeField] float upForce = 40f;
    [SerializeField] float moveSpeed = 0.05f;
    private float horizontalInput;
    Rigidbody rb;
    

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

    }

    private bool canTrigger = true;
    private void OnTriggerEnter(Collider other)
    {

        if (canTrigger)
        {
            Debug.Log("Collided");

            canTrigger = false;
            StartCoroutine(TriggerAfterDelay());

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        }

    }

    void MovePlayer()
    {
        Vector3 moveDirection = new Vector3(horizontalInput, 0, 0);

        RaycastHit hit;
        if (!Physics.Raycast(transform.position, moveDirection, out hit, 0.2f))
        {
            // If no obstacles are in the way, move the object.
            rb.velocity = new Vector3(moveDirection.x * moveSpeed, 
                rb.velocity.y, rb.velocity.z);
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
        canTrigger = true;
    }

}
