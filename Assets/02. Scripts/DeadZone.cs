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
    private float[] limitSize = new float[] { 2f, 2.5f, 3f, 3.5f, 4f, 4.5f, 5f, 5.5f, 6f };

    private int coinCount;

    private void Start()
    {
        if (DataManager.instance._isData && DataManager.instance._player._coinCount != 0)
        {
            coinCount = DataManager.instance._player._coinCount;
            piggy.localScale = new Vector3(1f + 0.025f * coinCount, 1f + 0.025f * coinCount, 1f + 0.025f * coinCount);
            if (piggy.localScale.x >= limitSize[DataManager.instance._player._stage - 1])
            {
                piggy.transform.localScale = Vector3.one * limitSize[DataManager.instance._player._stage - 1];
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obj"))
        {
            coin = other.GetComponent<ObjectController>();

            ButtonManager.instance.piggySellBtn.piggyValue.Value += coin.coinValue;

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
            DataManager.instance._player._coinCount = coinCount;
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

    private IEnumerator CoInitPiggy()
    {
        coinCount = 0;
        DataManager.instance._player._coinCount = coinCount;
        GameManager.instance.isSelling = true;
        ButtonManager.instance.piggySellBtn.canvas.alpha = 0f;
        ButtonManager.instance.piggySellBtn._btn.interactable = false;
        var duration = EffectManager.instance.LoadParticle(Enums.ParticleName.DollarbillFountain).main.duration;
        piggy.DOScale(Vector3.one, duration);
        piggy.DOLocalRotate(Vector3.down * 360f, duration).SetRelative();
        yield return new WaitForSeconds(1.5f);
        float value = 0.025f * coinCount;
        piggy.DOScale(value, 0.5f).SetRelative();
        GameManager.instance.isSelling = false;
        if (ButtonManager.instance.piggySellBtn.piggyValue.Value > 0)
        {
            ButtonManager.instance.piggySellBtn.canvas.alpha = 1f;
            ButtonManager.instance.piggySellBtn._btn.interactable = true;
        }
    }

    private void PiggyEffect()
    {
        if (tw != null)
            tw.Kill();

        tw = piggy.transform.DOScale(0.1f, 0.025f).SetRelative().SetLoops(2, LoopType.Yoyo);
    }
}