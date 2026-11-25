using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Model.Definitions.Repositories.Items.Editor
{
    [CustomPropertyDrawer(typeof(InventoryIdAttribute))]
    public class InventoryIdAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var definitions = DefsFacade.I.Items.ItemsForEditor;
            var ids = new List<string>();
            foreach (var itemDef in definitions)
            {
                ids.Add(itemDef.Id);
            }

            if (ids.Count == 0)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            var currentIndex = ids.IndexOf(property.stringValue);
            var selectedIndex = EditorGUI.Popup(position, property.displayName, currentIndex, ids.ToArray());

            if (selectedIndex >= 0)
                property.stringValue = ids[selectedIndex];
        }
    }
}
