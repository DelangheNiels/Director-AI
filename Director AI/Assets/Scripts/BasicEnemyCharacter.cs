using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyCharacter : BasicCharacter
{
    private GameObject _playerTarget = null;
    [SerializeField] private float _attackRange = 2.0f;
    private float _attackTimer = 0;
    [SerializeField] float _attackTime = 0.5f;


    private void Start()
    {
        //expensive method, use with caution
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player) _playerTarget = player.gameObject;
    }

    private void Update()
    {
        HandleMovement();
        HandleAttacking();
    }

    void HandleMovement()
    {
        if (_movementBehaviour == null)
            return;

        _movementBehaviour.Target = _playerTarget;
    }

    void HandleAttacking()
    {
        if (_shootingBehaviour == null) return;

        if (_playerTarget == null) return;

        //if we are in range of the player, fire our weapon, 
        //use sqr magnitude when comparing ranges as it is more efficient
        if ((transform.position - _playerTarget.transform.position).sqrMagnitude
            < _attackRange * _attackRange)
        {
            _attackTimer += Time.deltaTime;
            if(_attackTimer >= _attackTime)
            {
                _shootingBehaviour.PrimaryFire();
                _shootingBehaviour.Reload();
                _attackTimer = 0;
            }

        }

        else
        {
            _attackTimer = 0;
        }
    }

    const string KILL_METHODNAME = "Kill";
    void Kill()
    {
        Destroy(gameObject);
    }
}
