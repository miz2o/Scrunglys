using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public float crystals;

    public void AddCrystals(float amount)
    {
        crystals += amount;
    }
    public void SubtractCrystals(float amount)
    {
        crystals -= amount;
    }
}
