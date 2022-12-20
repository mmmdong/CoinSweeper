using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeadZone : MonoBehaviour
{
    public Transform piggy;
    private Tween tw;
    private ObjectController coin;
    private Vector3 prevScale;
    private float[] limitSize = new float[] { 2f, 2.5f, 3f, 3.5f, 4f, 4.5f, 5f, 5.5f, 6f};

    private int coinCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obj"))
        {
            coin = other.GetComponent<ObjectController>();
            //UIManager.CalculateCurrency(coin.coinValue);

            ButtonManager.instance.piggySellBtn.piggyValue.Value += coin.coinValue;

            //prevScale = piggy.localScale;
            //piggy.localScale = new Vector3(prevScale.x + 0.01f * coin.coinValue, prevScale.y + 0.01f * coin.coinValue, prevScale.z + 0.01f * coin.coinValue);

            if (piggy.localScale.x >= limitSize[ButtonManager.instance.addFloorBtn._btnLev - 1] && !GameManager.instance.isSelling)
            {
                piggy.transform.localScale = Vector3.one * limitSize[ButtonManager.instance.addFloorBtn._btnLev - 1];
                PiggyEffect();
            }
            else if (piggy.localScale.x < limitSize[ButtonManager.instance.addFloorBtn._btnLev - 1] && !GameManager.instance.isSelling)
            {
                piggy.DOScale(0.025f, 0f).SetRelative();
            }

            EffectManager.instance.PlayParticle(transform.position, Enums.ParticleName.GoldCoinDirectional);
            coinCount++;
            DestroyCoin(coin);
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
        StartCoroutine(CoInitPiggy());
    }

    private IEnumerator CoInitPiggy(){
        coinCount = 0;
        GameManager.instance.isSelling = true;
        ButtonManager.instance.piggySellBtn._btn.interactable = false;
        var duration = EffectManager.instance.LoadParticle(Enums.ParticleName.DollarbillFountain).main.duration;
        piggy.DOScale(Vector3.one, duration);
        piggy.DOLocalRotate(Vector3.down * 360f, duration).SetRelative();
        yield return new WaitForSeconds(1.5f);
        float value = 0.025f * coinCount;
        piggy.DOScale(value, 0.5f).SetRelative();
        GameManager.instance.isSelling = false;
        if (ButtonManager.instance.piggySellBtn.piggyValue.Value > 0)
            ButtonManager.instance.piggySellBtn._btn.interactable = true;
    }

    private void PiggyEffect()
    {
        if (tw != null)
            tw.Kill();

        tw = piggy.transform.DOScale(0.1f, 0.025f).SetRelative().SetLoops(2, LoopType.Yoyo);
    }
}