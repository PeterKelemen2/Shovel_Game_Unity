using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] public Vector2 uvOffset = new Vector2(1f, 2f);
    [SerializeField] public Vector2 uvScale = new Vector2(16f, 160f);

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        var material = rend.material;
        material.mainTextureScale = uvScale;
        material.mainTextureOffset = uvOffset;
    }

    void Update()
    {
        Vector2 currentOffset = rend.material.mainTextureOffset;
        float time = Time.deltaTime;
        Vector2 newOffset = new Vector2(0 * time, currentOffset.y + 0.1f * time);

        // Update the material's mainTextureOffset.
        rend.material.mainTextureOffset = newOffset;
    }
}