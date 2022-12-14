using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScripts : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CoRotate());
    }

    IEnumerator CoRotate()
    {
        while (true)
        {
            transform.Rotate(Vector3.down, GameManager.instance.speed * Time.deltaTime);

            yield return null;
        }
    }

}
