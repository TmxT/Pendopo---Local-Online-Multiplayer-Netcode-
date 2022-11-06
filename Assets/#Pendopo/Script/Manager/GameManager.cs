using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

using Pendopo.Network;
using Pendopo.Event;
using Pendopo.Character;

namespace Pendopo
{
    public class GameManager : NetworkBehaviour, IEventGame
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] GameObject[] listCharacterPrefab;

        [Space]
        [SerializeField] Transform[] listSpawnPos;

        [Space]
        [SerializeField] Transform transCharacterParent;
        [SerializeField] Transform transSpawnPos;

        private void Awake()
        {
            Instance = this;
        }

        private void Initialize()
        {

        }

        public Vector3 GetSpawnPos(int _index)
        {
            return listSpawnPos[_index].position;
        }

        public void OnEventController(EnumGame _game)
        {
            if(_game == EnumGame.Start)
                Initialize();
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