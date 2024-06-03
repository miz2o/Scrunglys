using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickUp : MonoBehaviour
{
    public int value;
    public GameObject currency;
    private void Start()
    {
        currency = GameObject.FindWithTag("Currency");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            currency.GetComponent<Currency>().AddCrystals(value);
            Destroy(gameObject);
        }
    }
}
