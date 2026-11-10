using System;
using Model.Data.Properties;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class AudioSettingsWidget : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Text value;

        private FloatPersistentProperty model;
        
        private void Start()
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            model.Value = value;
        }

        public void SetModel(FloatPersistentProperty model)
        {
            this.model = model;
            model.onChanged += OnValueChanged;
            OnValueChanged(model.Value, model.Value);
        }

        private void OnValueChanged(float newValue, float oldValue)
        {
            var textValue = Mathf.Round(newValue * 100);
            value.text = textValue.ToString();
            slider.normalizedValue = newValue;
        }

        private void OnDestroy()
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            model.onChanged -= OnValueChanged;
        }
    }
}