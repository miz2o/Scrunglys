using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    [Header("References")]
    public new Collider collider;
    public Animator animator;
    public Movement movement;
    public PlayerStats playerStats;

    [Header("Stats")]
    public float damage;
    private float damageToDo;

    [Tooltip("The time it takes for your next attack to be ready")]
    public float attackReset;
    public float staminaCost;

    public bool slashing;

    public void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }
    private void Update()
    {
        Inputs();
        CheckDashing();
    }
    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !slashing && !movement.dashing && playerStats.stamina > 0)
        {
            playerStats.Stamina(staminaCost);
            StartCoroutine(Slash(attackReset));
        }
    }

    public IEnumerator Slash(float attackReset)
    {
        animator.SetTrigger("Slash");

        slashing = true;
        collider.enabled = true;

        yield return new WaitForSeconds(attackReset);

        collider.enabled = false;
        slashing = false;
    }

    public void CheckDashing()
    {
        if (movement.dashing)
        {
            damageToDo = damage / 2;
        }
        else
        {
            damageToDo = damage;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Enemy" && slashing && !other.GetComponent<BasicAI>().hit || other.transform.tag == "Enemy" && movement.dashing && !other.GetComponent<BasicAI>().hit)
        {
            other.GetComponent<BasicAI>().Health(damageToDo);
        }
    }
}
