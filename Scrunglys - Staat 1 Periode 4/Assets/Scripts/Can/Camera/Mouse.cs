using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Visible();
    //    }
    //}

    void Visible()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
