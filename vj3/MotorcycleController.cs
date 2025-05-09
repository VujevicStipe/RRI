using System.Diagnostics;
using UnityEngine;

public class MotorController : MonoBehaviour
{
    public float acceleration = 20f;  
    public float brakingPower = 30f;  
    public float maxSpeed = 30f;      
    public float reverseSpeed = 10f;  
    public float turnSpeed = 50f;

    public float handbrakePower = 50f;

    public Rigidbody rb;
    private float currentSpeed = 0f;

    private Quaternion initialRotation;
    public Transform modelTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (modelTransform != null)
        {
            initialRotation = modelTransform.localRotation;  
        }
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");
        bool isHandbrakeActive = Input.GetKey(KeyCode.Space);

        if (isHandbrakeActive)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.fixedDeltaTime * (handbrakePower / 10f));
        }

        if (moveInput > 0)
        {
            currentSpeed += moveInput * acceleration * Time.fixedDeltaTime;
        }
        else if (moveInput < 0)
        {
            currentSpeed += moveInput * brakingPower * Time.fixedDeltaTime;
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.fixedDeltaTime * 2);
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -reverseSpeed, maxSpeed);

        Vector3 velocity = transform.forward * currentSpeed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

        float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
        if (currentSpeed < 0)
        {
            turn = -turn;
        }
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));

        if (modelTransform != null)
        {
            float leanAmount = turnInput * Mathf.Clamp01(currentSpeed / maxSpeed) * 45f;
            Quaternion targetRotation = Quaternion.Euler(leanAmount, 0f, 0f);
            modelTransform.localRotation = Quaternion.Slerp(modelTransform.localRotation, initialRotation * targetRotation, Time.fixedDeltaTime * 5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.Log("Sudar sa: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            UnityEngine.Debug.Log("aaaaaaa sa: " + collision.gameObject.name);
            Vector3 collisionForce = collision.impulse;

            if (Mathf.Abs(collisionForce.x) < 1f && Mathf.Abs(collisionForce.z) < 1f)
            {
                collisionForce += new Vector3(0, 0, 5f);
            }

            rb.AddTorque(new Vector3(collisionForce.z, 0, -collisionForce.x) * 10f, ForceMode.Impulse);
        }
    }
}

