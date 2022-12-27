using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseFrame;
using UniRx;
using Cysharp.Threading.Tasks;

public class ButtonPiggySellController : ButtonController
{
    public DeadZone deadZone;
    public Text text;
    public TextMesh piggyTxtMesh, txtMeshShadow;
    public LongReactiveProperty piggyValue;
    public CanvasGroup canvas;

    protected override void Start()
    {
        base.Start();

        piggyValue = new LongReactiveProperty(DataManager.instance._player._sellCost);

        piggyValue.Subscribe(x =>
        {
            DataManager.instance._player._sellCost = x;
            text.text = $"${UIManager.instance.ToCurrencyString(x)}";
            piggyTxtMesh.text = text.text;
            txtMeshShadow.text = text.text;
            if (x == 0)
            {
                canvas.alpha = 0f;
                _btn.interactable = false;
            }
            else if (x != 0 && !GameManager.instance.isSelling)
            {
                canvas.alpha = 1f;
                _btn.interactable = true;
            }
        });
    }

    protected override void ClickAction()
    {
        base.ClickAction();
        UIManager.CalculateCurrency(piggyValue.Value);
        EffectManager.instance.PlayParticle(deadZone.transform.position, Enums.ParticleName.DollarbillFountain);
        deadZone.InitPiggy();
        piggyValue.Value = 0;
    }
}
