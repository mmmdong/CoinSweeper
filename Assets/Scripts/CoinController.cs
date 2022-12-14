using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UniRx;
using UnityEngine;


public class CoinController : MonoBehaviour
{
    public int _rotSpeed;
    public PathFollower _pathFollower;
    [HideInInspector] public int _originRotSpd;

    private Coroutine _coRot;

    private void Awake()
    {
        _pathFollower = GetComponent<PathFollower>();
    }
    private void Start()
    {
        _originRotSpd = _rotSpeed;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"> 1 = right, -1 = left</param>
    public void StartRotation(int direction)
    {
        _coRot = StartCoroutine(CoRotate(direction));
    }

    public void StopRotation()
    {
        if (_coRot != null)
            StopCoroutine(_coRot);
    }

    private IEnumerator CoRotate(int direction)
    {
        var eulerAng = transform.rotation.eulerAngles;
        while (true)
        {
            eulerAng.x += _rotSpeed * direction * Time.deltaTime * 100;
            transform.rotation = Quaternion.Euler(eulerAng);

            yield return null;
        }
    }
}
