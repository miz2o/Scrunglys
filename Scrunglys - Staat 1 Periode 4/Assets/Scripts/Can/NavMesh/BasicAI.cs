using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Ranges")]
    public float noticeRange;
    public float distanceFromPlayer;
    public float attackRange;
    public float enemyPersonalSpace;
    public float wanderRange;

    [Header("Cooldowns")]
    public float attackTime;

    [Header("Booleans")]
    public bool tooClose= false;
    public bool inRange = false;
    public bool inAttackRange = false;
    public bool crowded = false;
    public bool following = false;
    public bool attacking = false;
    public bool searching = false;
    public bool alreadyListed = false;

    [Header("Layer")]
    public LayerMask enemy;

    [Header("List settings")]
    public int listIndex;

    public NavMeshAgent agent;
    void Update()
    {
        States();
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = player.position;
    }
    void States()
    {
        //if (!inRange && !inAttackRange && !tooClose)
        //{
        //    searching = true;
        //} 

        if (inAttackRange)
        {
            AttackPlayer();
            return;
        }
        else if (tooClose)
        {
            WanderAroundPlayer();
            return;
        }
        else if (inRange)
        {
            FollowPlayer();
            return;
        }
        else 
        {
            Search();
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

    void FollowPlayer()
    {
        following = true;
        agent.SetDestination(player.position);
    }
    void AttackPlayer()
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
    void Search()
    {
        agent.radius = enemyPersonalSpace;
    }
    public void WanderAroundPlayer()
    {
        agent.stoppingDistance = wanderRange;
    }
}
