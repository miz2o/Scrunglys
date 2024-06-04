using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public float crystals;

    public TMP_Text currencyText;

    public void Start()
    {
        currencyText.text = crystals.ToString();
    }
    public void AddCrystals(float amount)
    {
        crystals += amount;

        currencyText.text = crystals.ToString();
    }
    public void SubtractCrystals(float amount)
    {
        crystals -= amount;

        currencyText.text = crystals.ToString();
    }
}
