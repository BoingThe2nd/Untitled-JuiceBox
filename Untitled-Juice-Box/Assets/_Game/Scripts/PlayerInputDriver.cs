using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputDriver : NetworkBehaviour
{
    //Some Details Omitted During Basic Fish-Net Testing Can Delete or Comment Out This Dec.3rd 2022 Version is on Git hub Committ 
    private CharacterController _characterController;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    //private bool _jump; Will not being using Jump But use as reference for other input bases
    //[SerializeField] public float jumpSpeed = 6f;
    [SerializeField] public float speed = 8f;
    [SerializeField] public float gravity = -9.8f;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent(typeof(CharacterController)) as CharacterController;
    }

    // Update is called once per frame
    void Update()
    {
        if (!base.IsOwner)
            return;
        if (_characterController.isGrounded)
        {
            _moveDirection = new Vector3(_moveInput.x, 0.0f, _moveInput.y);
            _moveDirection *= speed;

            //if (_jump)
            //{
            //    _moveDirection.y = jumpSpeed;
            //    _jump = false;
            //}
        }
        _moveDirection.y += gravity * Time.deltaTime;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    #region UnityEventCallbacks
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!base.IsOwner)
            return;
        _moveInput = context.ReadValue<Vector2>();
    }
    //public void OnJump(InputAction.CallbackContext context)
    //{
    //    if (!base.IsOwner)
    //        return;
    //    if (context.started || context.performed)
    //    {
    //        _jump = true;
    //    }
    //    else if (context.canceled)
    //    {
    //        _jump = false;
    //    }
    //}
    #endregion
}
