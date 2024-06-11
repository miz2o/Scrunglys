using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BuyItems : MonoBehaviour
{
    [Header("References")]
    public PlayerStats playerStats;
    public Currency currency;

    public GameObject dagger;
    public GameObject sword;
    public GameObject claymore;
    public GameObject stick;

    public GameObject branchUI, daggerUI, swordUI, claymoreUI;

    public GameObject merchantPanel;


    [Header("weapon values")]
    public float daggerValue, swordValue, claymoreValue;

    [Header("Buyy Checks")]
    public bool boughtDagger, boughtSword, boughtClaymore;

    

    public void RefillPotions()
    {
        playerStats.potions = 3;
    }

    public void EquipStick()
    {
        stick.SetActive(true);
        branchUI.SetActive(true);

        sword.SetActive(false);
        dagger.SetActive(false);
        claymore.SetActive(false);

        swordUI.SetActive(false);
        daggerUI.SetActive(false);
        claymoreUI.SetActive(false);
    }
    public void BuyDagger()
    {
        if (!boughtDagger)
        {
            currency.SubtractCrystals(daggerValue);
            boughtDagger = true;
        }
    }

    public void EquipDagger()
    {
        if (boughtDagger)
        {
            dagger.SetActive(true);
            daggerUI.SetActive(true);

            sword.SetActive(false);
            stick.SetActive(false);
            claymore.SetActive(false);

            swordUI.SetActive(false);
            branchUI.SetActive(false);
            claymoreUI.SetActive(false);
        }
    }
    public void BuySword()
    {
        if (!boughtSword)
        {
            currency.SubtractCrystals(swordValue);

            boughtSword = true;
        }
    }
    public void EquipSword()
    {
        if (boughtSword)
        {
            sword.SetActive(true);
            swordUI.SetActive(true);

            dagger.SetActive(false);
            stick.SetActive(false);
            claymore.SetActive(false);
            
            daggerUI.SetActive(false);
            branchUI.SetActive(false);
            claymoreUI.SetActive(false);
        }
    }
    public void BuyClaymore()
    {
        if (!boughtClaymore)
        {
            currency.SubtractCrystals(claymoreValue);

            boughtClaymore = true;
        }
    }

    public void EquipClaymore()
    {
        if (boughtClaymore)
        {
            claymore.SetActive(true);
            claymoreUI.SetActive(true);

            dagger.SetActive(false);
            stick.SetActive(false);
            sword.SetActive(false);

            daggerUI.SetActive(false);
            branchUI.SetActive(false);
            swordUI.SetActive(false);
        }
    }

    public void ExitShop()
    {
        merchantPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
