using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

using Pendopo.UI;

namespace Pendopo
{
    public class Tools : NetworkBehaviour
    {
        private bool canInteract;

        [Header("Reference")]
        private InputManager inputManager;
        private PanelCookingTools cookingTools;

        protected void Awake()
        {
            inputManager = new InputManager();
            cookingTools = GetComponent<PanelCookingTools>();

            inputManager.Player.Interact.Enable();
            inputManager.Player.Interact.started += Interact;
        }

        private void Interact(InputAction.CallbackContext _context)
        {
            if(_context.started && canInteract)
            {
                Debug.Log("Calling Server RPC");
                InteractServerRpc();//cookingTools.StartLoadingServerRpc();
            }
        }

        private void OnTriggerEnter(Collider _col)
        {
            if (_col.CompareTag(EnumTag.PlayerCharacter.ToString()))
            {
                canInteract = true;
            }
        }

        private void OnTriggerExit(Collider _col)
        {
            if (_col.CompareTag(EnumTag.PlayerCharacter.ToString()))
            {
                canInteract = false;
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void InteractServerRpc()
        {
            Debug.Log("Calling Client RPC");
            cookingTools.StartLoadingClientRpc();
        }
    }
}