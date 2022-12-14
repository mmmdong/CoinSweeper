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

    private async UniTask Start()
    {
        //await LoadPlayerDataAsync();
    }

    public async UniTask LoadPlayerDataAsync()
    {
        _player = await LoadJsonDataAsync();
    }

    private async UniTask<PlayerData> LoadJsonDataAsync()
    {
        if (_jsonPath == string.Empty)
            _jsonPath = Path.Combine(Application.persistentDataPath, "data.json");
        var jsonData = await File.ReadAllTextAsync(_jsonPath);
        var data = JsonUtility.FromJson<PlayerData>(jsonData);
        return data;
    }

    public async UniTask SavedataAsync()
    {
        var jsonData = JsonUtility.ToJson(_player, true);
        if (_jsonPath == string.Empty)
            _jsonPath = Path.Combine(Application.persistentDataPath, "data.json");
        await File.WriteAllTextAsync(_jsonPath, jsonData);
    }

    private void DeleteData()
    {
        if (_jsonPath == string.Empty)
            _jsonPath = Path.Combine(Application.persistentDataPath, "data.json");
        File.Delete(_jsonPath);
    }

    [Serializable]
    public class PlayerData
    {
        public long _currency;
        public int _stage;
    }
}