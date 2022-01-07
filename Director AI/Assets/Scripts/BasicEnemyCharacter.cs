using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyCharacter : BasicCharacter
{
    private GameObject _playerTarget = null;
    [SerializeField] private float _attackRange = 2.0f;
    private float _attackTimer = 0.0f;
    [SerializeField] float _attackTime = 0.3f;
    PlayerCharacter _player = null;


    private void Start()
    {
        //expensive method, use with caution
        _player = FindObjectOfType<PlayerCharacter>();
        

        if (_player) _playerTarget = _player.gameObject;
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
                _player.Intensity += 0.05f;
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
}
