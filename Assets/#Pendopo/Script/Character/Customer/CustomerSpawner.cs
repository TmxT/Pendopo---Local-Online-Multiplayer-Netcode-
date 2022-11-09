using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

using Pendopo.Character;
using Pendopo.Netcode;

namespace Pendopo
{
    public class CustomerSpawner : MonoBehaviour
    {
        //private List<NetworkObject> listSpawned = new List<NetworkObject>();

        [SerializeField] Transform spawnPos;
        [SerializeField] Transform despawnPos;

        [Space]
        [SerializeField] NetworkObjectPool pooler;

        public void Spawn(WaitingPos _targetPos)
        {
            GameObject prefab = pooler.PooledPrefabsList[Random.Range(0, pooler.PooledPrefabsList.Count - 1)].Prefab;
            NetworkObject character = pooler.GetNetworkObject(prefab, spawnPos.position, Quaternion.identity);
            character.Spawn();
            character.GetComponent<CustomerMovement>().Initialize(spawnPos.position, _targetPos, despawnPos.position);

            //listSpawned.Add(character);
        }
    }
}