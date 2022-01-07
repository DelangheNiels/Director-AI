using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacter : MonoBehaviour
{
    protected ShootingBehaviour _shootingBehaviour;
    protected MovementBehaviour _movementBehaviour;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _shootingBehaviour = GetComponent<ShootingBehaviour>();
        _movementBehaviour = GetComponent<MovementBehaviour>();
    }
}

