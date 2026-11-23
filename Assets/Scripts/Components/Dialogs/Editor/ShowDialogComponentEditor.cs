using System;
using Model.Data;
using UnityEditor;
using UnityEngine;
using Utils.Editor;

namespace Components.Dialogs.Editor
{
    [CustomEditor(typeof(ShowDialogComponent))]
    public class ShowDialogComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty modeProperty;
        private void OnEnable()
        {
           modeProperty = serializedObject.FindProperty("mode"); 
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(modeProperty);

            if (modeProperty.GetEnum(out ShowDialogComponent.Mode mode))
            {
                switch (mode)
                {
                    case ShowDialogComponent.Mode.Bound:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("bound"));
                        break;
                    case ShowDialogComponent.Mode.External:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("external"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}