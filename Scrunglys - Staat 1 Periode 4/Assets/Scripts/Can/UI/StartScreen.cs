using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
   public void StartGame()
    {
        SceneManager.LoadScene("Can Scene", LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
