using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;
using Cysharp.Threading.Tasks;

public class ButtonSpeedUpController : ButtonController
{
    protected override void ClickAction()
    {
        base.ClickAction();
        GameManager.instance.speed += 10f;

        if (GameManager.instance.speed >= 100f){
            _btn.interactable = false;
            _costText.text = "MAX";
        }
    }
}
