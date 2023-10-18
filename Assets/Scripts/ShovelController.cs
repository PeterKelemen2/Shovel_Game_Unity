using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class ShovelController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float upForce = 1f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            // Debug.Log("Moved left");
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            // Debug.Log("Moved right");
        }
    }

    private bool canTrigger = true;
    public void OnTriggerEnter(Collider other)
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

    private IEnumerator TriggerAfterDelay()
    {
        // This is used so it doesn't add additional force when two blocks are hit
        yield return new WaitForSeconds(0.1f);

        // Re-enable the trigger
        canTrigger = true;
    }
}
