using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
/* using MoreMountains.NiceVibrations; */


namespace BaseFrame
{
    public class ButtonController : MonoBehaviour
    {
        [HideInInspector] public Button _btn;
        [SerializeField] protected int _btnLev;
        protected ButtonManager _manager;
        protected Text _costText;
        //public Text _cost;
        //public long _costValue;
        //[HideInInspector] public long _nextValue;
        //public long _buttonLev = 0;

        protected virtual async UniTask Awake()
        {
            _btn = GetComponent<Button>();
            _costText = GetComponentInChildren<Text>();
            _manager = GetComponentInParent<ButtonManager>();
            await UniTask.WaitUntil(() => GameManager.instance != null && DataManager.instance != null);
            _btn.onClick.AddListener(ClickAction);
            //_nextValue = _costValue;
        }

        virtual protected void Start()
        {
            //pass
        }

        virtual public void ButtonSetting()
        {

        }

        virtual protected void ClickAction()
        {
            //pass
            _btnLev++;
            /* Haptic(HapticTypes.MediumImpact); */
        }
        /* 
            private void Haptic(HapticTypes type)
            {
                MMVibrationManager.Haptic(type);
            } */
    }
}

