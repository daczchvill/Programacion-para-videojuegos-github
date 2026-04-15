using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float rotationSpeed = 200f;

    private Rigidbody rb;
    private bool isGrounded;

    private Animator animator;

    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Guardar posición inicial
        startPosition = transform.position;
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveForward = Input.GetAxis("Vertical");   // W/S
        float rotate = Input.GetAxis("Horizontal");      // A/D

        // 🔁 Rotación sobre eje Y
        transform.Rotate(Vector3.up * rotate * rotationSpeed * Time.deltaTime);

        // ⏩ Movimiento hacia adelante según dirección
        Vector3 movement = transform.forward * moveForward * moveSpeed;

        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            ResetPlayer();
        }
    }

    void ResetPlayer()
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = startPosition;
    }
}