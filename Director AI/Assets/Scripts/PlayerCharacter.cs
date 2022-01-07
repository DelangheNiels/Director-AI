using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BasicCharacter
{

    const string MOVEMENT_HORIZONTAL = "MovementHorizontal";
    const string MOVEMENT_VERTICAL = "MovementVertical";
    const string GROUND_LAYER = "Ground";
    const string PRIMARY_FIRE = "PrimaryFire";
    const string SECONDARY_FIRE = "SecondaryFire";
    const string RELOAD = "Reload";

    private Plane _cursorMovementPlane;

    float _intensity = 0.0f;

    public float Intensity
    {
        get { return _intensity; }
        set { _intensity = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        _cursorMovementPlane = new Plane(Vector3.up, transform.position);
    }

    private void Update()
    {
        HandleMovementInput();
        HandleFireInput();
        
    }

    void HandleMovementInput()
    {
        if (_movementBehaviour == null)
            return;

        //movement
        float horizontalMovement = Input.GetAxis(MOVEMENT_HORIZONTAL);
        float verticalMovement = Input.GetAxis(MOVEMENT_VERTICAL);

        Vector3 movement = horizontalMovement * Vector3.right + verticalMovement * Vector3.forward;

        _movementBehaviour.DesiredMovementDirection = movement;

        //rotation
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 positionOfMouseInWorld = transform.position;

        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 100000.0f, LayerMask.GetMask(GROUND_LAYER)))
        {
            positionOfMouseInWorld = hitInfo.point;
        }
        else
        {
            _cursorMovementPlane.Raycast(mouseRay, out float distance);
            positionOfMouseInWorld = mouseRay.GetPoint(distance);
        }

        _movementBehaviour.DesiredLookatPoint = positionOfMouseInWorld;
    }


    void HandleFireInput()
    {
        if (_shootingBehaviour == null) return;

        //fire
        if (Input.GetAxis(PRIMARY_FIRE) > 0.0f)
            _shootingBehaviour.PrimaryFire();
        if (Input.GetAxis(SECONDARY_FIRE) > 0.0f)
            _shootingBehaviour.SecondaryFire();
        //reload
        if (Input.GetAxis(RELOAD) > 0.0f)
            _shootingBehaviour.Reload();
    }

}

