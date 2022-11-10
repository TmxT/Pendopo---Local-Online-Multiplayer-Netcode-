using Pendopo.Event;
using Pendopo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Pendopo.UI
{
    public class PanelInGame : MonoBehaviour, IEventGame
    {
        public static PanelInGame Instance { get; private set; }

        [SerializeField] GameObject objPanel;

        [Space]
        [SerializeField] TextMeshProUGUI textJoinCode;

        private string joinCode;

        public string JoinCode
        {
            get
            {
                return joinCode;
            }
            set
            {
                joinCode = value;
                textJoinCode.text = joinCode;
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        public void Open()
        {
            objPanel.SetActive(true);
        }

        public void Close()
        {
            objPanel.SetActive(false);
        }

        public void OnEventController(EnumGame _game)
        {
            if (_game == EnumGame.Start)
                Open();
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