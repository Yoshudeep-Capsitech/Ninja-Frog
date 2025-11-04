using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.56f;
    public float jumpForce = 10.09f;

    private Rigidbody2D rb;
    private Animator anim; // Controls animations
    private bool isGrounded;

    // Ground check settings
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float groundCheckRadius = 0.2f;

    private float moveInput;
    private bool jumpRequested;

    private bool isDead = false;

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
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); 
        if (TouchInputManager.instance != null && Mathf.Abs(TouchInputManager.instance.HorizontalInput) > 0.1f)
        {
            moveInput = TouchInputManager.instance.HorizontalInput;
        }
        anim.SetBool("isRunning", moveInput != 0);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);


        bool keyboardJump = Input.GetButtonDown("Jump"); 
        
        bool touchJump = false;
        if (TouchInputManager.instance != null)
        {
            touchJump = TouchInputManager.instance.GetJumpInput();
        }

        if ((keyboardJump || touchJump) && isGrounded) 
        {
            AudioManager.instance.PlayJumpSound(); 
            jumpRequested = true;
            anim.SetTrigger("doJump");
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Apply jump force
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
        AudioManager.instance.PlaySpikeDeadSound();
        Debug.Log("Player hit a trap!");
        GameManager.instance.HandlePlayerDeath();
        gameObject.SetActive(false);
    }
}