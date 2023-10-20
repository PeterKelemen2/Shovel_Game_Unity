using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public GameObject player; // Reference to the player's Transform
    private float smoothSpeed = 2f; // Adjust the smoothness of the camera follow
    private bool _isplayerNotNull;

    void Start()
    {
        _isplayerNotNull = player != null;
    }

    void Update()
    {
        if (_isplayerNotNull)
        {
            //Debug.Log("Player attached");
            // Get the player's position


            float targetY = (player.transform.position.y * 0.8f) - 2f;

            //float playerX = player.transform.position.x;
            //float playerZ = player.transform.position.z;

            Vector3 targetPosition = new Vector3(0f, targetY, -6.5f);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition,
                smoothSpeed * Time.deltaTime);

            // Set the camera's position to match the player's position with the Z-axis offset
            transform.position = smoothedPosition;
            // Debug.Log("Camera moved to" + targetPosition);
        }
    }
}