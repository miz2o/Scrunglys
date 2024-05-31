using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMerchant : MonoBehaviour
{
    public Camera mainCamera;
    public RaycastHit hit;
    public float rayLength;
    public LayerMask merchant;

    public GameObject pressFText;
    public GameObject merchantPanel;
    public Mouse mouse;

    public void Update()
    {
        if(Physics.Raycast(mainCamera.transform.position, Vector3.forward, rayLength, merchant))
        {
            pressFText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                merchantPanel.SetActive(true);

                mouse.Visible();

                Time.timeScale = 0;
            }
        }
        else
        {
            pressFText.SetActive(false);
        }
    }
}
