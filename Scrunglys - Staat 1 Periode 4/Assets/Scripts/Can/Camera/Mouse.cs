using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Visible()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
