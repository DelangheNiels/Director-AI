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

    private List<Spawnpoint> _normalSpawnPoints = new List<Spawnpoint>();
    private List<Spawnpoint> _specialSpawnPoints = new List<Spawnpoint>();

    public List<Spawnpoint> NormalSpawnPoints
    {
        get { return _normalSpawnPoints; }
    }

    public List<Spawnpoint> SpecialSpawnPoints
    {
        get { return _specialSpawnPoints; }
    }

    public void RegisterSpawnPoint(Spawnpoint spawnpoint)
    {
        if(spawnpoint.IsSpecial == false)
        {
            if (!_normalSpawnPoints.Contains(spawnpoint))
            {
                _normalSpawnPoints.Add(spawnpoint);
            }
        }

        else
        {
            if (!_specialSpawnPoints.Contains(spawnpoint))
            {
                _specialSpawnPoints.Add(spawnpoint);
            }
        }
        
    }

    public void UnRegisterSpawnPoint(Spawnpoint spawnpoint)
    {
        if(spawnpoint.IsSpecial == false)
        {
            _normalSpawnPoints.Remove(spawnpoint);
        }
        else
        {
            _specialSpawnPoints.Remove(spawnpoint);
        }
        
    }

    private void Update()
    {
        _normalSpawnPoints.RemoveAll(s => s == null);
    }
}
