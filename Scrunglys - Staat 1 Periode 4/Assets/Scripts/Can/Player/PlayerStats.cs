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
    public bool healing = false;

    [Header("Cooldowns")]
    public float timer;
    public float staminaRegenCD;
    public float resetStaminaTimer;

    [Header("References")]
    public Movement movement;
    public Animator animator;

    public GameObject potionModel;

    public Slider healthSlider;
    public Slider staminaSlider;

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
    }
    void Heal()
    {
        if(potions != 0)
        {
            animator.SetTrigger("Drink");

            health += potionValue;

            healthSlider.value = health;

            healing = true;
            potionModel.SetActive(true);

            potions--;

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

        healthSlider.value = health;

        if (health <= 0)
        {
            //Death();
        }
    }
    public void Stamina(float actionStamina)
    {
        if(Time.timeScale > 0)
        {
            stamina -= actionStamina;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);

            staminaSlider.value = stamina;

            resetStaminaTimer = Time.time;
        }
    }
    public void SprintStamina(float sprintStamina)
    {
        stamina -= sprintStamina * Time.deltaTime;

        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        staminaSlider.value = stamina;
        resetStaminaTimer = Time.time;
    }
    void RegenStamina()
    {
        timer += Time.deltaTime;

        if(timer >= resetStaminaTimer + staminaRegenCD)
        {
            stamina += staminaRegenRate * Time.deltaTime;

            stamina = Mathf.Clamp(stamina, 0, maxStamina);

            staminaSlider.value = stamina;
        }
    }
}
