using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform playerTrans;
    [SerializeField] Rigidbody enemyRb;
    [SerializeField] float KickBackForce = 1f;

    bool isKickBack = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerTrans.position);
        if (isKickBack)
        {
            agent.isStopped = true;
            agent.updateRotation = false;
            agent.velocity = playerTrans.forward * KickBackForce;
        }
        else
        {
            agent.isStopped = false;
            agent.updateRotation = true;
        }
    }

    public void Damage()
    {
        StartCoroutine(KickBack());
    }

    IEnumerator KickBack()
    {
        isKickBack = true;

        yield return new WaitForSeconds(.2f);

        isKickBack = false;
    }
}
