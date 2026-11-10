using System;
using UnityEditor;

namespace Utils.Editor
{
    public static class SerializedPropertyExtensions
    {
        public static bool GetEnum<TEnumType>(this SerializedProperty property, out TEnumType retValue) 
            where TEnumType : Enum
        {
            retValue = default;
            var names = property.enumNames;
            if (names.Length == 0 || names == null) return false;
            
            var enumName = names[property.enumValueIndex];
            retValue = (TEnumType)Enum.Parse(typeof(TEnumType), enumName);
            return true;
        }
    }
}