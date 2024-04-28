using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController cController;
    public int walkSpeed;
    public int sprintSpeed;
    public int dash;

    public float dashCooldown;
    public float dashDuration;
    private float vert;
    private float hor;

    public bool dashing;
    

    private Vector3 moveDir;

    private void Start()
    {
         cController = GetComponent<CharacterController>();
    }
   
    private void Update()
    {
        Inputs();
    }
    void Inputs()
    {
        vert = Input.GetAxisRaw("Vertical");
        hor = Input.GetAxisRaw("Horizontal");

        moveDir = new Vector3(hor, 0f, vert);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprint();
        }
        else
        {
            Walk();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !dashing)
        {
            StartCoroutine(Dash());
        }
    }
    void Walk()
    {
        cController.Move(moveDir * walkSpeed * Time.deltaTime);
    }
    void Sprint()
    {
        cController.Move(moveDir * sprintSpeed * Time.deltaTime);
    }
   
    IEnumerator Dash()
    {
        dashing = true;

        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            cController.Move(moveDir * dash * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(dashCooldown);
        dashing = false;
    }
    
}
