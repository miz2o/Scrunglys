using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public Transform target;
    public float speed; 
    public float rotateSpeed;

    private float timer;
    public float homingDuration;

    private Rigidbody rb;
    public AudioSource homingSFX;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Orientation").transform;
    }

    private void LateUpdate()
    {   
        timer += Time.deltaTime;

        homingSFX.enabled = true;

        if(timer <= homingDuration)
        {
            Vector3 direction = target.position - rb.position;
            direction.Normalize();

            Vector3 amountToRotate = Vector3.Cross(direction, transform.forward) * Vector3.Angle(transform.forward, direction);

            rb.angularVelocity = -amountToRotate * rotateSpeed;

            rb.velocity = transform.forward * speed;
        }  
        
    }
}
