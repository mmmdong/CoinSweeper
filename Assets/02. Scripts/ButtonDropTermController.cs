using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;

public class ButtonDropTermController : ButtonController
{
    [SerializeField] private float term;
    protected override void ClickAction()
    {
        base.ClickAction();
        var temp = (decimal)term;
        var tempLimit = (decimal)GameManager.instance.timeLimit;
        GameManager.instance.timeLimit = (float)(tempLimit - temp);
        if (GameManager.instance.timeLimit <= 0.5f)
        {
            _btn.interactable = false;
            _costText.text = "MAX";
        }
    }
}
