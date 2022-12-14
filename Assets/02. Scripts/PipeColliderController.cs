using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeColliderController : MonoBehaviour
{
    private Transform[] colliders;


    private void Awake()
    {
        colliders = GetComponentsInChildren<Transform>();

        for (var i = 0; i < colliders.Length; i++)
        {
            colliders[i].localScale = new Vector3(1f + (i + 1) * 0.001f, 0f, 1f + (i + 1) * 0.001f);
        }
    }
}
