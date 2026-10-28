using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Widgets
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip audioClip;
        
        private AudioSource source;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if(source == null)
                source = GameObject.FindWithTag("SfxAudioSource").GetComponent<AudioSource>();
            source.PlayOneShot(audioClip);
        }
    }
}