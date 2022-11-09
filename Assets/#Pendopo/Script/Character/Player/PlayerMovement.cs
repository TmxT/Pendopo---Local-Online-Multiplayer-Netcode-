using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

using Pendopo.Event;
using Pendopo.Network;

namespace Pendopo.Character
{
    public class PlayerMovement : Movement
    {
        [SerializeField] protected Rigidbody rb;

        [Space]
        [SerializeField] PlayerNetwork Player;

        protected Vector2 moveDir;

        [Header("Reference")]
        private InputManager inputManager;

        protected override void Awake()
        {
            base.Awake();

            inputManager = new InputManager();

            inputManager.Player.Movement.Enable();
            inputManager.Player.Movement.performed += ActionMovement;
            inputManager.Player.Movement.canceled += ActionMovement;
        }

        private void ActionMovement(InputAction.CallbackContext _context)
        {
            if (EventGame.Instance.CurrentGame == EnumGame.Pause || !Player.IsOwner)
                return;

            moveDir = _context.ReadValue<Vector2>();
        }

        protected override void Move()
        {
            float speed = moveSpeed * 100 * Time.deltaTime;
            Vector3 move = new Vector3(moveDir.x * speed, 0, moveDir.y * speed) * (revers ? -1 : 1);
            
            rb.velocity = move;

            if (moveDir != Vector2.zero)
                model.rotation = Quaternion.Slerp(model.rotation, Quaternion.LookRotation(move), Time.deltaTime * rotationSpeed);

            isWalking = moveDir != Vector2.zero;

            base.Move();
        }
    }
}