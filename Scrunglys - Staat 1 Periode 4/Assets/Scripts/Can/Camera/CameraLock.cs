using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraLock : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;
    public CinemachineFreeLook freeLook;
    public CinemachineVirtualCamera enemyCamera;
    public Transform player;
    private Transform enemy;

    [Header("Bools")]
    public bool locked;

    [Header("Lock on settings")]
    public LayerMask enemyLayer;
    public float radius;
    public float maxAngle;

    //private void Start()
    //{
    //    enemyCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    //}
    public void Update()
    {
        Inputs();
        PlayerLookAt();
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !locked)
        {
            if (enemyCamera == null)
            {
                print("Waarom werk jij niet");
            }
            enemy = CalcEnemyLock();
            enemyCamera.LookAt = enemy;
            SetCamera();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)  && locked) 
        {
            locked = false;
            SetCamera();
        }
    }
    Transform CalcEnemyLock()
    {
        Collider[] nearbyEnemy = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        float closestAngle = maxAngle;
        Transform closestTarget = null;

        if(nearbyEnemy.Length <= 0)
        {
            return null;
        }

        for(int i = 0; i < nearbyEnemy.Length; i++)
        {
            Vector3 dir = nearbyEnemy[i].transform.position - mainCamera.transform.position;
            dir.y = 0;

            float angle = Vector3.Angle(mainCamera.transform.forward, dir);
            if (angle < maxAngle)
            {
                closestTarget = nearbyEnemy[i].transform;
                closestAngle = angle;
            }
        }
        locked = true;
        return closestTarget;
    }
    void SetCamera()
    {
        if (locked)
        {
            freeLook.Priority = 10;
            enemyCamera.Priority = 20;
        }
        else
        {
            freeLook.Priority = 20;
            enemyCamera.Priority = 10;
        }
    }
    void PlayerLookAt()
    {
        if (locked)
        {
            player.LookAt(enemy);
        }
        else
        {
            player.LookAt(null);
        }
        
    }
}
