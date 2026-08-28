using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Model.Definitions.Editor
{
    [CustomPropertyDrawer(typeof (InventoryIdAttribute))]
    public class InventoryIdAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var defs = DefsFacade.I.Items.ItemsForEditor;
            var ids = new List<string>();
            foreach (var def in defs)
            {
                ids.Add(def.Id);
            }
            if(ids.Count == 0) return;
            var index = ids.IndexOf(property.stringValue);
            
            index = EditorGUI.Popup(position, property.displayName, index, ids.ToArray());
            if(index >= 0)
                property.stringValue = ids[index];
            
        }
    }
}