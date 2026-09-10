using UnityEngine;

namespace Utils
{
    public static class HandlesUtils
    {
        public static readonly Color TransparentRed = new Color(1f, 0f, 0f, 0.1f);
        public static readonly Color TransparentGreen = new Color(0f, 1f, 0f, 0.1f);
        public static readonly Color TransparentBlue = new Color(0f, 0f, 1f, 0.1f);
        
        public static bool IsInLayer(this GameObject gameObject, LayerMask layerMask)
        {
            return (layerMask.value & (1 << gameObject.layer)) != 0;
        }
    }
}