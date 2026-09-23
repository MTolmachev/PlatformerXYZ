using System;
using UnityEngine;

namespace UI
{
    public class AnimatedWindow : MonoBehaviour
    {
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");
        private Animator animator;

        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            animator.SetTrigger(Show);
        }

        public void Close()
        {
            animator.SetTrigger(Hide);
        }

        public virtual void OnCloseAnimationComplete()
        {
            Destroy(gameObject);
        }
    }
}