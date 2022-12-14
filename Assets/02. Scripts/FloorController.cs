using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class FloorController : MonoBehaviour
{
    public GameObject wing;
    public MeshRenderer planeMeshRender;
    public ParticleSystem particle;

    public bool isFullUp;
    public int floorNum;
    public List<ParticleSystem> trail = new List<ParticleSystem>();
    private void Awake()
    {
        wing.SetActive(true);
        var trailTemp = wing.GetComponentsInChildren<ParticleSystem>();
        for (var i = 0; i < trailTemp.Length; i++)
        {
            trail.Add(trailTemp[i]);
        }


        GameManager.instance.isSpeedUp.Subscribe(x =>
        {
            for (var i = 0; i < trail.Count; i++)
            {
                trail[i].gameObject.SetActive(x);
                trail[i].Play();
            }
        });
    }

    public void MatColorChange()
    {
        var meshArr = wing.GetComponentsInChildren<MeshRenderer>();
        for (var i = 0; i < meshArr.Length; i++)
        {
            meshArr[i].material = ObjPool.instance.wingMat[floorNum];
        }
        for (var i = 0; i < trail.Count; i++)
        {
            var mainModule = trail[i].main;
            var trailColor = ObjPool.instance.wingMat[floorNum].color;
            mainModule.startColor = new Color(trailColor.r, trailColor.g, trailColor.b, 1f);
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(mainModule.startColor.color, 0.0f), new GradientColorKey(mainModule.startColor.color, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(0.2f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
            var color = trail[i].colorOverLifetime;
            color.color = grad;
        }

        planeMeshRender.material = ObjPool.instance.planeMat[floorNum];
    }
}
