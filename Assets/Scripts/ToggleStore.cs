using UnityEngine;
using UnityEngine.UI;

public class ToggleStore : MonoBehaviour
{
    private bool isOpen;
    private Vector3 openPosition = new Vector3(-284f, 184f, 0f);
    private Vector3 closedPosition = new Vector3(-634f, 184f, 0f);
    private float smoothSpeed = 5f;

    public RawImage arrowImage;
    private Quaternion startRotation;
    private Quaternion endRotation;

    private void Start()
    {
        transform.localPosition = closedPosition;
        isOpen = false;

        // Store the initial rotation of the arrowImage
        startRotation = arrowImage.rectTransform.localRotation;
        // Calculate the end rotation (180 degrees around the Z-axis)
        endRotation = startRotation * Quaternion.Euler(0, 0, 180);
    }

    private void Update()
    {
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, smoothSpeed * Time.deltaTime);

        // Interpolate the rotation
        arrowImage.rectTransform.localRotation = Quaternion.Lerp(arrowImage.rectTransform.localRotation,
            isOpen ? endRotation : startRotation,
            smoothSpeed * Time.deltaTime);
    }

    public void toggleStore()
    {
        isOpen = !isOpen;
    }
}