using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : TriggerController
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);


        if (gameObject.CompareTag("Left"))
        {
            coin.StartRotation(1);
        }
        else
        {
            coin.StartRotation(-1);
        }
    }
    
}
