using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    public float potionValue;

    [Header("Cooldowns")]
    public float timer;
    public float staminaRegenCD;
    public float resetStaminaTimer;

    [Header("References")]
    public Movement movement;
    public TMP_Text staminaText;
    public TMP_Text healthText;
    public TMP_Text potionsText;

    public void Update()
    {
        Inputs();
        RegenStamina();
        DisplayStats();
        CheckDeath();
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal();
        }
    }
    void Heal()
    {
        if(potions != 0)
        {
            health += potionValue;
            potions--;
        }
        health = Mathf.Clamp(health, 0, maxHealth);
    }
    public void Health(float damageToDo)
    {
        health -= damageToDo;
    }
    public void Stamina(float actionStamina)
    {
        if(Time.timeScale > 0)
        {
            stamina -= actionStamina;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            resetStaminaTimer = Time.time;
        }
    }
    public void SprintStamina(float sprintStamina)
    {
        stamina -= sprintStamina * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        resetStaminaTimer = Time.time;
    }
    void RegenStamina()
    {
        timer += Time.deltaTime;

        if(timer >= resetStaminaTimer + staminaRegenCD)
        {
            stamina += staminaRegenRate * Time.deltaTime;

            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }
    }
    void DisplayStats()
    {
        staminaText.text = stamina.ToString();
        healthText.text = health.ToString();
        potionsText.text = potions.ToString();
    }
    void CheckDeath()
    {
        if(health <= 0)
        {
            print("haha u dede");
            // Game Over screen 
        }
    }
}
