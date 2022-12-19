using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;

public class ButtonAddFloorController : ButtonController
{
    public Camera _cam;
    public Transform _pipe, _jar;
    public GameObject _floorPref;
    public Transform _firstFloor, _par;

    public int _floorCount;

    protected override void Start()
    {
        base.Start();
        _cost = 100;
        _costText.text = $"${UIManager.instance.ToCurrencyString(_cost)}";
    }

    protected override void ClickAction()
    {
        base.ClickAction();
        _floorCount++;
        _cam.orthographicSize += 2f;
        _cam.transform.position += Vector3.down * 2.25f;
        _pipe.position += Vector3.down * 3.5f;
        _jar.position += Vector3.down * 3.5f;

        var prevPos = _firstFloor.position;
        var spawnPos = new Vector3(prevPos.x, prevPos.y - 3.5f * ObjPool.instance.floorList.Count, prevPos.z);
        var floor = Instantiate(_floorPref, spawnPos, Quaternion.identity, _par).GetComponent<FloorController>();


        ObjPool.instance.floorList.Add(floor);
        var prevFloor = ObjPool.instance.floorList[ObjPool.instance.floorList.Count - 2];
        floor.wing.transform.eulerAngles = new Vector3(0f, prevFloor.wing.transform.eulerAngles.y + 15f, 0f);


        floor.floorNum = _btnLev;
        floor.MatColorChange();
        floor.transform.eulerAngles = Vector3.down * 180f * (ObjPool.instance.floorList.Count - 1);

        var payCost = _cost;
        _cost = (long)((100 * (_btnLev + 1)) * 5 * Mathf.Pow(_btnLev, 2f));
        _costText.text = $"${UIManager.instance.ToCurrencyString(_cost)}";
        UIManager.CalculateCurrency(-payCost);

        GameManager.instance.stageLev.Value = _btnLev;

        if (_btnLev >= 8)
        {
            _btn.interactable = false;
            _costText.text = "MAX";
        }
    }
}
