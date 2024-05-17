using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public EnemyManager enemyManager;

    [Header("Ranges")]
    public float noticeRange;
    public float distanceFromPlayer;
    public float attackRange;
    public float enemyPersonalSpace;
    public float wanderRange;

    [Header("Cooldowns")]
    public float attackTime;

    [Header("Booleans")]
    public bool inAttackRange = false;
    public bool crowded = false;
    public bool following = false;
    public bool attacking = false;
    public bool searching = false;
    public bool inWanderRange = false;

    public State currentState = State.SEARCH;

    static enum State
    {
        SEARCH,
        FOLLOWING,
        ATTACKING,
        WANDERING,
    }

    [Header("Layer")]
    public LayerMask enemy;


    public NavMeshAgent agent;

    public void Start()
    {
        enemyManager.enemies.Add(gameObject);
    }
    void Update()
    {
        States();
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = player.position;
    }
    void States()
    {
        switch (currentState)
        {
            case State.SEARCH:
                agent.destination = player.position;
                break;

            case State.FOLLOWING:
                agent.destination = player.position;
                break;

            case State.ATTACKING;
                break;

            case State.WANDERING:
                break;
        }
    }

    //void CheckDistance()
    //{
    //    if (!crowded)
    //    {
    //        inRange = Physics.CheckSphere(player.position, noticeRange, enemy);

    //        inAttackRange = Physics.CheckSphere(player.position, attackRange, enemy);

    //        crowded = Physics.CheckSphere(player.position, distanceFromPlayer, enemy);
    //        if (!alreadyListed && inRange)
    //        {
    //            enemyCount.enemies.Add(gameObject);
    //            listIndex = enemyCount.enemies.Count;
    //            alreadyListed = true;
    //        }
    //        else if (!inRange)
    //        {
    //            //enemyCount.enemies.Remove(listIndex);
    //        }


    //    }
    //    else
    //    {
    //        WanderAroundPlayer();
    //    }
    //}

    public void FollowPlayer()
    {
        following = true;
        agent.stoppingDistance = attackRange / 2;
        agent.SetDestination(player.position);
    }
    public void AttackPlayer()
    {
        agent.transform.LookAt(player);
        agent.stoppingDistance = attackRange;

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
    public void Search()
    {
        agent.radius = enemyPersonalSpace;
    }
    public void WanderAroundPlayer()
    {
        agent.stoppingDistance = wanderRange;
    }
}
