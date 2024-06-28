using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItems : MonoBehaviour
{
    [Header("References")]
    public PlayerStats playerStats;
    public InteractMerchant interactMerchant;
    public Currency currency;
    public GameObject dagger;
    public GameObject sword;
    public GameObject claymore;
    public GameObject stick;

    public GameObject branchUI, daggerUI, swordUI, claymoreUI;

    public GameObject merchantPanel;

    public AudioClip bling;
    public float volumeBling;
    private float pitchBling;
    public float pitchMin;
    public float pitchMax;


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

        SFXManager.instance.PlaySFXClip(bling, transform, volumeBling, Pitch());
    }
    public void BuyDagger()
    {
        if (!boughtDagger && currency.crystals >= daggerValue)
        {
            currency.SubtractCrystals(daggerValue);
            boughtDagger = true;

            SFXManager.instance.PlaySFXClip(bling, transform, volumeBling, Pitch());
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

            SFXManager.instance.PlaySFXClip(bling, transform, volumeBling, Pitch());
        }
    }
    public void BuySword()
    {
        if (!boughtSword && currency.crystals >= swordValue)
        {
            currency.SubtractCrystals(swordValue);

            boughtSword = true;

            SFXManager.instance.PlaySFXClip(bling, transform, volumeBling, Pitch());
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

            SFXManager.instance.PlaySFXClip(bling, transform, volumeBling, Pitch());
        }
    }
    public void BuyClaymore()
    {
        if (!boughtClaymore && currency.crystals >= claymoreValue)
        {
            currency.SubtractCrystals(claymoreValue);

            boughtClaymore = true;

            SFXManager.instance.PlaySFXClip(bling, transform, volumeBling, Pitch());
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

            SFXManager.instance.PlaySFXClip(bling, transform, volumeBling, Pitch());
        }
    }

    public void ExitShop()
    {
        merchantPanel.SetActive(false);
        interactMerchant.shopping = false;
        Time.timeScale = 1;

        SFXManager.instance.PlaySFXClip(bling, transform, volumeBling, Pitch());
    }
    public float Pitch()
    {
        pitchBling = Random.Range(pitchMin, pitchMax);

        return pitchBling;
    }  
}
