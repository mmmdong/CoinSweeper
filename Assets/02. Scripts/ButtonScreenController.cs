using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;

public class ButtonScreenController : ButtonController
{
    protected override void ClickAction()
    {
        base.ClickAction();

        GameManager.instance.isSpeedUp.Value = true;
        GameManager.instance.speed = 100f;

        var count = GameManager.instance.countPerClick;
        if (count == 1)
        {
            ObjPool.instance.Spawn();
            return;
        }
        for (var i = 0; i < count; i++)
        {
            ObjPool.instance.Spawn();
        }
    }
}
