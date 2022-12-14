using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;


public class ObjPool : MonoBehaviour
{
    public static ObjPool instance;
    public int _capacity;

    public BoxCollider firstPlane;
    public List<FloorController> floorList = new List<FloorController>();
    [SerializeField] private GameObject _coinPref;

    public Mesh[] coinMeshes;
    public Material[] planeMat;
    public Material[] wingMat;
    private Queue<ObjectController> _coinQue = new Queue<ObjectController>();
    private Vector3 _defaultSpawnPos;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        _defaultSpawnPos = transform.position;
    }

    private void Start()
    {
        for (var i = 0; i < _capacity; i++)
        {
            AddCoinToQueue();
        }
    }



    private void AddCoinToQueue()
    {
        var coin = Instantiate(_coinPref, _defaultSpawnPos, Quaternion.identity, transform).GetComponent<ObjectController>();

        coin.gameObject.SetActive(false);

        _coinQue.Enqueue(coin);
    }

    public ObjectController Spawn()
    {
        if (_coinQue.Count == 0)
        {
            _capacity++;
            AddCoinToQueue();
        }

        var coin = _coinQue.Dequeue();

        coin.gameObject.SetActive(true);
        coin.transform.eulerAngles = new Vector3(Random.Range(-60f, 60f), Random.Range(-60f, 60f), Random.Range(-60f, 60f));

        coin.transform.position = Return_RandomPosition();


        return coin;
    }

    public void DestroyCoin(ObjectController coin)
    {
        //큐브가 파괴되는 것이 아닌 풀로 돌려보냄
        coin.CoinInitialize();
        coin.transform.position = Vector2.zero;
        coin.gameObject.SetActive(false);
        _coinQue.Enqueue(coin);
    }


    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = firstPlane.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = firstPlane.bounds.size.x;
        float range_Z = firstPlane.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
}



