using UnityEngine;

namespace Model.Data.Properties
{
    public class ObservableProperty<TPropertyType>
    {
        [SerializeField] private TPropertyType value;
        
        public delegate void OnPropertyChanged(TPropertyType newValue,  TPropertyType oldValue);
        public event OnPropertyChanged onChanged;
        
        public TPropertyType Value
        {
            get => value;
            set
            {
                var isSame = this.value.Equals(value);
                if (isSame) return;
                var oldValue = this.value;
                this.value = value;
                onChanged?.Invoke(this.value, oldValue);
            }
            
        }
    }
}