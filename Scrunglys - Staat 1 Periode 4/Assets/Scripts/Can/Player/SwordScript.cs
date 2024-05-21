using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    [Header("References")]
    public Collider collider;
    public Animator animator;
    public Movement movement;

    [Header("Stats")]
    public float damage;
    public float attackReset;

    public bool slashing;

    public void Start()
    {
        collider = GetComponent<Collider>();
        //collider.enabled = false;
    }
    private void Update()
    {
        Inputs();
        AnimationController();
    }
    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !slashing && !movement.dashing)
        {
            StartCoroutine(Slash());
        }
    }
    
    void AnimationController()
    {
        animator.SetBool("Slash", slashing);
    }

    IEnumerator Slash()
    {
        slashing = true;
        //collider.enabled = true;

        yield return new WaitForSeconds(attackReset);

        //collider.enabled = false;
        slashing = false;
    }

    public void OnTriggerStay(Collider other)
    {
        print("Trigger");
        if(other.transform.tag == "Enemy" && slashing)
        {
            other.GetComponent<BasicAI>().Health(damage);
            //other.GetComponent<BasicAI>().hit = true;
        }
    }
    //public void OnCollisionEnter(Collision collision)
    //{
    //    print("Collision");
    //    if (collision.transform.tag == "Enemy")
    //    {
    //        collision.transform.GetComponent<BasicAI>().Health(damage);
    //    }
    //}
}
