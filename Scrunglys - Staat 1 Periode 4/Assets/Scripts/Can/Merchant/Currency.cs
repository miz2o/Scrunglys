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

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            AddCrystals(50);
        }
    }
    public void AddCrystals(float amount)
    {
        crystals += amount;

        currencyText.text = crystals.ToString();
    }
    public void SubtractCrystals(float amount)
    {
        if(crystals >= amount)
        {
            crystals -= amount;

        currencyText.text = crystals.ToString();
        }
      /*   crystals -= amount;

        currencyText.text = crystals.ToString(); */
    }
}
