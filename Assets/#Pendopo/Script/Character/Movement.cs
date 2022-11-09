using InfinityCode.UltimateEditorEnhancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pendopo.Event;
using Unity.Netcode;

namespace Pendopo.Character
{
    public class Movement : NetworkBehaviour
    {
        [SerializeField] protected Transform model;

        [Space]
        [Range(1f, 10f)]
        [SerializeField] protected float moveSpeed = 5f;
        [Range(1f, 10f)]
        [SerializeField] protected float rotationSpeed = 5f;

        [Space]
        [SerializeField] protected bool revers = true;
        protected bool isWalking;

        [Header("Reference")]
        protected AnimatorController animatorController;

        protected virtual void Awake()
        {
            animatorController = GetComponent<AnimatorController>();
        }

        protected virtual void FixedUpdate()
        {
            if (EventGame.Instance.CurrentGame != EnumGame.Start || !IsOwner)
                return;

            Move();
        }

        protected virtual void Move()
        {
            if (isWalking)
                animatorController.Play(EnumAnim.Walk);
            else
                animatorController.Play(EnumAnim.Idle);
        }
    }
}