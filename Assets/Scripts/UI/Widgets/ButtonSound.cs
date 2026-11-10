using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace UI.Widgets
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip audioClip;
        
        private AudioSource source;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if(source == null)
                source = AudioUtils.FindSfxSource();
            source.PlayOneShot(audioClip);
        }
    }
}
