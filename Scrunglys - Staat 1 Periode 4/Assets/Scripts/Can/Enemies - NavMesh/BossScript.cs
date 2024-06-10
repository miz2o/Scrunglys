using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

public class BossScript : BasicAI
{
    public GameObject projectileBoss;
    public Transform targetPos;
    public Transform shootPos;

    public float projectileSpeed;
    override public void UpdateAI()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.CHASING:
                agent.destination = player.position;

                RotateTowardsPlayer();

                if (distance <= data.attackRange && !enemyManager.crowded)
                {
                    currentState = State.ATTACKING;
                }
               
                break;

            case State.MELEE:
                break;

            case State.RANGED:
                agent.SetDestination(transform.position);

                if (!attacking)
                {
                    AttackPlayerRanged();
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

            ResetAttackTimer();

            timer = 0;
        }
    }

    IEnumerator RangedAttackStart(float animationDuration)
    {
        yield return new WaitForSeconds(animationDuration);
        
        Attack();
    }

    void Attack()
    {
        RotateTowardsPlayer();

        GameObject spawnedProjectile = Instantiate(projectileBoss, shootPos.position, Quaternion.identity);

        Rigidbody rb = spawnedProjectile.GetComponent<Rigidbody>();

        Vector3 direction = (targetPos.transform.position - shootPos.position).normalized;

        rb.velocity = direction * projectileSpeed;
    }
    void ResetAttackTimer()
    {
        attackTimer = Random.Range(data.attackTimerMin, data.attackTimerMax);
    }

    public Vector3 RandomNavSphereTowardsPlayer(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += player.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
