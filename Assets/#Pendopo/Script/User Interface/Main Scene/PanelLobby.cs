using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Pendopo.Event;
using Pendopo.Network;

namespace Pendopo.UI
{
    public class PanelLobby : MonoBehaviour, IEventGame
    {
        public static PanelLobby Instance { get; private set; }

        [SerializeField] GameObject objPanel;

        [Space]
        [SerializeField] Button btnHost;
        [SerializeField] Button btnClient;

        [Space]
        [SerializeField] TMP_InputField inputJoinCode;

        public string JoinCode
        {
            get
            {
                return inputJoinCode.text;
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            btnClient.interactable = true;
            btnClient.interactable = false;
        }

        public void Open()
        {
            objPanel.SetActive(true);
        }

        public void Close()
        {
            objPanel.SetActive(false);
        }

        public void BtnHost()
        {
            //NetworkManager.Singleton.StartHost();
            Relay.Instance.JoinAsHost();
        }

        public void BtnClient()
        {
            //NetworkManager.Singleton.StartClient();
            Relay.Instance.JoinAsClient();
        }

        public void OnInputJoinCode(string _value)
        {
            btnHost.interactable = string.IsNullOrEmpty(_value);
            btnClient.interactable = !string.IsNullOrEmpty(_value);
        }

        public void OnEventController(EnumGame _game)
        {
            if (_game == EnumGame.Start)
                Close();
        }

        private void OnEnable()
        {
            EventGame.Instance.AddListener(OnEventController);
        }

        private void OnDisable()
        {
            if (!gameObject.scene.isLoaded) return;

            EventGame.Instance.RemoveListener(OnEventController);
        }
    }
}