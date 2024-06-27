using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

public class BossScript : BasicAI
{
    public GameObject projectileBoss;
    public GameObject snakes;
    public Collider attackCollider;
    public Transform snakeSummonPos;
    public Transform targetPos;
    public Transform shootPos;

    public bool summoned;
    public float attackRestTimer;
    public float projectileSpeed;
    public float summonSpeed;

    public float halfHealth;

    public int snakeCap;

    public AudioClip projectileSFX;
    public float pitch;
    public float volume;

    public override void Start()
    {
        currentState = State.CHASING;
        halfHealth = health/2;
    }
    override public void UpdateAI()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        timer += Time.deltaTime;
        
        switch (currentState)
        {
            case State.STANDBY:
                
                agent.destination = transform.position;

                if(distance <= 20)
                {
                    currentState = State.CHASING;
                }
                break;
            case State.CHASING:

                agent.destination = player.position;

                RotateTowardsPlayer();
                
                if (distance <= data.rangedAttackRange)
                {
                    currentState = State.RANGED;
                }
                
                if(health <= halfHealth && !summoned)
                {
                    currentState = State.SUMMONING;
                }
               
                break;

            case State.MELEE:

                if(!attacking )
                {
                    RotateTowardsPlayer();

                    if(timer >= attackTimer)
                    {
                        RotateTowardsPlayer();

                        print("Attacking");
                        AttackPlayerMelee();
                    }
                }
            
                if(health <= halfHealth && !summoned)
                {
                    currentState = State.SUMMONING;
                }

                if(distance >= data.meleeAttackRange)
                {
                    currentState = State.RANGED;
                }

                break;

            case State.RANGED:
            
                RotateTowardsPlayer();
                if (!attacking && timer >= attackTimer)
                {
                    RotateTowardsPlayer();

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

                if(health <= halfHealth && !summoned)
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
                else if(summoned && distance <= data.rangedAttackRange)
                {
                    currentState = State.RANGED;
                }
                else if(summoned)
                {
                    currentState = State.CHASING;
                }

                break;
        }
    }
    IEnumerator MeleeAttackStart(float animationDuration)
    {
        yield return new WaitForSeconds(animationDuration);
        
        print("corotine start");
        StartCoroutine(AttackPlayerMelee(data.meleeAttackTime));
    }

     public void AttackPlayerMelee()
    {
        if(timer >= attackTimer)
        {
            RotateTowardsPlayer();

            attacking = true;

            
            agent.SetDestination(transform.position);
            animator.SetTrigger("Attack Melee");

            StartCoroutine(MeleeAttackStart(data.meleeAttackTime));

            timer = 0; 
        }     
    }
     public IEnumerator AttackPlayerMelee(float attackDuration)
    {
        
        attacking = true;

        yield return new WaitForSeconds(data.waitAnimation);

        attackCollider.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        attackCollider.enabled = false;
        SetDestination();


        yield return new WaitForSeconds(data.attackWaitTime);


        attacking = false;

        timer = 0;
    
        
    }

     public void AttackPlayerRanged()
    {
        if(timer >= attackTimer)
        {
            RotateTowardsPlayer();

            attacking = true;

            
            agent.SetDestination(transform.position);
            animator.SetTrigger("Attack Ranged");

            StartCoroutine(RangedAttackStart(data.rangedAttackTime));

            timer = 0; 
        }     
    }

    IEnumerator RangedAttackStart(float animationDuration)
    {     
        yield return new WaitForSeconds(animationDuration);

        SFXManager.instance.PlaySFXClip(projectileSFX, transform, volume, pitch);
        
        StartCoroutine(RangedAttack());
    }

    IEnumerator RangedAttack()
    {
        RotateTowardsPlayer();

        GameObject spawnedProjectile = Instantiate(projectileBoss, shootPos.position, Quaternion.identity);    

        /* Rigidbody rb = spawnedProjectile.GetComponent<Rigidbody>();

        Vector3 direction = (targetPos.transform.position - shootPos.position).normalized;    

        rb.velocity = direction * projectileSpeed; */

        yield return new WaitForSeconds(attackRestTimer);
        
        ResetAttackTimer();

        attacking = false;

        agent.SetDestination(player.position);
    }

    void SetDestination()
    {
        Vector3 newPos = RandomNavSphereTowardsPlayer(transform.position, data.wanderRange, -1);
        agent.SetDestination(newPos);
        print("New pos");
    }
    void ResetAttackTimer()
    {
        attackTimer = UnityEngine.Random.Range(data.attackTimerMin, data.attackTimerMax);
    }

    public Vector3 RandomNavSphereTowardsPlayer(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * dist;
        randomDirection += player.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);
        return navHit.position;
    }
    void SummonSnakes()
    {
        enemyManager.enemies.Add(Instantiate(snakes, snakeSummonPos.position, quaternion.identity));
        
    }
    private IEnumerator SnakeRoutine(float burstinterval)
    {
        for(int i = 0; i < snakeCap; i++)
        {
            SummonSnakes();

            yield return new WaitForSeconds(burstinterval);
        }

        summoned = true;
    }

    public void OnAttackColliderTrigger(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().Health(data.damage);
        }
    }

    void ChangeStats()
    {
        // decrease some attack timers for 2nd phase maybe
    }
}
