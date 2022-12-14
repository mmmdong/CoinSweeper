using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : TriggerController
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Coin"))
        {            
            coin.StopRotation();
        }
    }
}
