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
 public AudioClip bling;
 public float volumeBling;
 public float pitchBling;
 public float pitchMin, pitchMax;

 public void ExitButton()
 {
    pausePanel.SetActive(false);
    playerStatsPanel.SetActive(true);
   
    SFXManager.instance.PlaySFXClip(bling, transform, volumeBling, Pitch());
    
    mouse.paused = false;  
    Time.timeScale = 1;
    mouse.Hide();
 }
 public void OptionsButton()
 {
    optionsPanel.SetActive(true);
    pausePanel.SetActive(false);

    SFXManager.instance.PlaySFXClip(bling, transform, volumeBling, Pitch());
 }

 public void ReturnButton()
 {
  optionsPanel.SetActive(false);
  pausePanel.SetActive(true);

  SFXManager.instance.PlaySFXClip(bling, transform, volumeBling, Pitch());
 }

 public void QuitGame()
 {
   SceneManager.LoadScene("Can Scene UI", LoadSceneMode.Single);

   SFXManager.instance.PlaySFXClip(bling, transform, volumeBling, Pitch());
 }

 public float Pitch()
 {
   pitchBling = Random.Range(pitchMin, pitchMax);

   return pitchBling;
 }
}
