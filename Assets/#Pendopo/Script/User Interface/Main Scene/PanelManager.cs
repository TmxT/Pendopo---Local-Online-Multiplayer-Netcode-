using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

using Pendopo.Event;

namespace Pendopo.UI
{
    public class PanelManager : MonoBehaviour, IEventGame
    {
        [SerializeField] GameObject objPanel;

        public void BtnHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void BtnClient()
        {
            NetworkManager.Singleton.StartClient();
        }

        public void OnEventController(EnumGame _game)
        {
            if (_game == EnumGame.Start)
                objPanel.SetActive(false);
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