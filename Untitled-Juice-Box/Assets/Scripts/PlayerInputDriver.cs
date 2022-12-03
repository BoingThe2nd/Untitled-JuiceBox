using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputDriver : MonoBehaviour
{

    public enum PlayerNumber {One, Two, Three, Four }
    public PlayerNumber m_PlayerNumber;

    //This is the Post Removal of Fish-Net Script
    private CharacterController m_CharacterController;
    private Vector2 m_MoveInput;
    private Vector3 m_MoveDirection;
    //private bool _jump; Will not being using Jump But use as reference for other input bases
    //[SerializeField] public float jumpSpeed = 6f;
    [SerializeField] public float speed = 8f;
    // Start is called before the first frame update

    void Start()
    {
        m_CharacterController = GetComponent(typeof(CharacterController)) as CharacterController;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!base.IsOwner)
        //    return;
        if (m_CharacterController.isGrounded)
        {
            m_MoveDirection = new Vector3(m_MoveInput.x, 0.0f, m_MoveInput.y);
            m_MoveDirection *= speed;

            //if (_jump)
            //{
            //    _moveDirection.y = jumpSpeed;
            //    _jump = false;
            //}
        }
        m_CharacterController.Move(m_MoveDirection * Time.deltaTime);
    }

    #region UnityEventCallbacks
    public void OnMovement(InputAction.CallbackContext context)
    {
        //if (!base.IsOwner)
        //    return;
        m_MoveInput = context.ReadValue<Vector2>();
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