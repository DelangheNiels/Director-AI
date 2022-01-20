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

    List<Spawnpoint> _normalSpawnPoints = new List<Spawnpoint>();
    List<Spawnpoint> _specialSpawnPoints = new List<Spawnpoint>();

    PlayerCharacter _playerCharacter = null;

    [SerializeField] int minimumAmountOfSpawnedEnemiesRange;
    [SerializeField] int maxAmountOfSpawnedEnemiesRange;

    int _amountOfEnemiesToSpawn;
    int _spawnedEnemies;

    int _amountOfEnemiesToSpawnInPeak = 30;
    int _amountOfSpecialEnemiesToSpawnInPeak = 2;

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
        _normalSpawnPoints = SpawnpointManager.Instance.NormalSpawnPoints;
        _specialSpawnPoints = SpawnpointManager.Instance.SpecialSpawnPoints;

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
                int index = Random.Range(0, _normalSpawnPoints.Count);
                _spawnedEnemies++;
                _normalSpawnPoints[index].Spawn();
                
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

        if(_state == State.peak && _spawnedEnemies == 0)
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
                _amountOfEnemiesToSpawnInPeak += 5;
                _amountOfSpecialEnemiesToSpawnInPeak += 2;
                Debug.Log("Increase difficulty");
            }

            else
            {
                _amountOfEnemiesToSpawn -= 4;
                _amountOfEnemiesToSpawnInPeak -= 3;
                _amountOfSpecialEnemiesToSpawnInPeak -= 1;
                Debug.Log("Decrease difficulty");
            }

            _oldIntensity = _playerCharacter.Intensity;
            _difficultyChangeTimer = 0;
        }
    }

    private void SpawnEnemiesInPeak()
    {
        int normalEnemiesSpawned = 0;
        int specialEnemiesSpawned = 0;

        Debug.Log("Spaning " + _amountOfSpecialEnemiesToSpawnInPeak + " special enemies in peak and " + _amountOfEnemiesToSpawnInPeak + " normal enemies");

        while (normalEnemiesSpawned < _amountOfEnemiesToSpawnInPeak)
        {
            int index = Random.Range(0, _normalSpawnPoints.Count);
            _spawnedEnemies++;
            _normalSpawnPoints[index].Spawn();
            normalEnemiesSpawned++;

        }

        while(specialEnemiesSpawned < _amountOfSpecialEnemiesToSpawnInPeak)
        {
            int index = Random.Range(0, _specialSpawnPoints.Count);
            _spawnedEnemies++;
            _specialSpawnPoints[index].Spawn();
            specialEnemiesSpawned++;
        }
    }
}
