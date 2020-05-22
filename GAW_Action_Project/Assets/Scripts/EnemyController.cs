﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform playerTrans;
    [SerializeField] Rigidbody enemyRb;
    [SerializeField] float anglerRaito = 10f;
    [SerializeField] float EnemySpeed = 10f;
    [SerializeField] float stopDistance = 1f;
    
    private float defaultDrag;
    
    [SerializeField] float KickBackForce = 1f;
    [SerializeField] float KickBackTime = .1f;
    [SerializeField] float hitStopTime = .1f;
    [SerializeField] float HSStartTime = .1f;

    bool isKickBack = false;

    [SerializeField] Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultDrag = enemyRb.drag;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Velocity", enemyRb.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        if ((transform.position - playerTrans.position).magnitude > stopDistance)
        {
            Vector3 velocity = transform.forward * EnemySpeed;
            enemyRb.AddForce(velocity * enemyRb.mass * enemyRb.drag / (1f - enemyRb.drag * Time.fixedDeltaTime));
        }

        Vector3 dir = playerTrans.position - transform.position;

        Quaternion target_rot = Quaternion.LookRotation(dir);
        target_rot = target_rot * Quaternion.Inverse(transform.rotation);
        Vector3 torque = new Vector3(target_rot.x, target_rot.y, target_rot.z) * anglerRaito;

        enemyRb.AddTorque(torque);
    }

    public void Damage()
    {
        StartCoroutine(KickBack());
        animator.SetTrigger("Damage");
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
}
