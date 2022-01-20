using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HudScript : MonoBehaviour
{

    [SerializeField] Text _health = null;
    [SerializeField] Text _ammo = null;

    private Health _playerHealth = null;
    private ShootingBehaviour _playerShooting = null;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player != null)
        {
            _playerHealth = player.GetComponent<Health>();
            _playerShooting = player.GetComponent<ShootingBehaviour>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SyncData();
    }

    void SyncData()
    {
        //health
        if(_playerHealth != null)
        {
            _health.text = _playerHealth.CurrentHealth.ToString();
        }

        //ammo
        if(_playerShooting != null)
        {
            _ammo.text = _playerShooting.Weapon.CurrentAmmo.ToString();
        }
    }
}
