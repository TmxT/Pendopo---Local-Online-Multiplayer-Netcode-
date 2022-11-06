using InfinityCode.UltimateEditorEnhancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pendopo.Event;

namespace Pendopo.Character
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] protected Rigidbody rb;

        [Space]
        [SerializeField] protected Transform model;

        protected Vector2 moveDir;

        [Space]
        [Range(1f, 10f)]
        [SerializeField] protected float moveSpeed = 5f;
        [Range(1f, 10f)]
        [SerializeField] protected float rotationSpeed = 5f;

        [Space]
        [SerializeField] protected bool revers = true;

        [Header("Reference")]
        protected AnimatorController animatorController;

        protected virtual void Awake()
        {
            animatorController = GetComponent<AnimatorController>();
        }

        protected virtual void FixedUpdate()
        {
            if (EventGame.Instance.CurrentGame != EnumGame.Start)
                return;

            Move();
        }

        protected virtual void Move()
        {
            if (moveDir != Vector2.zero)
                animatorController.Play(EnumAnim.Walk);
            else
                animatorController.Play(EnumAnim.Idle);
        }
    }
}