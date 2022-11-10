using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

using Pendopo.UI;
using Unity.Netcode;

namespace Pendopo.Network
{
    public class Relay : MonoBehaviour
    {
        public static Relay Instance { get; private set; }

        [SerializeField] UnityTransport transport;

        [Space]
        [SerializeField] int maxPlayer = 2;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Authenticate();
        }

        public async Task Authenticate()
        {
            Debug.Log("Authenticating . . .");

            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            Debug.Log("Authenticating - Success");
        }

        public async void JoinAsHost()
        {
            Debug.Log("Creating Room . . .");

            Allocation alloc = await RelayService.Instance.CreateAllocationAsync(maxPlayer);
            transport.SetRelayServerData(alloc.RelayServer.IpV4, (ushort)alloc.RelayServer.Port, alloc.AllocationIdBytes, alloc.Key, alloc.ConnectionData, alloc.ConnectionData);

            PanelInGame.Instance.JoinCode = await RelayService.Instance.GetJoinCodeAsync(alloc.AllocationId);

            NetworkManager.Singleton.StartHost();

            Debug.Log("Creating Room - Success");
        }

        public async void JoinAsClient()
        {
            Debug.Log("Joining Room");

            JoinAllocation joinAlloc = await RelayService.Instance.JoinAllocationAsync(PanelLobby.Instance.JoinCode);

            transport.SetClientRelayData(joinAlloc.RelayServer.IpV4, (ushort)joinAlloc.RelayServer.Port, joinAlloc.AllocationIdBytes, joinAlloc.Key, joinAlloc.ConnectionData, joinAlloc.HostConnectionData);

            NetworkManager.Singleton.StartClient();

            Debug.Log("Joining Room - Success");
        }
    }
}