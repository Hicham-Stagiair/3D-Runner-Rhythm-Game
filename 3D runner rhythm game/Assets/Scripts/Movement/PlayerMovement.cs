using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float slideDuration = 1f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckDistance = 0.1f;

    private bool isSliding = false;
    private bool isGrounded = false;
    private Rigidbody rb;
    private BoxCollider collider;
    private GameObject model;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        model = transform.GetChild(0).gameObject;
        collider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isSliding)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded && !isSliding)
        {
            StartCoroutine(Slide());
        }
    }

    void Jump()
    {
        if (isSliding) return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    IEnumerator Slide()
    {
        isSliding = true;
        // Adjust player collider for sliding
        collider.size = new Vector3(collider.size.x, 0.5f, collider.size.z);
        // Adjust player model for sliding
        model.transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, 0.7f, transform.position.z);

        yield return new WaitForSeconds(slideDuration);

        // Reset player collider
        collider.size = new Vector3(collider.size.x, 1f, collider.size.z);
        // Reset player model
        model.transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, 0.96f, transform.position.z);
        isSliding = false;
    }
}