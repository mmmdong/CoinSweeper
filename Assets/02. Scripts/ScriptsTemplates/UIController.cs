using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
namespace BaseFrame
{
    public class UIController : MonoBehaviour
    {
        public Text _text;
        Tween _tw;

        protected virtual async UniTask Awake()
        {
            await UniTask.WaitUntil(() => UIManager.instance != null);
        }

        protected virtual void TextEffect()
        {
            if (_tw != null)
                _tw.Kill();

            _text.transform.localScale = Vector3.one;
            _tw = _text.transform.DOScale(0.5f, 0.05f).SetRelative().SetLoops(2, LoopType.Yoyo);
        }
    }
}