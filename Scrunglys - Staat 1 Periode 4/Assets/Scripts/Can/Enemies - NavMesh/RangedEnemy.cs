using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;


public class RangedEnemy : BasicAI
{
    public GameObject projectile;
    public Transform attackPoint;

    public Transform projectileSpawnPoint;

    public float projectileSpeed;
    public bool moving;

    private new void Awake()
    {
        base.Awake();
    }
    private new void Start()
    {
        base.Start();
    }
    #region Update 
    override public void UpdateAI()
    {
        Animator();

        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.IDLE:

                if (searchTimer == 0)
                {
                    searchTimer = Random.Range(data.searchTimerMin, data.searchTimerMax);
                    timer = 0;
                    Vector3 newPos = RandomNavSphereIdle(transform.position, data.searchRange, -1);
                    agent.SetDestination(newPos);
                }

                Search();

                if (distance <= data.noticeRange)
                {
                    currentState = State.CHASING;
                }
                break;

            case State.CHASING:

                agent.destination = player.position;

                RotateTowardsPlayer();

                if (distance <= data.attackRange)
                {
                    currentState = State.ATTACKING;
                }
                else if (distance >= data.noticeRange)
                {
                    currentState = State.IDLE;
                }
                break;

            case State.ATTACKING:

                timer += Time.deltaTime;

                if (!attacking)
                {
                    AttackPlayer();
                }
                else if (attacking)
                {
                    RotateTowardsPlayer();
                }
                else if (distance >= data.attackRange)
                {
                    currentState = State.CHASING;
                }
                break;
        }
    }
    #endregion
    override public void AttackPlayer()
    {
        if (timer >= attackTimer)
        {
            RotateTowardsPlayer();

            attacking = true;

            agent.SetDestination(transform.position);
            animator.SetTrigger("Attack");

            StartCoroutine(AttackPlayerRoutine(data.attackTime));

            ResetAttackTimer(); 

            timer = 0;
        }
    }

    public Vector3 RandomNavSphereTowardsPlayer(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += player.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    private IEnumerator AttackPlayerRoutine(float attackDuration)
    {
        yield return new WaitForSeconds(attackDuration);

        StartCoroutine(ShootBurst(data.burstInterval));
    }

    private IEnumerator ShootBurst(float burstinterval)
    {
        for(int i = 0; i <= data.bulletCap; i++)
        {
            ShootProjectile();

            yield return new WaitForSeconds(burstinterval);
        }
        yield return attacking = false;

        SetDestination();
    }

    private void ShootProjectile()
    {
        RotateTowardsPlayer();

        GameObject spawnedProjectile = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        Rigidbody rb = spawnedProjectile.GetComponent<Rigidbody>();

        Vector3 direction = (attackPoint.transform.position - projectileSpawnPoint.position).normalized;

        rb.velocity = direction * projectileSpeed;
    }
    void ResetAttackTimer()
    {
        attackTimer = Random.Range(data.attackTimerMin, data.attackTimerMax);
    }
    void SetDestination()
    {
        Vector3 newPos = RandomNavSphereTowardsPlayer(transform.position, data.wanderRange, -1);
        agent.SetDestination(newPos);
    }

    void Animator()
    {
        if(agent.velocity != Vector3.zero)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        animator.SetBool("Moving", moving);
    }
}

    

