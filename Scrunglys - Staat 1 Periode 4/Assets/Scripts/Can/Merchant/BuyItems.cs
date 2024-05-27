using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BuyItems : MonoBehaviour
{
    [Header("References")]
    public PlayerStats playerStats;
    public Currency currency;

    public float daggerValue, swordValue, claymoreValue;

    public GameObject dagger;
    public GameObject sword;
    public GameObject claymore;
    public GameObject stick;

    public void RefillPotions()
    {
        playerStats.potions = 3;
    }

    public void BuyDagger()
    {
        currency.crystals -= daggerValue;
    }

    public void EquipDagger()
    {
        dagger.SetActive(true);

        sword.SetActive(false);
        stick.SetActive(false);
        claymore.SetActive(false);
    }
    public void BuySword()
    {
        currency.crystals -= swordValue;
    }
    public void EquipSword()
    {
        sword.SetActive(true);

        dagger.SetActive(false);
        stick.SetActive(false);
        claymore.SetActive(false);
    }
    public void BuyClaymore()
    {
        currency.crystals -= claymoreValue;
    }

    public void EquipClaymore()
    {
        claymore.SetActive(true);

        dagger.SetActive(false);
        stick.SetActive(false);
        sword.SetActive(false);
    }


}
