using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    [Header("References")]
    public new Collider collider;
    public CheckCurrentSword currentSword;
    public GameObject vFXHit;
    public Transform vFXPos;
    public Animator animator;
    public Movement movement;
    public PlayerStats playerStats;
    public InteractMerchant interactMerchant;
    public AudioClip swooshSFX;

    private string slashAnim;

    [Header("Stats")]
    public float damage;
    private float damageToDo;

    [Tooltip("The time it takes for your next attack to be ready")]
    public float attackReset;
    public float staminaCost;

    public bool slashing;

    public bool dagger, branch, sword, claymore;

    [Header("Audio")]
    public float volumeSwoosh;
    public float pitchSwoosh;


    
    public void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
        CheckCurrentSword();
    }
    private void Update()
    {
        Inputs();
        CheckDashing();
    }
    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !slashing && !movement.dashing && playerStats.stamina >= staminaCost/2 && !playerStats.healing && !interactMerchant.shopping)
        {
            playerStats.Stamina(staminaCost);
            StartCoroutine(Slash(attackReset));
        }
    }

    public IEnumerator Slash(float attackReset)
    {
        animator.SetTrigger(slashAnim);
        SFXManager.instance.PlaySFXClip(swooshSFX, transform, volumeSwoosh, pitchSwoosh);

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
    void CheckCurrentSword()
    {
       if(branch || sword)
       {
            slashAnim = "Slash";
       }

       if(dagger)
       {
            slashAnim = "Slash Dagger";
       }

       if(claymore)
       {
            slashAnim = "Slash Claymore";
       }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Enemy" && slashing && !other.GetComponent<BasicAI>().hit || other.transform.tag == "Enemy" && movement.dashing && !other.GetComponent<BasicAI>().hit)
        {
            GameObject vfx = Instantiate(vFXHit, vFXPos.position, quaternion.identity);
            Destroy(vfx, 0.2f);
            other.GetComponent<BasicAI>().Health(damageToDo);
        }
    }
}
