using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Transform piggy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obj"))
        {
            var coin = other.GetComponent<ObjectController>();
            UIManager.CalculateCurrency(coin.coinValue);
            DestroyCoin(coin);
            var prevScale = piggy.localScale;
            piggy.localScale = new Vector3(prevScale.x + 0.01f, prevScale.y + 0.01f, prevScale.z + 0.01f);
        }
    }

    private void DestroyCoin(ObjectController obj)
    {
        var rig = obj.GetComponent<Rigidbody>();
        rig.Sleep();
        ObjPool.instance.DestroyCoin(obj);
    }
}