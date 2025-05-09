using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float crouchHeight = 1f;
    private float originalHeight;
    public bool isCrouching = false;

    public float jumpHeight = 3f;
    public float gravity = -15f;
    private Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    public bool isJumping = false; 

    void Start()
    {
        walkSpeed = 6f;
        runSpeed = 12f;
        crouchSpeed = 4f;
        originalHeight = controller.height;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumping = false;  
        }

        // Crouch
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            controller.height = isCrouching ? crouchHeight : originalHeight;
        }

        // Trčanje
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching;
        float currentSpeed = walkSpeed;

        if (isRunning)
            currentSpeed = runSpeed;
        else if (isCrouching)
            currentSpeed = crouchSpeed;

        // Kretanje
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Skakanje
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;  
        }

        // Gravitacija
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
