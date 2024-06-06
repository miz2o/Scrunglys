using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CheckCurrentSword : MonoBehaviour
{
    public GameObject[] swords;
    public SwordScript swordScript;

   
    public void Update()
    {
        foreach (GameObject sword in swords)
        {
            if (sword.activeSelf)
            {
                swordScript = sword.GetComponent<SwordScript>();
            }
            if (!sword.activeSelf)
            {
                sword.GetComponent<SwordScript>().slashing = false;
            }
        }
    }
}
