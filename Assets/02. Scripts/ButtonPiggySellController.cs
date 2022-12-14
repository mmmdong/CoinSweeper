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
    public LongReactiveProperty piggyValue = new LongReactiveProperty(0);

    protected override async UniTask Awake()
    {
        await base.Awake();
        piggyValue.Subscribe(x =>
        {
            text.text = $"${UIManager.instance.ToCurrencyString(x)}";
            if (x == 0)
            {
                _btn.interactable = false;
            }
            else
            {
                _btn.interactable = true;
            }
        });
    }

    protected override void ClickAction()
    {
        base.ClickAction();
        UIManager.CalculateCurrency(piggyValue.Value);
        deadZone.InitPiggy();
        piggyValue.Value = 0;
    }
}
