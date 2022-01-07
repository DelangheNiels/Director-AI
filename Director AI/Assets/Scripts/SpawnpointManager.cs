using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointManager : MonoBehaviour
{
    #region SINGLETON
    private static SpawnpointManager _instance;

    public static SpawnpointManager Instance
    {
        get
        {
            if(_instance == null && !_applicationQuiting)
            {
                _instance = FindObjectOfType<SpawnpointManager>();
                if(_instance == null)
                {
                    GameObject newObject = new GameObject("Singleton_SpawnpointManager");
                    _instance = newObject.AddComponent<SpawnpointManager>();
                }
            }

            return _instance;
        }
    }

    private static bool _applicationQuiting = false;

    public void OnApplicationQuit()
    {
        _applicationQuiting = true;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private List<Spawnpoint> _spawnPoints = new List<Spawnpoint>();
    public List<Spawnpoint> SpawnPoints
    {
        get { return _spawnPoints; }
    }

    public void RegisterSpawnPoint(Spawnpoint spawnpoint)
    {
        if(!_spawnPoints.Contains(spawnpoint))
        {
            _spawnPoints.Add(spawnpoint);
        }
    }

    public void UnRegisterSpawnPoint(Spawnpoint spawnpoint)
    {
        _spawnPoints.Remove(spawnpoint);
    }

    private void Update()
    {
        _spawnPoints.RemoveAll(s => s == null);
    }
}
