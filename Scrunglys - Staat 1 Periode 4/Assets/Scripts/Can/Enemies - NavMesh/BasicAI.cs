using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class BasicAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public EnemyManager enemyManager;

    [Header("Stats/Health")]
    public float health;

    [Header("Ranges")]
    public float noticeRange;
    public float distanceFromPlayer;
    public float attackRange;
    public float wanderRange;
    public float searchRange;

    [Header("Cooldowns")]
    public float attackTime;

    [Header("Timers")]
    public float wanderTimer;
    public float wanderTimerMin;
    public float wanderTimerMax;

    public float searchTimer;
    public float searchTimerMin;
    public float searchTimerMax;

    public float timer;

    [Header("Booleans")]
    public bool listed = false;
    public bool attacked = false;
    public bool hit = false;

    public State currentState = State.IDLE;

    public enum State
    {
        IDLE,
        CHASING,
        ATTACKING,
        WANDERING,
    }

    [Header("Layer")]
    public LayerMask enemy;

    public NavMeshAgent agent;

    #region UPDATEAI
    public void UpdateAI()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        if (enemyManager == null)
        {
            enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        }

        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.IDLE:
                if(searchTimer == 0)
                {
                    searchTimer = Random.Range(searchTimerMin, searchTimerMax);
                    timer = 0;
                    Vector3 newPos = RandomNavSphereIdle(transform.position, searchRange, -1);
                    agent.SetDestination(newPos);
                }

                Search();

                if(distance <= noticeRange)
                {
                    currentState = State.CHASING;
                }

                if (listed)
                {
                    enemyManager.crowdCount--;
                    listed = false;
                }
                break;

            case State.CHASING:
                agent.destination = player.position;

                Vector3 direction = player.position - transform.position; //rotate enemy to look at player without ruining other rotations
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation; 

                if (distance <= attackRange && !enemyManager.crowded)
                {
                    currentState = State.ATTACKING;
                }
                else if (distance <= wanderRange && enemyManager.crowded)
                {
                    currentState = State.WANDERING;
                }
                else if (distance >= noticeRange)
                {
                    currentState = State.IDLE;
                }

                if (listed)
                {
                    enemyManager.crowdCount--;
                    listed = false;
                }
                break;

            case State.ATTACKING:
                if (!listed)
                {
                    enemyManager.crowdCount++;
                    listed = true;
                }


                AttackPlayer();
               
                if(distance >= attackRange)
                {
                    currentState = State.CHASING;
                }
                break;

            case State.WANDERING:

                if (listed)
                {
                    enemyManager.crowdCount--;
                    listed = false;
                }

                if (wanderTimer == 0)
                {
                    wanderTimer = Random.Range(wanderTimerMin, wanderTimerMax);
                    timer = 0;
                    Vector3 newPos = RandomNavSphereWander(player.position, wanderRange, -1);
                    agent.SetDestination(newPos);
                }

                WanderAroundPlayer();

                if (!enemyManager.crowded)
                {
                    currentState = State.CHASING;
                }
                break;
        }
    }
    #endregion

    #region ATTACK
    public void AttackPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;

        if (!attacked)
        {
            attacked = true;
            Invoke(nameof(ResetAttack), attackTime);
        }

        if (attacked)
        {
            //Vector3 newPos = RandomNavSphereAttacking(player.position, attackRange, -1);
            agent.SetDestination(player.position);
        }
    }
    void ResetAttack()
    {
        attacked = false;
        print("Reset attack");
    }

    //public static Vector3 RandomNavSphereAttacking(Vector3 origin, float dist, int layermask)
    //{
    //    Vector3 randDistance = Random.insideUnitSphere * dist;

    //    randDistance += origin;

    //    NavMeshHit navMeshHit;
    //    NavMesh.SamplePosition(randDistance, out navMeshHit, dist, -1);

    //    return navMeshHit.position;
    //}
    #endregion

    #region SEARCHING
    public void Search()
    {
        timer += Time.deltaTime;

        if (timer >= searchTimer)
        {
            Vector3 newPos = RandomNavSphereIdle(transform.position, searchRange, -1);
            agent.SetDestination(newPos);
            SetSearchTime();
            timer = 0;
        }
    }
    public void SetSearchTime()
    {
        searchTimer = Random.Range(searchTimerMin, searchTimerMax);
    }
    public static Vector3 RandomNavSphereIdle(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDistance = Random.insideUnitSphere * dist;

        randDistance += origin;

        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(randDistance, out navMeshHit, dist, -1);

        return navMeshHit.position;
    }
    #endregion

    #region WANDERING
    public void WanderAroundPlayer()
    {
        timer += Time.deltaTime;

        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;


        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphereWander(player.position, wanderRange, -1);
            agent.SetDestination(newPos);
            SetWanderTime();
            timer = 0;
        }
    }
    public void SetWanderTime()
    {
        wanderTimer = Random.Range(wanderTimerMin, wanderTimerMax);
    }
    public static Vector3 RandomNavSphereWander(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDistance = Random.insideUnitSphere * dist;

        randDistance += origin;

        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(randDistance, out navMeshHit, dist, -1);

        return navMeshHit.position;
    }
    #endregion

    #region HEALTH
    public void Health(float damageToDo)
    {
        print("Hit");
        health -= damageToDo;

        if (health <= 0)
        {
            if (listed)
            {
                enemyManager.crowdCount--;
                listed = false;
            }
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
    #endregion
}

