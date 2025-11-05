using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5.56f;
    public float jumpForce = 10.09f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private float moveInput;
    private bool jumpRequested;
    private bool isDead = false;

    private TouchInputManager inputManager; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (groundCheck == null)
        {
            GameObject check = new GameObject("GroundCheck");
            check.transform.SetParent(this.transform);
            check.transform.localPosition = new Vector3(0, -0.15f, 0);
            groundCheck = check.transform;
        }

        // Try to find the TouchInputManager
        inputManager = FindFirstObjectByType<TouchInputManager>();
    }

    void Update()
    {
        if (inputManager == null)
            inputManager = FindFirstObjectByType<TouchInputManager>();

        // --- Horizontal Input ---
        moveInput = 0f;

        // Keyboard movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            moveInput = -1f;
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            moveInput = 1f;

        // Touch/Android movement
        if (inputManager != null)
        {
            if (inputManager.moveLeft) moveInput = -1f;
            if (inputManager.moveRight) moveInput = 1f;
        }

        // --- Jump Input ---
        bool keyboardJump = Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump");
        bool touchJump = (inputManager != null && inputManager.jumpPressed);

        if ((keyboardJump || touchJump) && isGrounded)
        {
            AudioManager.instance?.PlayJumpSound();
            jumpRequested = true;
            anim.SetTrigger("doJump");
        }

        // --- Animation parameters ---
        anim.SetBool("isRunning", moveInput != 0);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Apply jump
        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap") && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        AudioManager.instance?.PlaySpikeDeadSound();
        Debug.Log("Player hit a trap!");
        GameManager.instance?.HandlePlayerDeath();
        gameObject.SetActive(false);
    }
}
