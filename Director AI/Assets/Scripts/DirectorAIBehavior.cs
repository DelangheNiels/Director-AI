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

    float _spawnTimer = 5.0f;
    [SerializeField]float _spawnTime = 5.0f;

    float _difficultyChangeTimer = 0.0f;
    float _difficultyChangeTime = 10.0f;

    float _oldIntensity = 0.0f;

    int _spawnEnemiesInPeakCounter = 0;

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
        if(_spawnTimer < _spawnTime)
        {
            _spawnTimer += Time.deltaTime;
        }

        if(_state == State.buildUp)
        {
            SpawnEnemies();
            StateChange();
            ChangeDifficulty();
        }

        if(_state == State.peak)
        {
            if(_spawnEnemiesInPeakCounter == 0)
            {
                _spawnEnemiesInPeakCounter++;
                SpawnEnemiesInPeak();
            }
        }
    }

    public void DecreaseEnemiesAlive()
    {
        _spawnedEnemies = _spawnedEnemies - 1;
    }

    private void SpawnEnemies()
    {
        if (_spawnTimer >= _spawnTime)
        {
            while (_spawnedEnemies < _amountOfEnemiesToSpawn)
            {
                int index = Random.Range(0, _spawnPoints.Count);
                _spawnedEnemies++;
                _spawnPoints[index].Spawn();
                
            }
            _spawnTimer = 0;
        }
    }

    private void StateChange()
    {
        if (_playerCharacter.Intensity >= 0.80f)
        {
            _state = State.peak;
            Debug.Log("In peak");
        }

        if(_state == State.peak)
        {
            _state = State.relax;
            Debug.Log("In relax state");
        }
    }

    private void ChangeDifficulty()
    {
        _difficultyChangeTimer += Time.deltaTime;
        if(_difficultyChangeTimer >= _difficultyChangeTime)
        {
            Debug.Log("old: " + _oldIntensity + " new: " + _playerCharacter.Intensity);
            if(_playerCharacter.Intensity - _oldIntensity < 0.20)
            {
                _amountOfEnemiesToSpawn += 8;
                Debug.Log("Increase difficulty");
            }

            else
            {
                _amountOfEnemiesToSpawn -= 4;
                Debug.Log("Decrease difficulty");
            }

            _oldIntensity = _playerCharacter.Intensity;
            _difficultyChangeTimer = 0;
        }
    }

    private void SpawnEnemiesInPeak()
    {
        int extraToSpawn = 40;
        int spawned = 0;
        while (spawned < extraToSpawn)
        {
            int index = Random.Range(0, _spawnPoints.Count);
            _spawnedEnemies++;
            _spawnPoints[index].Spawn();
            spawned++;

        }
    }
}
