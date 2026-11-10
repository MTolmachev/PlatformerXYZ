using System;
using Model.Definitions.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Localization
{
    [RequireComponent(typeof(Text))]
    public class LocalizeText : MonoBehaviour
    {
        [SerializeField] private string key;
        [SerializeField] private bool capitalize;

        private Text text;
        private void Awake()
        {
            text = GetComponent<Text>();
            
            LocalizationManager.I.OnLocaleChanged += OnLocaleChanged;
            Localize();
        }

        private void OnLocaleChanged()
        {
            Localize();
        }

        private void Localize()
        {
            var localized = LocalizationManager.I.Localize(key);
            text.text = capitalize ? localized.ToUpper() : localized;
        }
    }
}