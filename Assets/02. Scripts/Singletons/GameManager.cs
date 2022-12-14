using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;
using UniRx;

public class GameManager : SingleTon<GameManager>
{
    public float speed = 40f;
    public int countPerClick = 2;
    public float timeLimit = 5f;
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
        if(speed <= 40f)
        {
            speed = 40f;
            isSpeedUp.Value = false;    
        }
    }
}
