using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;

public class ButtonIncomeController : ButtonController
{
    public int incomeLev;
    public FloorController floor;


    protected override void Start()
    {
        base.Start();

        //floor.wings[incomeLev].SetActive(true);
    }

    protected override void ClickAction()
    {
        base.ClickAction();
        if (!floor.isFullUp)
        {
            incomeLev++;
        }
        else
        {
            var leng = ObjPool.instance.floorList.Count;
            for (var i = 0; i < leng; i++)
            {
                if (!ObjPool.instance.floorList[i].isFullUp)
                {
                    floor = ObjPool.instance.floorList[i];
                    incomeLev++;
                    break;
                }
            }
        }
        if (incomeLev >= 3)
        {
            floor.isFullUp = true;
            incomeLev = 0;
        }
        if (floor.floorNum == ObjPool.instance.floorList[ObjPool.instance.floorList.Count - 1].floorNum && floor.isFullUp)
        {
            _btn.interactable = false;
            if (floor.floorNum >= 8)
            {
                _costText.text = "MAX";
            }
        }
    }
}
