using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField]
    private GameObject SpawnTemplate = null;

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
