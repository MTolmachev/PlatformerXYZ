using System;
using System.Collections;
using Model.Data;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Hud.Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private GameObject container;
        [SerializeField] private Animator animator;

        [Space] [SerializeField] private float textSpeed = 0.09f;
        
        [Header("Sounds")]
        [SerializeField] private AudioClip typing;
        [SerializeField] private AudioClip open;
        [SerializeField] private AudioClip close;

        private DialogData data;
        private int currentSentence;
        private AudioSource sfxSource;
        private Coroutine typingRoutine;
        
        private static readonly int IsOpen = Animator.StringToHash("IsOpen");

        private void Start()
        {
            sfxSource = AudioUtils.FindSfxSource();
        }


        public void ShowDialog(DialogData data)
        {
            this.data = data;
            currentSentence = 0;
            text.text = string.Empty;
            
            container.SetActive(true);
            sfxSource.PlayOneShot(open);
            animator.SetBool(IsOpen, true);
        }

        private IEnumerator TypeDialogText()
        {
            text.text = string.Empty;
            var sentence = data.Sentences[currentSentence];
            
            foreach (var letter in sentence)
            {
                text.text += letter;
                sfxSource.PlayOneShot(typing);
                yield return new WaitForSeconds(textSpeed);
            }
            
            typingRoutine = null;
        }

        public void OnSkip()
        {
            if(typingRoutine == null) return;

            StopTypeAnimation();
            text.text = data.Sentences[currentSentence];
        }

        private void StopTypeAnimation()
        {
            if(typingRoutine != null) 
                StopCoroutine(typingRoutine);
            typingRoutine = null;
        }

        public void OnContinue()
        {
            StopTypeAnimation();
            currentSentence++;
            
            var isDialogCompleted = currentSentence >= data.Sentences.Length;
            if (isDialogCompleted)
            {
                HideDialogBox();
            }
            else
            {
                OnStartDialogAnimation();
            }
        }

        private void HideDialogBox()
        {
            animator.SetBool(IsOpen, false);
            sfxSource.PlayOneShot(close);
        }

        private void OnStartDialogAnimation()
        {
            typingRoutine = StartCoroutine(TypeDialogText());
        }

        private void OnCloseAnimationComplete()
        {
            
        }
    }
}