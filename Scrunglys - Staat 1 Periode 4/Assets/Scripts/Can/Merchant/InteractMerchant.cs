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
    public bool shopping;

    public void Update()
    {
        if(Physics.Raycast(mainCamera.transform.position, Vector3.forward, rayLength, merchant))
        {
            pressFText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                merchantPanel.SetActive(true);
                shopping = true;

                mouse.Visible();
            }
        }
        else
        {
            pressFText.SetActive(false);
        }
        if (shopping)
        {
            Time.timeScale = 0;
        }
    }
}
