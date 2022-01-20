using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField]
    private GameObject SpawnTemplate = null;

    [SerializeField]
    private bool _isSpecial = false;

    public bool IsSpecial
    {
        get { return _isSpecial; }
    }

    private void OnEnable()
    {
        SpawnpointManager.Instance.RegisterSpawnPoint(this);
    }

    private void OnDisable()
    {
        SpawnpointManager.Instance.UnRegisterSpawnPoint(this);
    }

    public GameObject Spawn()
    {
        return Instantiate(SpawnTemplate, transform.position, transform.rotation);
    }
}
