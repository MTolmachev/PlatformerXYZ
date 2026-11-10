using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class CustomButton : Button
    {
        [SerializeField] private GameObject normal;
        [SerializeField] private GameObject pressed;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);
            normal.SetActive(state != SelectionState.Pressed);
            pressed.SetActive(state == SelectionState.Pressed);
        }
    }
}