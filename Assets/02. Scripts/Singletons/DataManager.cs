using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.IO;
using BaseFrame;
using System;
using MondayOFF;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData _player = new PlayerData();
    private string _jsonPath;

    public bool _isData;

    private void Awake()
    {
        if (instance == null) instance = this;
        LoadPlayerData();
    }

#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        Savedata();
    }
#else
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            Savedata();
    }
#endif


    public void LoadPlayerData()
    {
        _player = LoadJsonData();
    }

    private PlayerData LoadJsonData()
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath, "data.json")))
        {
            _isData = false;
            EventTracker.ClearStage(0);
            EventTracker.TryStage(1);

            Debug.Log("----------------------------------------------------Empty----------------------------------------------------");

            return new PlayerData();
        }
        else
        {
            _jsonPath = Path.Combine(Application.persistentDataPath, "data.json");

            var jsonData = File.ReadAllText(_jsonPath);

            var data = JsonUtility.FromJson<PlayerData>(jsonData);
            _isData = true;
            Debug.Log("----------------------------------------------------Load----------------------------------------------------");
            return data;
        }
    }

    public void Savedata()
    {
        _player._currency = UIManager.currency.Value;

        var jsonData = JsonUtility.ToJson(_player, true);
        if (_jsonPath == null)
            _jsonPath = Path.Combine(Application.persistentDataPath, "data.json");
        File.WriteAllText(_jsonPath, jsonData);
        Debug.Log("----------------------------------------------------Save----------------------------------------------------");
    }

    
    private void DeleteData()
    {
        if (_jsonPath == null)
            _jsonPath = Path.Combine(Application.persistentDataPath, "data.json");
        File.Delete(_jsonPath);
    }

    [Serializable]
    public class PlayerData
    {
        public long _currency;
        public int _stage = 1;

        public long _addFloorCost;
        public long _sellCost;
        public int _coinCount;
        public long _spawnTermCost;
        public int _spawnTermLev;
    }
}