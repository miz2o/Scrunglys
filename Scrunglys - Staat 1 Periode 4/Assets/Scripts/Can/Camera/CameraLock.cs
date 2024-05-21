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
    public Transform camReset;
    public Transform orientation;
    private Transform enemy;

    public ThirdPersonCam cam;  

    [Header("Bools")]
    public bool locked;
    public bool switched;

    [Header("Lock on settings")]
    public LayerMask enemyLayer;
    public float radius;
    public float maxAngle;
    public float maxDistance;
    //public float switchTimer;
    //public float timer;

   
    public void Update()
    {
        Inputs();
        PlayerLookAt();
        CalcEnemyDistance();

        if(enemyCamera.LookAt == null && !switched)
        {
            freeLook.ForceCameraPosition(camReset.position, camReset.rotation);

            if(freeLook.transform.position == camReset.position)
            {
                enemyCamera.Priority = 10;
                freeLook.Priority = 20;

                switched = true;
                locked = false;
            }
        }
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !locked)
        {
            enemy = CalcEnemyLock();
            if(enemy != null)
            {
                enemyCamera.LookAt = enemy;
                switched = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)  && locked) 
        {
            freeLook.ForceCameraPosition(camReset.position, camReset.rotation);
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
        if (closestTarget != null)
        {
            locked = true;
            SetCamera();
            return closestTarget;
        }
        else
        {
            return null;
        }
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
    void CalcEnemyDistance()
    {
        if (enemy != null)
        {
            Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
            Vector3 enemyPos = new Vector3(enemy.position.x, 0, enemy.position.z);

            float distance = Vector3.Distance(playerPos, enemyPos);

            if (distance > maxDistance)
            {
                locked = false;
                enemy = null;
                enemyCamera.LookAt = null;
                freeLook.ForceCameraPosition(camReset.position, camReset.rotation);
                SetCamera();
            }
        }
    }
    void PlayerLookAt()
    {
        if (locked && enemy != null)
        {
            Vector3 direction = enemy.position - player.position;
            direction.y = 0; 
            Quaternion rotation = Quaternion.LookRotation(direction);
            player.rotation = rotation;
        }
    }
}
