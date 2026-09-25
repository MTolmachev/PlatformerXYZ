using UnityEngine;

namespace Model.Data.Properties
{
    public abstract class PersistentProperty<TPropertyType>
    {
        [SerializeField] private TPropertyType value;
        private TPropertyType stored;
        private TPropertyType defaultValue;
        
        public delegate void OnPropertyChanged(TPropertyType newValue,  TPropertyType oldValue);
        public event OnPropertyChanged onChanged;

        public PersistentProperty(TPropertyType defaultValue)
        {
            this.defaultValue = defaultValue;
        }
        
        public TPropertyType Value
        {
            get => stored;
            set
            {
                var isEquals = stored.Equals(value);
                if(isEquals) return;
                
                var oldValue = this.value;
                Write(value);
                stored = this.value = value;
                
                onChanged?.Invoke(value, oldValue);
            }
        }

        public void Init()
        {
            stored = value = Read(defaultValue);
        }
        
        protected abstract void Write(TPropertyType value);
        protected abstract TPropertyType Read(TPropertyType defaultValue);
        
        
        public void Validate()
        {
            if (!stored.Equals(this.value))
                Value = this.value;
        }
    }
}