using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;
using UniRx;
using Cysharp.Threading.Tasks;

public class UICurrencyController : UIController
{
    protected override async UniTask Awake()
    {
        await base.Awake();
        
        UIManager.currency.Subscribe(x =>
        {
            _text.text = $"${UIManager.instance.ToCurrencyString(x)}";
        });
    }
}
