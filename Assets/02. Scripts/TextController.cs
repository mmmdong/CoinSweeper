using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TextController : MonoBehaviour
{
    public TextMesh valueTxt;
    public TextMesh shadow;

    private FloatReactiveProperty alpha = new FloatReactiveProperty(1f);

    private void Awake()
    {
        alpha.TakeUntilDestroy(this).Subscribe(x =>
        {
            var color = valueTxt.color;
            color.a = x;
            valueTxt.color = color;

            var shadowColor = shadow.color;
            shadowColor.a = x;
            shadow.color = shadowColor;

            if (x <= 0f)
                TextObjPool.instance.DestroyText(this);
        });
    }

    public void ValueChange(int val)
    {
        valueTxt.text = $"${val}";
        shadow.text = valueTxt.text;
    }

    public void TextInitialize(){
        transform.localPosition = Vector3.zero;
        alpha.Value = 1f;
    }

    private IEnumerator CoTextEffect()
    {
        while (alpha.Value > 0f)
        {
            alpha.Value -= Time.deltaTime;
            transform.position += Vector3.up * 4f * Time.deltaTime;
            yield return null;
        }
    }
    public void TextEffect(){
        StartCoroutine(CoTextEffect());
    }
}
