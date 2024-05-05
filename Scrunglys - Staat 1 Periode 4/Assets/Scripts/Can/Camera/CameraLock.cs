using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    [Header("References")]
    public CinemachineFreeLook freeLook;
    public Transform player;
    public Transform enemy;
    public ThirdPersonCam cam;
    
    [Header("Bools")]
    public bool locked;

    [Header("Transition")]
    public float smooth;
    public bool rotateAgain;

    public void Update()
    {
        Inputs();   
        Rotate();
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !locked)
        {
            locked = true;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)  && locked) 
        {
            locked = false;
        }
    }

    void Transition(Transform target)
    {

        Vector3 startPos = freeLook.transform.position;
        Quaternion startRot = freeLook.transform.rotation;


        freeLook.transform.position = Vector3.Lerp(startPos, target.position, smooth);
        freeLook.transform.rotation = Quaternion.Slerp(startRot, Quaternion.LookRotation(target.position - freeLook.transform.position), smooth);

        freeLook.LookAt = target;
    }


    void Rotate()
    {
        if (locked)
        {
            player.LookAt(enemy);
            cam.enabled = false;
            Transition(enemy);
        }
        else
        {
            Transition(player);
            cam.enabled = true;
        }
    }
}
