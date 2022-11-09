using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

using Pendopo.Event;

namespace Pendopo
{
    public class WaitingPos : MonoBehaviour, IEventGame
    {
        [SerializeField] int id;

        public NetworkObject Customer { private get; set; }

        public int Id { get { return id; } }

        public bool Available { get { return !Customer; } }

        [Header("Reference")]
        private CustomerManager customerManager;

        private void Start()
        {
            customerManager = CustomerManager.Instance;
        }

        private void RequestCustomer()
        {
            customerManager.SpawningCustomer(this);
        }

        public void OnEventController(EnumGame _game)
        {
            if(_game == EnumGame.Start)
                if (Available)
                    RequestCustomer();
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