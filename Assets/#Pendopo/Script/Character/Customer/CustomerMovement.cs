using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

namespace Pendopo.Character
{
    public class CustomerMovement : Movement
    {
        private NavMeshAgent agent;

        private WaitingPos targetPos;

        private NetworkObject networkObject;

        private Vector3 despawnPos;
        private Vector3 currentTarget;

        private bool arrived;

        [Header("Reference")]
        private Order order;

        protected override void Awake()
        {
            base.Awake();

            agent = GetComponent<NavMeshAgent>();
            networkObject = GetComponent<NetworkObject>();
            order = GetComponent<Order>();
        }

        private void Update()
        {
            arrived = Vector3.Distance(transform.position, currentTarget) < .1f;

            if (targetPos && !order.IsOrdering && arrived)
            {
                order.Ordering();
                Arrived();
                targetPos = null;
            }
        }

        public void Initialize(Vector3 _spawnPos, WaitingPos _targetPos, Vector3 _despawnPos)
        {
            targetPos = _targetPos;
            targetPos.Customer = networkObject;
            despawnPos = _despawnPos;

            agent.Warp(_spawnPos);

            Move(targetPos.transform.position);
        }

        public void Move(Vector3 _target)
        {
            isWalking = true;
            //model.localRotation = Quaternion.identity;
            agent.SetDestination(_target);

            currentTarget = _target;
        }

        private void Arrived()
        {
            isWalking = false;
            agent.Warp(currentTarget);
            transform.Rotate(new Vector3(0, -90, 0));// = new Quaternion(0, -90, 0, 0);
        }
    }
}