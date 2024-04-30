using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform orientation;
    public Transform player;

    public float rotationSpeed;

    private void Update()
    {
        Vector3 vieuwDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = vieuwDir.normalized;

        float hor = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = orientation.forward * vert + orientation.right * hor;

        if (inputDir != Vector3.zero)
        {
            player.forward = Vector3.Slerp(player.forward, inputDir.normalized, rotationSpeed * Time.deltaTime);
        }
    }
}
