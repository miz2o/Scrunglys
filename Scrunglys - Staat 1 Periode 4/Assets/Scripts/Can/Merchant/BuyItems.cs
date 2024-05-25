using System.Collections;
using System.Collections.Generic;
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

    public void RefillPotions()
    {
        playerStats.potions = 3;
    }

    public void BuyDagger()
    {

    }
    public void BuySword()
    {

    }
    public void BuyClaymore()
    {

    }


}
