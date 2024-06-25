using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{   
    public GameObject optionsPanel;
    public GameObject playerStatsPanel;
    public InteractMerchant interactMerchant;

    public bool paused;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused && !interactMerchant.shopping)
        {
            Visible();

            playerStatsPanel.SetActive(false);

            optionsPanel.SetActive(true);
            paused = true;
            
            Time.timeScale = 0;
        }
    }

    public void Visible()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
