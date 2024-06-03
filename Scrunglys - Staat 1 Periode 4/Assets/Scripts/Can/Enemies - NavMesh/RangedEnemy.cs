using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static Thry.AnimationParser;

public class RangedEnemy : BasicAI
{
    public GameObject projectile;
    public Transform attackPoint;

    public Transform projectileSpawnPoint;

    public float projectileSpeed;

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

                Vector3 direction = player.position - transform.position; //rotate enemy to look at player without ruining other rotations
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;

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

                agent.SetDestination(transform.position);

                timer += Time.deltaTime;

                if (!attacking)
                {
                    AttackPlayer();
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
            animator.SetTrigger("Attack");

            StartCoroutine(AttackPlayerRoutine(data.attackTime));

            ResetAttackTimer(); 

            timer = 0;
        }
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
            Vector3 directionToPlayer = transform.position - player.position;
            directionToPlayer.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            player.rotation = Quaternion.Slerp(player.rotation, targetRotation, data.rotationSmooth * Time.deltaTime);

            ShootProjectile();

             yield return new WaitForSeconds(burstinterval);
        }
       
    }

    private void ShootProjectile()
    {
       
        GameObject spawnedProjectile = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        Rigidbody rb = spawnedProjectile.GetComponent<Rigidbody>();

        Vector3 direction = (attackPoint.transform.position - projectileSpawnPoint.position).normalized;

        rb.velocity = direction * projectileSpeed;
    }
    void ResetAttackTimer()
    {
        attackTimer = Random.Range(data.attackTimerMin, data.attackTimerMax);
    }
}

    

