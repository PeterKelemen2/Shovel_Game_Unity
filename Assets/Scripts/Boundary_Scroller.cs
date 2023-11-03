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
    }

    void Update()
    {
        float newYOffset = transform.position.y * offsetSpeed;
        offset.y = newYOffset;
        material.SetTextureOffset("_MainTex", offset);
    }
}