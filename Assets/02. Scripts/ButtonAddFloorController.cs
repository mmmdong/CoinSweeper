using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;
using Cysharp.Threading.Tasks;

public class ButtonAddFloorController : ButtonController
{
    public Camera _cam;
    public Transform _pipe, _jar;
    public GameObject _floorPref;
    public Transform _firstFloor, _par;

    public int _floorCount;

    protected override async UniTask Awake()
    {
        await base.Awake();

        if (!DataManager.instance._isData)
        {
            _btnLev = 1;
            _floorCount = _btnLev;
            _cost = 100;
            _costText.text = $"${UIManager.instance.ToCurrencyString(_cost)}";
        }
        else
        {
            _btnLev = DataManager.instance._player._stage;
            _floorCount = _btnLev;

            if (_btnLev == 1)
            {
                _cost = 100;
                _costText.text = $"${UIManager.instance.ToCurrencyString(_cost)}";
            }
            else
            {
                _cost = DataManager.instance._player._addFloorCost;
                _costText.text = $"${UIManager.instance.ToCurrencyString(_cost)}";
                if (_btnLev >= 9)
                {
                    _btn.interactable = false;
                    _costText.text = "MAX";
                }
            }

            for (var i = 1; i < _btnLev; i++)
            {
                InstantiateFloor(i);
            }
        }

        _btn.interactable = UIManager.currency.Value >= _cost && ButtonManager.instance.addFloorBtn._costText.text != "MAX";
    }

    protected override void ClickAction()
    {
        base.ClickAction();
        _floorCount++;

        InstantiateFloor(_floorCount - 1, true);

        var payCost = _cost;
        _cost = (long)((100 * (_btnLev)) * 5 * Mathf.Pow(_btnLev - 1, 2f));
        _costText.text = $"${UIManager.instance.ToCurrencyString(_cost)}";
        UIManager.CalculateCurrency(-payCost);

        DataManager.instance._player._stage = _btnLev;
        DataManager.instance._player._addFloorCost = _cost;

        if (_btnLev >= 9)
        {
            _btn.interactable = false;
            _costText.text = "MAX";
        }
    }

    private void InstantiateFloor(int lev, bool isStart = false)
    {
        _cam.orthographicSize += 2f;
        _cam.transform.position += Vector3.down * 2.25f;
        _pipe.position += Vector3.down * 3.5f;
        _jar.position += Vector3.down * 3.5f;

        var prevPos = _firstFloor.position;
        var spawnPos = new Vector3(prevPos.x, prevPos.y - 3.5f * ObjPool.instance.floorList.Count, prevPos.z);
        var floor = Instantiate(_floorPref, spawnPos, Quaternion.identity, _par).GetComponent<FloorController>();
        if (isStart)
        {
            EffectManager.instance.PlayParticle(spawnPos, Enums.ParticleName.StunStarExplosion);
        }

        ObjPool.instance.floorList.Add(floor);
        var prevFloor = ObjPool.instance.floorList[ObjPool.instance.floorList.Count - 2];
        floor.wing.transform.eulerAngles = new Vector3(0f, prevFloor.wing.transform.eulerAngles.y + 15f, 0f);


        floor.floorNum = lev;
        floor.MatColorChange();
        floor.transform.eulerAngles = Vector3.down * 180f * (ObjPool.instance.floorList.Count - 1);
    }
}
