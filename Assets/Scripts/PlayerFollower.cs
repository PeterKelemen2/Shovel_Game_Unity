using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class PlayerFollower : MonoBehaviour
{
    //public GameObject player; // Reference to the player's Transform
    public GameObject toFollow;
    public GameObject background;
    private float smoothSpeed = 2f; // Adjust the smoothness of the camera follow
    private bool _isplayerNotNull;
    

    [SerializeField] public Vector2 uvOffset = new Vector2(0f, 2f);
    [SerializeField] public Vector2 uvScale = new Vector2(1f, 1f);

    private Renderer rend;

    void Start()
    {
        // toFollow = Instantiate(player,
        //     new Vector3(0f, 2f, 13f),
        //     Quaternion.identity);

        //toFollow.AddComponent<ShovelController>();

        _isplayerNotNull = toFollow != null;
        rend = background.GetComponent<Renderer>();
        var material = rend.material;
        material.mainTextureScale = uvScale;
        material.mainTextureOffset = uvOffset;
    }

    void Update()
    {
        moveCamera();
    }

    private void moveCamera()
    {
        if (_isplayerNotNull)
        {
            float targetY = (toFollow.transform.position.y) - 2f;

            Vector3 targetPosition = new Vector3(0f, targetY, transform.position.z);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition,
                smoothSpeed * Time.deltaTime);

            Vector2 newOffset = new Vector2(0f, smoothedPosition[1] * 0.07f);
            // Update the material's mainTextureOffset.
            rend.material.mainTextureOffset = newOffset;

            // Set the camera's position to match the player's position with the Z-axis offset
            transform.position = smoothedPosition;
        }
    }
}