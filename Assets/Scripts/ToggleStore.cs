using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToggleStore : MonoBehaviour
{
    private bool isOpen;
    private Vector3 openPosition = new(-284f, 184f, 0f);
    private Vector3 closedPosition = new(-634f, 184f, 0f);
    private float smoothSpeed = 5f;

    private void Start()
    {
        transform.localPosition = closedPosition;
        isOpen = false;
    }

    private void Update()
    {
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition,
            targetPosition,
            smoothSpeed * Time.deltaTime);
    }

    public void toggleStore()
    {
        isOpen = !isOpen;
    }
}