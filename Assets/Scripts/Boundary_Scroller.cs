using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary_Scroller : MonoBehaviour
{
    private Renderer rend;
    private Material boundaryMaterial;
    private Vector2 offset;
    private Vector2 uvScale = new(1f, 20f);
    
    private float timeOffset = 0.0f; // Additional time offset for UV scrolling

    public float scrollSpeed = 4.0f; // Adjust this value to control the scrolling speed.

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        boundaryMaterial = rend.material;
        boundaryMaterial.mainTextureScale = uvScale;
        offset.y = transform.position.y;
        boundaryMaterial.mainTextureOffset = offset;
    }

    void Update()
    {
        offset.y = transform.position.y + (Time.time * 0.4f);
        boundaryMaterial.mainTextureOffset = offset;
    }
}