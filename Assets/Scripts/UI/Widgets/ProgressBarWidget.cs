using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class ProgressBarWidget : MonoBehaviour
    {
        [SerializeField] private Image bar;

        public void SetProgress(float progress)
        {
            bar.fillAmount = progress;
        }
    }
}