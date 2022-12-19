using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseFrame;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class UICurrencyController : UIController
{
    protected override async UniTask Awake()
    {
        await base.Awake();
        UIManager.currency.Subscribe(x =>
        {
            DataManager.instance._player._currency = x;
            ButtonManager.instance.addFloorBtn._btn.interactable = ButtonManager.instance.addFloorBtn._cost <= x;
            ButtonManager.instance.dropTermBtn._btn.interactable = ButtonManager.instance.dropTermBtn._cost <= x && ButtonManager.instance.dropTermBtn._costText.text != "MAX";
            
            _text.text = $"${UIManager.instance.ToCurrencyString(x)}";
            TextEffect();
        });
    }
}
