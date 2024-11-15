using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;         // The character the camera will follow
    public float distance = 8.0f;    // Initial distance from the character
    public float minDistance = 2.0f; // Minimum zoom distance
    public float maxDistance = 10.0f; // Maximum zoom distance
    public float zoomSpeed = 2.0f;   // Speed of zooming

    public float mouseSensitivity = 3.0f;
    public float yMinLimit = -20f; // Minimum vertical angle
    public float yMaxLimit = 80f;  // Maximum vertical angle

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    public bool bIsPlaying=false;


    void Start()
    {
        // Initialize camera rotation based on current position
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;

        // Lock cursor to the game window (optional)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if(!bIsPlaying){
            return;
        }
        // Handle camera rotation when the right mouse button is held down
        if (Input.GetMouseButton(1)) // Right-click
        {
            // Hide the cursor while rotating
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Update camera rotation based on mouse input
            rotationX += mouseX * mouseSensitivity;
            rotationY -= mouseY * mouseSensitivity;

            // Clamp the vertical rotation
            rotationY = Mathf.Clamp(rotationY, yMinLimit, yMaxLimit);
        }
        else
        {
            // Release cursor lock when right-click is not pressed
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Zoom in/out with the mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        distance -= scrollInput * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Calculate the new position and rotation of the camera
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * distance);

        // Apply the rotation and position
        transform.rotation = rotation;
        transform.position = position;
    }
    public void CameraLaunch(){
        target=GameObject.FindGameObjectsWithTag("Player")[0].transform;
        bIsPlaying=true;
    }
}