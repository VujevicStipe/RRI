using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public Transform firstPersonPosition;   
    public Transform thirdPersonPosition;  
    public Transform cameraTransform;    

    private bool isFirstPerson = true;

    void Start()
    {
        SwitchCameraPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
        }

        Transform target = isFirstPerson ? firstPersonPosition : thirdPersonPosition;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, target.position, Time.deltaTime * 5f);
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, target.rotation, Time.deltaTime * 5f);
    }

    void SwitchCameraPosition()
    {
        if (isFirstPerson)
        {
            cameraTransform.position = firstPersonPosition.position;
            cameraTransform.rotation = firstPersonPosition.rotation;
        }
        else
        {
            cameraTransform.position = thirdPersonPosition.position;
            cameraTransform.rotation = thirdPersonPosition.rotation;
        }
    }
}
