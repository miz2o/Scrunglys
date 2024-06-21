using Cinemachine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;
    public CinemachineFreeLook freeLook;
    public CinemachineFreeLook enemyCamera;

    public Transform player;
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
    public float rotationSmooth;
    public Vector3 cameraOffset;

    private void Update()
    {
        HandleInputs();
        PlayerLookAtEnemy();
        CheckEnemyDistance();

        if (enemyCamera.LookAt == null && !switched)
        {
            SwitchToFreeLookCamera();
        }
    }
    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!locked)
            {
                enemy = FindEnemyToLockOn();
                if (enemy != null)
                {
                    enemyCamera.LookAt = enemy.GetComponent<BasicAI>().lookAt;
                    switched = false;
                    locked = true;

                    enemy.GetComponent<BasicAI>().arrowIndicator.SetActive(true);

                    SwitchToEnemyCamera();
                }
            }
            else
            {

                enemy.GetComponent<BasicAI>().arrowIndicator.SetActive(false);

                UnlockCamera();        
            }
        }
    }

    private Transform FindEnemyToLockOn()
    {
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        Transform closestTarget = null;
        float closestAngle = maxAngle;

        foreach (Collider enemyCollider in nearbyEnemies)
        {
            Vector3 directionToEnemy = enemyCollider.transform.position - mainCamera.transform.position;
            directionToEnemy.y = 0;

            float angleToEnemy = Vector3.Angle(mainCamera.transform.forward, directionToEnemy);
            if (angleToEnemy < closestAngle)
            {
                closestTarget = enemyCollider.transform;
                closestAngle = angleToEnemy;
            }
        }
        if(enemy == null)
        {
            print("GEEN ENEMY");
        }

        return closestTarget;
    }

    private void SwitchToEnemyCamera()
    {
        freeLook.Priority = 10;
        enemyCamera.Priority = 20;
    }

    private void SwitchToFreeLookCamera()
    {
        freeLook.Priority = 20;
        enemyCamera.Priority = 10;
        switched = true;
        locked = false;
    }

    private void UnlockCamera()
    {
        locked = false;
        enemyCamera.LookAt = null;
        SwitchToFreeLookCamera();
    }

    private void CheckEnemyDistance()
    {
        if (enemy != null)
        {
            Vector3 playerPosition = new Vector3(player.position.x, 0, player.position.z);
            Vector3 enemyPosition = new Vector3(enemy.position.x, 0, enemy.position.z);

            float distanceToEnemy = Vector3.Distance(playerPosition, enemyPosition);
            if (distanceToEnemy > maxDistance)
            {
                UnlockCamera();
                enemy = null;
            }
        }
    }

    private void PlayerLookAtEnemy()
    {
        if (locked && enemy != null)
        {
            Vector3 directionToEnemy = enemy.position - player.position;
            directionToEnemy.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            player.rotation = Quaternion.Slerp(player.rotation, targetRotation, rotationSmooth * Time.deltaTime);
        }
    }
}