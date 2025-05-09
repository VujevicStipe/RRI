using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float acceleration = 30f;  
    public float maxSpeed = 50f;       
    public float turnSpeed = 20f;     
    public float waterDrag = 2f;      

    public float waveHeight = 0.5f;    
    public float waveFrequency = 1f; 

    private Rigidbody rb;
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position; 

        rb.linearDamping = 1f;
        rb.angularDamping = 2f;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        ApplyDrag();
        SimulateWaves(); 

        Vector3 position = rb.position;
        position.y = Mathf.Max(position.y, initialPosition.y - 0.5f);
        rb.MovePosition(position);
    }

    private void HandleInput()
    {
        // Acceleration and deceleration
        float moveInput = Input.GetAxis("Vertical");
        Vector3 forwardForce = transform.forward * moveInput * acceleration;
        rb.AddForce(forwardForce, ForceMode.Acceleration);

        // Steering
        float turnInput = Input.GetAxis("Horizontal");
        float turn = turnInput * turnSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));

        // Clamp speed
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    private void ApplyDrag()
    {
        Vector3 dragForce = -rb.linearVelocity.normalized * waterDrag * rb.linearVelocity.sqrMagnitude * Time.fixedDeltaTime;
        rb.AddForce(dragForce, ForceMode.Force);
    }

    private void SimulateWaves()
    {
        float waveOffset = Mathf.Sin(Time.time * waveFrequency) * waveHeight;

        Vector3 wavePosition = rb.position;
        wavePosition.y = initialPosition.y + waveOffset; 
        rb.MovePosition(wavePosition);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Vector3 velocity = rb.linearVelocity;
            if (velocity.y < 0)
            {
                velocity.y = 0;
                rb.linearVelocity = velocity;
            }

            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
