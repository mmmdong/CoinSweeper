using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    public FloorController _floor;
    private void OnTriggerExit(Collider other)
    {
        var listCnt = ObjPool.instance.floorList.Count;
        if (other.CompareTag("Obj") && ObjPool.instance.floorList[listCnt - 1] != _floor)
        {
            var effectPos = other.transform.position;
            var coin = other.GetComponent<ObjectController>();
            var grade = _floor.floorNum;
            EffectManager.instance.PlayParticle(effectPos, Enums.ParticleName.DollarbillDirectional);
            UIManager.CalculateCurrency(grade + 1);
            coin.NextLevel(grade + 1);
        }
    }
}
