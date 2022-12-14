using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextObjPool : MonoBehaviour
{
    public static TextObjPool instance;
    public int _capacity;

    [SerializeField] private GameObject _textPref;

    private Queue<TextController> _textQue = new Queue<TextController>();
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
            AddTextToQueue();
        }
    }



    private void AddTextToQueue()
    {
        var text = Instantiate(_textPref, _defaultSpawnPos, Quaternion.identity, transform).GetComponent<TextController>();

        text.gameObject.SetActive(false);

        _textQue.Enqueue(text);
    }

    public TextController Spawn(Vector2 spawnPos, int value)
    {
        if (_textQue.Count == 0)
        {
            _capacity++;
            AddTextToQueue();
        }

        var text = _textQue.Dequeue();

        text.gameObject.SetActive(true);

        text.transform.position = spawnPos;
        text.ValueChange(value);
        
        text.TextEffect();


        return text;
    }

    public void DestroyText(TextController text)
    {
        //큐브가 파괴되는 것이 아닌 풀로 돌려보냄
        text.TextInitialize();
        text.transform.position = Vector2.zero;
        text.gameObject.SetActive(false);
        _textQue.Enqueue(text);
    }
}
