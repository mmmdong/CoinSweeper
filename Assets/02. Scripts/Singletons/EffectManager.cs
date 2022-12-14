using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public ParticleSystem[] itemArr;
    public ParticleSystem _particle;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void PlayParticle(Vector2 pos, Color color, Enums.ParticleName file)
    {

        _particle = LoadParticle(file);

        var mainModule = _particle.main;

        mainModule.startColor = color;

        _particle.transform.position = pos;

        var col = _particle.colorOverLifetime;
        col.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(0.5f, 0.0f), new GradientAlphaKey(1.0f, 0.6f), new GradientAlphaKey(0.0f, 1.0f) });

        col.color = grad;

        _particle.Play();
    }

    public void PlayParticle(Vector3 pos, Enums.ParticleName file)
    {
        _particle = LoadParticle(file);

        var _particleMainModule = _particle.main;

        _particle.transform.position = pos;
        _particle.Play();
    }

    private ParticleSystem LoadParticle(Enums.ParticleName file)
    {
        var fileName = ChangeFileName(file);


        var itemArr = GetComponentsInChildren<ParticleSystem>();
        for (var i = 0; i < itemArr.Length; i++)
        {
            if (itemArr[i].name == $"{fileName}")
                return itemArr[i];
        }
        return null;
    }

    private string ChangeFileName(Enums.ParticleName a)
    {
        return a.ToString();
    }
}
