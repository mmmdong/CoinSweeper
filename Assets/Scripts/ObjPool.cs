using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    public static ObjPool instance;

    private Queue<CoinController> _coinQue = new Queue<CoinController>();
    private Vector3 _defaultSpawnPos;
    [SerializeField] private GameObject _coinPref;

    public int _capacity;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        _defaultSpawnPos = transform.position;
    }

    private void Start()
    {
        for(var i = 0; i < _capacity; i++)
        {
            AddCoinToQueue();
        }
    }

    private void AddCoinToQueue()
    {
        var coin = Instantiate(_coinPref, _defaultSpawnPos, Quaternion.identity, transform).GetComponent<CoinController>();

        coin.gameObject.SetActive(false);
        
        _coinQue.Enqueue(coin);
    }
}
