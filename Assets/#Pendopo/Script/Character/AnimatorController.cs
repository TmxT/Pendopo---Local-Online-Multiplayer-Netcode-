using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pendopo.Character
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] Animator animator;

        private EnumAnim currentAnim = EnumAnim.Idle;

        public void Play(EnumAnim _anim)
        {
            if (currentAnim == _anim)
                return;

            animator.Play(_anim.ToString());
            currentAnim = _anim;
        }
    }
}