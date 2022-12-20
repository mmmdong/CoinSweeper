using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.IO;
using BaseFrame;
using System;

public class DataManager : SingleTon<DataManager>
{
    public PlayerData _player = new PlayerData();
    private string _jsonPath;

    public bool _isData;

    public override void Awake()
    {
        base.Awake();
        LoadPlayerData();
    }


    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SavedataAsync().Forget();
    }

    private void OnApplicationQuit()
    {
        SavedataAsync().Forget();
    }

    public void LoadPlayerData()
    {
        _player = LoadJsonData();
    }

    private PlayerData LoadJsonData()
    {
        if (!File.Exists(Path.Combine(Application.dataPath, "data.json")))
        {
            _isData = false;
            return new PlayerData();
        }
        else
        {
            _jsonPath = Path.Combine(Application.dataPath, "data.json");

            var jsonData = File.ReadAllText(_jsonPath);

            var data = JsonUtility.FromJson<PlayerData>(jsonData);
            _isData = true;
            return data;
        }
    }

    public async UniTask SavedataAsync()
    {
        _player._currency = UIManager.currency.Value;

        var jsonData = JsonUtility.ToJson(_player, true);
        if (_jsonPath == null)
            _jsonPath = Path.Combine(Application.dataPath, "data.json");
        await File.WriteAllTextAsync(_jsonPath, jsonData);
    }

    private void DeleteData()
    {
        if (_jsonPath == null)
            _jsonPath = Path.Combine(Application.dataPath, "data.json");
        File.Delete(_jsonPath);
    }

    [Serializable]
    public class PlayerData
    {
        public long _currency;
        public int _stage = 1;

        public long _addFloorCost;
        public long _sellCost;
        public long _spawnTermCost;
        public int _spawnTermLev;
    }
}