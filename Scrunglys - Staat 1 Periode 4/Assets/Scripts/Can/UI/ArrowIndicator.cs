using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    public float amplitude = 1f; 
    public float frequency = 1f; 

    private Vector3 startPosition;
    private Transform cam;

    void Start()
    {
        startPosition.y = transform.localPosition.y;
        cam = Camera.main.transform;
    }

    void Update()
    {
        float newY = startPosition.y + amplitude * Mathf.Sin(Time.time * frequency);

        transform.localPosition = new Vector3(0, newY, 0);

        transform.rotation = Quaternion.LookRotation(transform.position - cam.position);

        Vector3 newRotation = transform.eulerAngles;
        newRotation.z = 90;
        transform.eulerAngles = newRotation;
    }
}
