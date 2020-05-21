using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 playerMove { get; set; }
    public bool Attack { get; set; }

    [SerializeField] Rigidbody rb;
    [SerializeField] float Speed = 20f;
    [SerializeField] float JumpSpeed = 5.0f;
    float defaultDrag;

    [Header("キャラクターアニメーション")]
    [SerializeField] Animator animator;
    [SerializeField] float AttackForce = 5.0f;

    [Header("接地判定")]
    [SerializeField] Transform groundOrigin;
    [SerializeField] float groundRange;
    [SerializeField] bool isGrounded;

    [Header("攻撃判定")]
    [SerializeField] Transform AttackOrigin;
    [SerializeField] Vector3 attackBoxSize;
    [SerializeField] LayerMask attackLayer;

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
        animator.SetBool("Attack", Attack);
    }

    private void FixedUpdate()
    {
        Vector3 target_velocity = Vector3.right * playerMove.x * Speed;

        if (isGrounded)
        {
            rb.drag = defaultDrag;

            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("moveTree"))
            {
                if (playerMove.x != 0)
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.right * playerMove.x);
                }

                rb.AddForce(target_velocity * rb.mass * rb.drag / (1f - rb.drag * Time.fixedDeltaTime));

                if (playerMove.y > InputSystem.settings.defaultButtonPressPoint)
                {
                    rb.AddForce(transform.up * JumpSpeed, ForceMode.Impulse);
                }
            }
        }
        else
        {
            rb.drag = 0f;
        }

        animator.SetFloat("Velocity", rb.velocity.magnitude);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.4f);
        Gizmos.DrawCube(AttackOrigin.position, attackBoxSize * 2f);
    }

    void OnAttack()
    {
        Debug.Log("Attack");
        if (isGrounded)
        {
            rb.AddForce(transform.forward * AttackForce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(transform.up * AttackForce / 2f, ForceMode.Impulse);
        }

        Collider[] cols = Physics.OverlapBox(AttackOrigin.position, attackBoxSize,Quaternion.identity,attackLayer);

        foreach (Collider target in cols)
        {
            Rigidbody target_rb = target.GetComponent<Rigidbody>();
            if (target_rb != null)
            {
                target_rb.AddForceAtPosition(transform.forward * 20, AttackOrigin.position, ForceMode.Impulse);
            }
        }
    }
}
