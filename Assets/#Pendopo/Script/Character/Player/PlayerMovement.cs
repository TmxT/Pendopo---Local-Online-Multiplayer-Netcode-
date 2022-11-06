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
        [SerializeField] PlayerNetwork Player;

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
            Vector3 move = new Vector3(moveDir.x * moveSpeed, 0, moveDir.y * moveSpeed) * (revers ? -1 : 1);
            
            rb.velocity = move;

            if (moveDir != Vector2.zero)
                model.rotation = Quaternion.Slerp(model.rotation, Quaternion.LookRotation(move), Time.deltaTime * rotationSpeed);

            base.Move();
        }
    }
}