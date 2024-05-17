using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("References")]
    private CharacterController cController;
    public Transform thirdPersonCamera;

    [Header("Basic movement")]
    public int walkSpeed;
    public int sprintSpeed;
    public float gravity;

    [Header("Dashing")]
    public int dash;
    public float dashCooldown;
    public float dashDuration;
    public bool dashing;

    [Header("Inputs")]
    private float vert;
    private float hor;

    private Vector3 moveDir;

    private void Start()
    {
         cController = GetComponent<CharacterController>();
    }
   
    private void Update()
    {
        Inputs();
        ApplyGravity();
    }
    void Inputs()
    {
        vert = Input.GetAxisRaw("Vertical");
        hor = Input.GetAxisRaw("Horizontal");

        moveDir = new Vector3(hor, 0f, vert);

        moveDir = Quaternion.AngleAxis(thirdPersonCamera.rotation.eulerAngles.y, Vector3.up) * moveDir;

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
        dashing = true;

        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            cController.Move(moveDir.normalized * dash * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(dashCooldown);
        dashing = false;
    }
}
