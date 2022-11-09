using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

using Pendopo.Event;

namespace Pendopo
{
    public class CustomerManager : NetworkBehaviour, IEventGame
    {
        public static CustomerManager Instance { get; private set; }

        [SerializeField] WaitingPos[] listPos;

        [Space]
        [SerializeField] int customer = 5;

        [Space]
        [SerializeField] float spawnTimeMin = 1;
        [SerializeField] float spawnTimeMax = 10;

        [Header("Reference")]
        private CustomerSpawner customerSpawner;

        private void Awake()
        {
            Instance = this;

            customerSpawner = GetComponent<CustomerSpawner>();
        }

        private IEnumerator CoroutineSpawn(WaitingPos _targetPos)
        {
            float time = Random.Range(spawnTimeMin, spawnTimeMax);
            Debug.Log("Spawning Customer");

            while (time > 0 && EventGame.Instance.CurrentGame != EnumGame.Finish)
            {
                if (EventGame.Instance.CurrentGame != EnumGame.Pause)
                    time -= Time.deltaTime;

                yield return null;
            }

            customerSpawner.Spawn(_targetPos);
        }

        public void SpawningCustomer(WaitingPos _targetPos)
        {
            if (!IsOwner || customer == 0)
                return;

            StartCoroutine(CoroutineSpawn(_targetPos));

            customer--;
        }

        public void OnEventController(EnumGame _game)
        {
            /*if(_game == EnumGame.Start && IsOwner)
                StartCoroutine(CoroutineSpawn());*/
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