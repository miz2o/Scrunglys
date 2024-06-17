using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsScreen : MonoBehaviour
{
 public GameObject pausePanel;
 public GameObject optionsPanel;

 public GameObject playerStatsPanel;

 public Mouse mouse;
 public AudioSource bling;

 public void ExitButton()
 {
    pausePanel.SetActive(false);
    playerStatsPanel.SetActive(true);

    bling.Play();

    Time.timeScale = 1;
    mouse.Hide();
 }
 public void OptionsButton()
 {
    optionsPanel.SetActive(true);
    pausePanel.SetActive(false);

    bling.Play();
 }

 public void ReturnButton()
 {
  optionsPanel.SetActive(false);
  pausePanel.SetActive(true);

  bling.Play();
 }

 public void QuitGame()
 {
   SceneManager.LoadScene("Can Scene UI", LoadSceneMode.Single);

   bling.Play();
 }
}
