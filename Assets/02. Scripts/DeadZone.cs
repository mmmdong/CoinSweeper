using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeadZone : MonoBehaviour
{
    public Transform piggy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obj"))
        {
            var coin = other.GetComponent<ObjectController>();
            //UIManager.CalculateCurrency(coin.coinValue);

            ButtonManager.instance.piggySellBtn.piggyValue.Value += coin.coinValue;

            DestroyCoin(coin);
            var prevScale = piggy.localScale;
            piggy.localScale = new Vector3(prevScale.x + 0.01f * coin.coinValue, prevScale.y + 0.01f * coin.coinValue, prevScale.z + 0.01f * coin.coinValue);
        }
    }

    private void DestroyCoin(ObjectController obj)
    {
        var rig = obj.GetComponent<Rigidbody>();
        rig.Sleep();
        ObjPool.instance.DestroyCoin(obj);
    }

    public void InitPiggy()
    {
        piggy.localScale = new Vector3(1f, 1f, 1f);
    }
}