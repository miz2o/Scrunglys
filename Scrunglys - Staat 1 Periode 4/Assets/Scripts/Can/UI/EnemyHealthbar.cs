using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    public Slider healthBar;
    public Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }
}
