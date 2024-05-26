using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("References")]
    private CharacterController cController;
    public Transform thirdPersonCamera;
    public PlayerStats playerStats;
    public Animator mainAnimation;
    public SwordScript swordScript;

    [Header("Basic movement")]
    public int walkSpeed;
    public int sprintSpeed;
    public float gravity;
    public float sprintStamina;

    [Header("Dashing")]
    public int dash;
    public float dashCooldown;
    public float dashDuration;
    public bool dashing;
    public float dashStamina;

    [Header("Inputs")]
    private float vert;
    private float hor;

    [Header("Animation Bools")]
    public bool sprint;
    public bool walking;

    [Header("Layers")]
    public LayerMask enemy;
    public LayerMask nothing;

    private Vector3 moveDir;

    private void Start()
    {
         cController = GetComponent<CharacterController>();
         mainAnimation = GetComponent<Animator>();
    }
   
    private void Update()
    {
        Inputs();
        ApplyGravity();
        AnimatorManager();
    }
    void Inputs()
    {
        if (!swordScript.slashing)
        {
            vert = Input.GetAxisRaw("Vertical");
            hor = Input.GetAxisRaw("Horizontal");

            moveDir = new Vector3(hor, 0f, vert);

            if (moveDir.magnitude > 0)
            {
                walking = true;
            }
            else
            {
                walking = false;
                sprint = false;
            }

            moveDir = Quaternion.AngleAxis(thirdPersonCamera.rotation.eulerAngles.y, Vector3.up) * moveDir;
        }
        else
        {
            vert = 0;
            hor = 0;
        }
        

        if (Input.GetKey(KeyCode.LeftShift) && playerStats.stamina > 0 && !swordScript.slashing)
        {
            playerStats.SprintStamina(sprintStamina);
            Sprint();
            sprint = true;
        }
        else if (!swordScript.slashing)
        {
            Walk();
            sprint = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !dashing && !swordScript.slashing && playerStats.stamina > 0)
        {
            playerStats.Stamina(dashStamina);
            mainAnimation.SetTrigger("DashTrigger");

            StartCoroutine(Dash());
        }
    }
    void Walk()
    {
        cController.Move(moveDir.normalized * walkSpeed * Time.deltaTime);
    }
    void Sprint()
    {
        cController.Move(moveDir.normalized * sprintSpeed * Time.deltaTime);
    }
    void ApplyGravity()
    {
         cController.Move(-Vector3.up * gravity * Time.deltaTime);
    }
    IEnumerator Dash()
    {

        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            cController.Move(moveDir.normalized * dash * Time.deltaTime);
            dashing = true;
            swordScript.collider.enabled = true;

            cController.excludeLayers = enemy;
            yield return null;
        }

        if(Time.time > startTime + dashDuration)
        {
            swordScript.collider.enabled = false;
            cController.excludeLayers = nothing;
        }
        yield return new WaitForSeconds(dashCooldown);
        dashing = false;
    }
    void AnimatorManager()
    {
        mainAnimation.SetBool("Dash", dashing);
        mainAnimation.SetBool("Sprint", sprint);
        mainAnimation.SetBool("Walking", walking);
    }
}
