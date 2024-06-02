using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Thry.AnimationParser;

public class RangedEnemy : BasicAI
{
    public GameObject projectile;

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

    override public void UpdateAI()
    {
        base.UpdateAI();
    }
    override public void AttackPlayer()
    {
        animator.SetTrigger("Attack");

        StartCoroutine(AttackPlayerRoutine(data.attackTime));
    }
    private IEnumerator AttackPlayerRoutine(float attackDuration)
    {
        yield return new WaitForSeconds(attackDuration);

        ShootProjectile();
    }

    private void ShootProjectile()
    {
       
        // Instantiate the projectile at the spawn point
        GameObject spawnedProjectile = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // Get the Rigidbody component of the projectile to apply force
        Rigidbody rb = spawnedProjectile.GetComponent<Rigidbody>();

        // Calculate the direction to the player or a target point
        Vector3 direction = (player.transform.position - projectileSpawnPoint.position).normalized;

        // Apply force to the projectile to make it move
        rb.velocity = direction * projectileSpeed;
        
    }
}

    

