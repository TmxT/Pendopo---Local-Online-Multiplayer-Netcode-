using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Netcode;

using Pendopo.Event;
using Pendopo.Character;
using System;

namespace Pendopo.Network
{
    public class PlayerNetwork : NetworkBehaviour
    {
        [Header("Reference")]
        private GameManager gameManager;

        private void Awake()
        {
            gameManager = GameManager.Instance;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            EventGame.Instance.Invoke(EnumGame.Start);
            transform.position = gameManager.GetSpawnPos(Convert.ToInt32(OwnerClientId));
            Debug.Log("Server : " + IsServer);
        }
    }
}