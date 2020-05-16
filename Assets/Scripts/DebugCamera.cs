using System.Collections;
using System.Collections.Generic;
using FirstPerson;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugCamera : MonoBehaviour
{
    public Transform playerCamera;

    public float cameraSensitivity = 10;
    public float climbSpeed = 4;
    public float normalMoveSpeed = 10;
    
    public float slowMoveFactor = 0.25f;
    public float fastMoveFactor = 3f;
    
    private float _currentSpeedFactor = 1f;
    private float _inputRotX;
    private float _inputRotY;
    private float _horizontalMovement;
    private float _verticalMovement;
    private float _upMovement;
    
    private float _rotationX;
    private float _rotationY;
    

    void Start()
    {
        _inputRotX = 0f;
        _inputRotY = 0f;
        
        _horizontalMovement = 0f;
        _verticalMovement = 0f;
    }
    
    void Update()
    {
        var transformations = transform;

        _rotationX += _inputRotX * cameraSensitivity * Time.deltaTime;
        _rotationY += _inputRotY * cameraSensitivity * Time.deltaTime;
        _rotationY = Mathf.Clamp(_rotationY, -90, 90);
        
        transform.localRotation =
            Quaternion.AngleAxis(_rotationX, Vector3.up) *
            Quaternion.AngleAxis(_rotationY, Vector3.left);
        
        transform.position +=
             (transformations.forward * CalculateTransformation(_verticalMovement)) +
             (transformations.right * CalculateTransformation(_horizontalMovement)) +
             (transformations.up * (_upMovement * climbSpeed * Time.deltaTime));
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValue<Vector2>();
        _horizontalMovement = value.x;
        _verticalMovement = value.y; CalculateTransformation(value.y);
    }
    
    public void OnAim( InputAction.CallbackContext ctx )
    {
        var value = ctx.ReadValue<Vector2>();
        _inputRotX = value.x;
        _inputRotY = value.y;
    }
    
    public void OnSpeedUp( InputAction.CallbackContext ctx )
    {
        if (ctx.performed)
            _currentSpeedFactor = fastMoveFactor;
        else
        {
            if (ctx.canceled)
                _currentSpeedFactor = 1f;
        }
    }

    public void OnSlowDown( InputAction.CallbackContext ctx )
    {
        if (ctx.performed)
            _currentSpeedFactor = slowMoveFactor;
        else
        {
            if (ctx.canceled)
                _currentSpeedFactor = 1f;
        }
    }

    // TODO: Implement
    public void OnAscend( InputAction.CallbackContext ctx )
    {
        if (ctx.performed)
            _upMovement = 1;
        else
        {
            if (ctx.canceled)
                _upMovement = 0;
        }
    }

    // TODO: Implement
    public void OnDescend( InputAction.CallbackContext ctx )
    {
        if (ctx.performed)
            _upMovement = -1;
        else
        {
            if (ctx.canceled)
                _upMovement = 0;
        }
    }

    public void OnEnter( InputAction.CallbackContext ctx )
    {
        if (ctx.performed)
        {
            ResetCameraPosition();
            gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(false);
        
            FirstPersonPlayer.LocalPlayerInstance.ChangePlayerActionMap("Debug");
        }

    }
    
    public void OnLeaveDebugCamera( InputAction.CallbackContext ctx )
    {
        if (ctx.performed)
        {
            playerCamera.gameObject.SetActive(true);
            gameObject.SetActive(false);
            FirstPersonPlayer.LocalPlayerInstance.ChangePlayerActionMap("Player");
        }
    }

    private void ResetCameraPosition()
    {
        transform.position = playerCamera.position;
        transform.rotation = playerCamera.rotation;
    }
    
    private float CalculateTransformation(float value)
    {
        return (normalMoveSpeed * _currentSpeedFactor) * value * Time.deltaTime;
    }
}
