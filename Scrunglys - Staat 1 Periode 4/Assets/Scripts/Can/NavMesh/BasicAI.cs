using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public EnemyCount enemyCount;

    [Header("Ranges")]
    public float noticeRange;
    public float distanceFromPlayer;
    public float attackRange;

    [Header("Cooldowns")]
    public float attackTime;
    [Header("Booleans")]
    public bool tooClose= false;
    public bool inRange = false;
    public bool inAttackRange = false;
    public bool crowded = false;
    public bool following = false;
    public bool attacking = false;

    [Header("Layer")]
    public LayerMask enemy;

    public NavMeshAgent agent;
    void Update()
    {
        CheckDistance();
        States();
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = player.position;
    }
    void States()
    {
        if (inRange)
        {
            FollowPlayer();
        }
    }

    void CheckDistance()
    {
        if (!crowded)
        {
            inRange = Physics.CheckSphere(player.position, noticeRange, enemy);

            inAttackRange = Physics.CheckSphere(player.position, attackRange, enemy);

            tooClose = Physics.CheckSphere(player.position, distanceFromPlayer, enemy);
            //enemyCount.enemies.Add(gameObject);
        }
        else
        {
            
        }
    }

    void FollowPlayer()
    {
        following = true;
        agent.SetDestination(player.position);
    }
    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        agent.transform.LookAt(player);

        if (!attacking)
        {
            attacking = true;
            Invoke(nameof(ResetAttack), attackTime);
        }
    }
    void ResetAttack()
    {
        attacking = false;
    }

}
