using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform playerTrans;
    [SerializeField] Rigidbody enemyRb;
    [SerializeField] float anglerRaito = 10f;
    [SerializeField] float stopDistance = 1f;
    private float defaultDrag;

    [Header("ダメージ処理")]
    [SerializeField] float KickBackForce = 1f;
    [SerializeField] float KickBackTime = .1f;
    [SerializeField] float hitStopTime = .1f;
    [SerializeField] float HSStartTime = .1f;
    bool isKickBack = false;

    [Header("HP処理系")]
    public float MaxHp;
    public float Hp;
    [SerializeField] bool isDead = false;
    [SerializeField] float DestroyTime = 0.5f;

    [Header("攻撃範囲")]
    [SerializeField] Transform AttackBoxOrigin;
    [SerializeField] Vector3 AttackBoxScale = Vector3.one;
    [SerializeField] float AttackForce = 10f;

    [Header("ステータスデータ")]
    [SerializeField] EnemyData data;

    [SerializeField] Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHp = data.MaxHp;
        defaultDrag = enemyRb.drag;

        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            animator.SetFloat("Velocity", enemyRb.velocity.magnitude);
        }
        animator.SetBool("Dead", isDead);
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if ((transform.position - playerTrans.position).magnitude > stopDistance)
            {
                animator.ResetTrigger("Attack");
                DoMove();
            }
            else
            {
                animator.SetTrigger("Attack");
            }

            PlayerLookAt();
        }
    }

    // Call me from FixedUpdate()
    void DoMove()
    {
        Vector3 velocity = transform.forward * data.EnemyMaxSpeed;
        enemyRb.AddForce(velocity * enemyRb.mass * enemyRb.drag / (1f - enemyRb.drag * Time.fixedDeltaTime));
    }

    // Call me from FixedUpdate()
    void PlayerLookAt()
    {
        Vector3 dir = playerTrans.position - transform.position;

        Quaternion target_rot = Quaternion.LookRotation(dir);
        target_rot = target_rot * Quaternion.Inverse(transform.rotation);
        Vector3 torque = new Vector3(target_rot.x, target_rot.y, target_rot.z) * anglerRaito;

        enemyRb.AddTorque(torque);
    }

    public void Damage(float damage)
    {
        Hp -= damage;
        // To dead
        if (Hp <= 0)
        {
            Hp = 0;
            isDead = true;
            IsDead();
        }
        else
        {
            StartCoroutine(KickBack());
            animator.SetTrigger("Damage");
        }
    }

    void IsDead()
    {
        enemyRb.constraints = RigidbodyConstraints.None;
        enemyRb.drag = 0;
        GetComponent<CapsuleCollider>().direction = 2;
        GetComponent<Collider>().enabled = false;
        Destroy(this.gameObject,DestroyTime);
    }

    IEnumerator KickBack()
    {
        isKickBack = true;
        StartCoroutine(HSStartTimer());

        yield return new WaitForSecondsRealtime(KickBackTime);

        isKickBack = false;
    }
    
    IEnumerator HSStartTimer()
    {
        yield return new WaitForSecondsRealtime(HSStartTime);
        StartCoroutine(HitStop());
    }

    IEnumerator HitStop()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(hitStopTime);
        Time.timeScale = 1;
    }

    // animationEvent
    void OnAttack()
    {
        Collider[] cols = Physics.OverlapBox(AttackBoxOrigin.position, AttackBoxScale, Quaternion.identity);

        foreach (Collider target in cols)
        {
            Rigidbody target_rb = target.GetComponent<Rigidbody>();
            if (target_rb != null)
            {
                target_rb.AddForceAtPosition(transform.forward * AttackForce, AttackBoxOrigin.position, ForceMode.Impulse);
            }

            if (target.CompareTag("Player"))
            {
                playerTrans.GetComponent<PlayerController>().Damage(data.AttackPoint);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawCube(AttackBoxOrigin.position, AttackBoxScale * 2f);
    }
}
