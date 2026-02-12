using UnityEngine;

namespace Components
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private bool state;
        private static readonly int IsOpened = Animator.StringToHash("isOpened");

        public void Switch()
        {
            state = !state;
            animator.SetBool(IsOpened, state);
        }

        [ContextMenu("Switch")]
        public void SwitchIt()
        {
            Switch();
        }
    }
}