using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float max_Thrust = 100f;
    public float thrust = 0f;
    public float acceleration = 5f;
    public float deceleration = 5f;

    public float rollSpeed = 30f;  
    public float pitchSpeed = 1000f; 
    public float yawSpeed = 15f;   

    public float takeoffSpeed = 50f;
    public float landingSpeed = 20f;
    public float liftForce = 10f;
    public float maxPitchAngle = 15f;

    private Rigidbody rb;
    private bool isAirborne = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleThrustInput();
        ApplyThrust();
        HandleRotationInput();
        SimulateTakeoffAndLanding();
    }

    private void HandleThrustInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            thrust += acceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            thrust -= deceleration * Time.deltaTime;
        }

        //da vrijednost ne prelazi 100
        thrust = Mathf.Clamp(thrust, 0, max_Thrust);
    }

    private void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(transform.forward * rollSpeed * Time.deltaTime, ForceMode.Acceleration);
            UnityEngine.Debug.Log("Applying roll left force");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-transform.forward * rollSpeed * Time.deltaTime, ForceMode.Acceleration);
            UnityEngine.Debug.Log("Applying roll right force");
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddTorque(transform.right * pitchSpeed * Time.deltaTime, ForceMode.Acceleration); 
            UnityEngine.Debug.Log("Applying pitch down force");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddTorque(-transform.right * pitchSpeed * Time.deltaTime, ForceMode.Acceleration);
            UnityEngine.Debug.Log("Applying pitch up force");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(-transform.up * yawSpeed * Time.deltaTime, ForceMode.Acceleration); 
            UnityEngine.Debug.Log("Applying yaw left force");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(transform.up * yawSpeed * Time.deltaTime, ForceMode.Acceleration); 
            UnityEngine.Debug.Log("Applying yaw right force");
        }
    }

    private void ApplyThrust()
    {
        Vector3 forward_Force = transform.forward * thrust;
        rb.AddForce(forward_Force, ForceMode.Acceleration);
    }

    private void SimulateTakeoffAndLanding()
    {
        float speed = rb.linearVelocity.magnitude;

        float pitchAngle = Vector3.Angle(transform.forward, Vector3.up);
        UnityEngine.Debug.Log($"Speed: {speed}, Pitch Angle: {pitchAngle}, Is Airborne: {isAirborne}");

        if (!isAirborne)
        {
            if (speed > takeoffSpeed && pitchAngle > 5f && pitchAngle < maxPitchAngle)
            {
                isAirborne = true;
                UnityEngine.Debug.Log("Takeoff!");
            }
        }
        else
        {
            rb.AddForce(Vector3.up * liftForce, ForceMode.Acceleration);
            if (speed < landingSpeed && Mathf.Abs(pitchAngle) < 5f)
            {
                isAirborne = false;
                UnityEngine.Debug.Log("Landing!");
            }
        }
    }
}

