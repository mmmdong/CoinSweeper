using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;
using UniRx;

public class GameManager : SingleTon<GameManager>
{
    public float speed = 20f;
    public int countPerClick = 1;
    public float timeLimit = 15f;
    public bool isSelling;

    private float dropTime;

    public BoolReactiveProperty isSpeedUp = new BoolReactiveProperty(false);

    private void FixedUpdate()
    {
        dropTime += Time.deltaTime;

        if (dropTime >= timeLimit)
        {
            ObjPool.instance.Spawn();
            dropTime = 0f;
        }

        speed -= 40f * Time.deltaTime;
        if (speed <= 20f)
        {
            speed = 20f;
            isSpeedUp.Value = false;
        }
    }
}
