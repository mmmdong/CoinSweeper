using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [HideInInspector] public CoinController coin;
    virtual public void OnTriggerEnter(Collider other)
    {
        coin = other.GetComponent<CoinController>();
    }
}
