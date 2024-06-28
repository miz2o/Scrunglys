using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public float health;
    public float maxHealth;
    public float stamina;
    public float staminaRegenRate;
    public float maxStamina;

    [Header("Potions")]
    public int potions;
    public float potionValue, animationDuration;
    public TMP_Text potionText;
    public bool healing = false;

    [Header("Cooldowns")]
    public float timer;
    public float staminaRegenCD;
    public float resetStaminaTimer;

    [Header("References")]
    public Movement movement;
    public Animator animator;

    public GameObject potionModel;
    public GameObject gameOverPanel;

    public Slider healthSlider;
    public Slider staminaSlider;
    public AudioClip hitSFX;
    public AudioClip potionSFX;
    public float pitchPotion, pitchHit;
    public float volumePotion, volumeHit;

    public bool staminaLock;
    public bool canDie = true;

    private void Start()
    {
        animator = GetComponent<Animator>();

        health = maxHealth; 

        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = stamina;
    }
    public void Update()
    {
        Inputs();
        RegenStamina();
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.E) && !healing)
        {
            Heal();
        }
        if(Input.GetKeyDown(KeyCode.P) && canDie)
        {
            canDie = false;
        }
        else if(Input.GetKeyDown(KeyCode.P) && !canDie)
        {
            canDie = true;
        }
        if(Input.GetKeyDown(KeyCode.O) && !staminaLock)
        {
            staminaLock = true;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            staminaLock = false;
        }
    }
    void Heal()
    {
        if(potions != 0)
        {
            SFXManager.instance.PlaySFXClip(potionSFX, transform, volumePotion, pitchPotion);

            animator.SetTrigger("Drink");

            health += potionValue;

            healthSlider.value = health;

            healing = true;
            potionModel.SetActive(true);

            potions--;
            potionText.text = potions.ToString();

            Invoke("ResetHeal", animationDuration);
        }

        health = Mathf.Clamp(health, 0, maxHealth);
    }
    private void ResetHeal()
    {
        healing = false;
        potionModel.SetActive(false);
    }
    public void Health(float damageToDo)
    {
        health -= damageToDo;
        SFXManager.instance.PlaySFXClip(hitSFX, transform, volumeHit, pitchHit);
        
        health = Mathf.Clamp(health, 0, maxHealth);

        healthSlider.value = health;

        // voor de presentatie, zodat we niet dood kunnen gaan
        if (health <= 0 && canDie) 
        {
            Death();
        } 
    }

    public void Death()
    {
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.Confined;

        gameOverPanel.SetActive(true);
    }
    public void Stamina(float actionStamina)
    {
        if(Time.timeScale > 0)
        {
            stamina -= actionStamina;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);

            staminaSlider.value = stamina;

            timer = 0;
        }
    }
    public void SprintStamina(float sprintStamina)
    {
        stamina -= sprintStamina * Time.deltaTime;

        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        staminaSlider.value = stamina;
        
        timer = 0;
    }
    void RegenStamina()
    {
        if(!staminaLock)
        {
            timer += Time.deltaTime;

            if(timer >= staminaRegenCD)
            {
                stamina += staminaRegenRate * Time.deltaTime;

                stamina = Mathf.Clamp(stamina, 0, maxStamina);

                staminaSlider.value = stamina;

            }
        }
        else
        {
            stamina = maxStamina;
            staminaSlider.value = stamina;
        }
    }
}
