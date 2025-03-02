using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target; // The player
    public float followSpeed = 5f; // How smoothly the camera follows the player
    public Vector3 offset = new Vector3(0, 10, -5); // Adjust for desired top-down view
    public bool allowRotation = false; // Toggle for camera rotation
    public float rotationSpeed = 100f; // Speed of rotation

    void LateUpdate()
    {
        if (target == null) return;

        // Smoothly move the camera towards the target position
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Rotate camera based on input (Optional)
        if (allowRotation)
        {
            float rotationInput = Input.GetAxis("Horizontal"); // Example: Rotate with A/D or Arrow Keys
            transform.RotateAround(target.position, Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
        }
    }
}