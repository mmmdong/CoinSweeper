using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
namespace BaseFrame
{
    public class UIController : MonoBehaviour
    {
        public Text _text;

        protected virtual async UniTask Awake()
        {
            await UniTask.WaitUntil(() => UIManager.instance != null);
        }
    }
}