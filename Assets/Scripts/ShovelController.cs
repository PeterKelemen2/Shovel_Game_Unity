using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
            Debug.Log("Moved left");
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            Debug.Log("Moved right");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
    }
}
