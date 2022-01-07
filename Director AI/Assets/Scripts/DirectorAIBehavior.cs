using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DirectorAIBehavior : MonoBehaviour
{

    #region SINGLETON
    private static DirectorAIBehavior _instance;

    public static DirectorAIBehavior Instance
    {
        get
        {
            if (_instance == null && !_applicationQuiting)
            {
                _instance = FindObjectOfType<DirectorAIBehavior>();
                if (_instance == null)
                {
                    GameObject newObject = new GameObject("Singleton_DirectorAI");
                    _instance = newObject.AddComponent<DirectorAIBehavior>();
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
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    List<Spawnpoint> _spawnPoints = new List<Spawnpoint>();
    PlayerCharacter _playerCharacter = null;
    [SerializeField] int minimumAmountOfSpawnedEnemiesRange;
    [SerializeField] int maxAmountOfSpawnedEnemiesRange;
    int _amountOfEnemiesToSpawn;
    int _spawnedEnemies;
    enum State { buildUp, peak, relax };
    private State _state = State.buildUp;

    // Start is called before the first frame update
    void Start()
    {
        _spawnPoints = SpawnpointManager.Instance.SpawnPoints;
        _playerCharacter = FindObjectOfType<PlayerCharacter>();

        _amountOfEnemiesToSpawn = Random.Range(minimumAmountOfSpawnedEnemiesRange, maxAmountOfSpawnedEnemiesRange);
    }

    // Update is called once per frame
    void Update()
    {
        if(_state == State.buildUp)
        {
            while (_spawnedEnemies < _amountOfEnemiesToSpawn)
            {
                int index = Random.Range(0, _spawnPoints.Count);
                _spawnedEnemies++;
                _spawnPoints[index].Spawn();
                Debug.Log("Spawned enemy: " + _spawnedEnemies);
            }
        }
    }

    public void DecreaseEnemiesAlive()
    {
        _spawnedEnemies = _spawnedEnemies - 1;
        Debug.Log("dead");
    }
}
