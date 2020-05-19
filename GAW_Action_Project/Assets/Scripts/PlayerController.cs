using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 playerMove { get; set; }

    [SerializeField] Rigidbody rb;
    [SerializeField] float Speed = 20f;
    [SerializeField] float JumpSpeed = 5.0f;
    float defaultDrag;

    [Header("キャラクターアニメーション")]
    [SerializeField] Animator animator;

    [Header("接地判定")]
    [SerializeField] Transform groundOrigin;
    [SerializeField] float groundRange;
    [SerializeField] bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        defaultDrag = rb.drag;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Linecast(groundOrigin.position, (groundOrigin.position - transform.up * groundRange));
        animator.SetBool("Grounded", isGrounded);
    }

    private void FixedUpdate()
    {
        Vector3 target_velocity = Vector3.right * playerMove.x * Speed;

        if (playerMove.x != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.right * playerMove.x);
        }

        if (isGrounded)
        {
            rb.drag = defaultDrag;
            rb.AddForce(target_velocity * rb.mass * rb.drag / (1f - rb.drag * Time.fixedDeltaTime));

            if (playerMove.y > InputSystem.settings.defaultButtonPressPoint)
            {
                rb.AddForce(transform.up * JumpSpeed,ForceMode.Impulse);
            }
        }
        else
        {
            rb.drag = 0f;
        }

        animator.SetFloat("Velocity", rb.velocity.magnitude);
    }
}
