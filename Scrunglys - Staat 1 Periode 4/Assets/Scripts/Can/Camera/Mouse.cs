using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{   
    public GameObject optionsPanel;
    public GameObject playerStatsPanel;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Visible();
            playerStatsPanel.SetActive(false);
            optionsPanel.SetActive(true);
            
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
