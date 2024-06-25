using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class MeleeEnemy : BasicAI
{
    public Collider[] attackCollider;
    public bool moving;

    public AudioClip attack;
    public float volume;
    public float pitch;
  

    override public void UpdateAI()
    {
        base.UpdateAI();
        Animator();
    }
    override public void AttackPlayer()
    {
        animator.SetTrigger("Attack");
        SFXManager.instance.PlaySFXClip(attack, transform, volume, pitch);

        StartCoroutine(AttackPlayer(data.attackTime));
    }
    public IEnumerator AttackPlayer(float attackDuration)
    {

        attacking = true;

        yield return new WaitForSeconds(data.waitAnimation);
        foreach(Collider collider in attackCollider)
        {
            collider.enabled = true;
        }
        

        yield return new WaitForSeconds(attackDuration);

        foreach(Collider collider in attackCollider)
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(data.attackWaitTime);

        attacking = false;
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

    public void OnAttackColliderTrigger(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().Health(data.damage);
        }
    }
}
