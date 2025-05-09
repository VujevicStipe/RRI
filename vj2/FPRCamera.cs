using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerBody; 
    public float mouseSensitivity = 100f;

    private float xRotation = 0f;

    void Update()
    {
        if (playerBody == null)
        {
            Debug.LogError("playerBody nije postavljen u Inspectoru!");
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX); 
    }
}
