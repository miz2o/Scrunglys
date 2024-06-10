using System.Collections;
using System.Collections.Generic;
/* using Unity.Mathematics; */
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

public class BossScript : BasicAI
{
    public GameObject projectileBoss;
    public GameObject snakes;
    public Transform snakeSummonPos;
    public Transform targetPos;
    public Transform shootPos;

    public bool summoned;
    public float attackRestTimer;
    public float projectileSpeed;
    public float summonSpeed;

    int snakeCap;

    override public void UpdateAI()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        timer += Time.deltaTime;
        
        switch (currentState)
        {
            case State.CHASING:

                agent.destination = player.position;

                RotateTowardsPlayer();

                if(distance <= data.meleeAttackRange)
                {
                    currentState = State.MELEE;
                }
                else if (distance <= data.rangedAttackRange)
                {
                    currentState = State.RANGED;
                }
                
                if(health <= health/2 && !summoned)
                {
                    currentState = State.SUMMONING;
                }
               
                break;

            case State.MELEE:

                if(!attacking && timer >= attackTimer)
                {
                    timer = 0;
                }

                if(health <= health/2 && !summoned)
                {
                    currentState = State.SUMMONING;
                }

                if(distance >= data.meleeAttackRange)
                {
                    currentState = State.RANGED;
                }

               

                break;

            case State.RANGED:
            
                if (!attacking && timer >= attackTimer)
                {
                    timer = 0;
                    AttackPlayerRanged();
                }
                if (distance <= data.meleeAttackRange)
                {
                    currentState = State.MELEE;
                }
                if (distance >= data.rangedAttackRange)
                {
                    currentState = State.CHASING;
                }

                if(health <= health/2 && !summoned)
                {
                    currentState = State.SUMMONING;
                }
                break;
            case State.SUMMONING:
                
                StartCoroutine(SnakeRoutine(summonSpeed));


                if(summoned && distance <= data.meleeAttackRange)
                {
                    currentState = State.MELEE;
                }
                else if(distance <= data.rangedAttackRange)
                {
                    currentState = State.RANGED;
                }
                else
                {
                    currentState = State.CHASING;
                }

                break;
            
        }
    }
     public void AttackPlayerRanged()
    {
        if (timer >= attackTimer)
        {
            RotateTowardsPlayer();

            attacking = true;

            agent.SetDestination(transform.position);
            animator.SetTrigger("Attack Ranged");

            StartCoroutine(RangedAttackStart(data.attackTime));

            timer = 0;
        }
    }

    IEnumerator RangedAttackStart(float animationDuration)
    {
        yield return new WaitForSeconds(animationDuration);
        
        StartCoroutine(RangedAttack());
    }

    IEnumerator RangedAttack()
    {
        RotateTowardsPlayer();


        GameObject spawnedProjectile = Instantiate(projectileBoss, shootPos.position, Quaternion.identity);

        Rigidbody rb = spawnedProjectile.GetComponent<Rigidbody>();

        Vector3 direction = (targetPos.transform.position - shootPos.position).normalized;

        rb.velocity = direction * projectileSpeed;

        yield return new WaitForSeconds(attackRestTimer);
        
        ResetAttackTimer();
        agent.SetDestination(player.position);
    }

   /*  void SetDestination()
    {
        Vector3 newPos = RandomNavSphereTowardsPlayer(transform.position, data.wanderRange, -1);
        agent.SetDestination(newPos);
    } */
    void ResetAttackTimer()
    {
        attackTimer = Random.Range(data.attackTimerMin, data.attackTimerMax);
    }

    /* public Vector3 RandomNavSphereTowardsPlayer(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += player.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);
        return navHit.position;
    } */
    void SummonSnakes()
    {
        Instantiate(snakes, snakeSummonPos, snakeSummonPos);
    }
    private IEnumerator SnakeRoutine(float burstinterval)
    {
        for(int i = 0; i <= snakeCap; i++)
        {
            SummonSnakes();

            yield return new WaitForSeconds(burstinterval);
        }
        yield return summoned = false;

    }
    void ChangeStats()
    {
        // decrease some attack timers for 2nd phase maybe
    }
}
