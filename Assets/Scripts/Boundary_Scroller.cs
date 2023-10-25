using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary_Scroller : MonoBehaviour
{
    [SerializeField] public Vector2 uvOffset = new Vector2(1f, 2f);
    //[SerializeField] public Vector2 uvScale = new Vector2(1.0f, 1.0f);

    private Renderer rend;
    private Material material;
    private float offsetSpeed = 1f;
    private Vector2 offset;
    void Start()
    {
        material = GetComponentInChildren<Renderer>().material;
        offset = material.GetTextureOffset("_MainTex");
        // material.mainTextureScale = uvScale;
        //material.mainTextureOffset = new Vector2(0f, 0f);
    }

    void Update()
    {
        /*
        Vector2 currentOffset = rend.material.mainTextureOffset;
        float time = Time.deltaTime;
        Vector2 newOffset = new Vector2(0 * time, currentOffset.y + 3f);

        // Update the material's mainTextureOffset.
        rend.material.mainTextureOffset = newOffset;
        */

        float newYOffset = transform.position.y * offsetSpeed;
        offset.y = newYOffset;
        material.SetTextureOffset("_MainTex", offset);
    }
}