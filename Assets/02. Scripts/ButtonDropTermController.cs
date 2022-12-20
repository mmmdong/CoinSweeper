using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;

public class ButtonDropTermController : ButtonController
{
    [SerializeField] private float term;

    protected override void Start()
    {
        base.Start();
        if (!DataManager.instance._isData)
        {
            _btnLev = 1;
            DataManager.instance._player._spawnTermLev = _btnLev;
            _cost = 20;
            DataManager.instance._player._spawnTermCost = _cost;
            _costText.text = $"${UIManager.instance.ToCurrencyString(_cost)}";
        }
        else
        {
            _btnLev = DataManager.instance._player._spawnTermLev;
            _cost = DataManager.instance._player._spawnTermCost;
            _costText.text = $"${UIManager.instance.ToCurrencyString(_cost)}";

            var temp = (decimal)term * (_btnLev - 1);
            var tempLimit = (decimal)GameManager.instance.timeLimit;
            GameManager.instance.timeLimit = (float)(tempLimit - temp);

            if (GameManager.instance.timeLimit <= 0.5f)
        {
            _btn.interactable = false;
            _costText.text = "MAX";
        }
        }
    }

    protected override void ClickAction()
    {
        base.ClickAction();
        var temp = (decimal)term;
        var tempLimit = (decimal)GameManager.instance.timeLimit;
        GameManager.instance.timeLimit = (float)(tempLimit - temp);

        UIManager.CalculateCurrency(-_cost);
        _cost = _cost + 20 + _btnLev - 1;
        _costText.text = $"${UIManager.instance.ToCurrencyString(_cost)}";

        DataManager.instance._player._spawnTermCost = _cost;
        DataManager.instance._player._spawnTermLev = _btnLev;

        if (GameManager.instance.timeLimit <= 0.5f)
        {
            _btn.interactable = false;
            _costText.text = "MAX";
        }
    }
}
