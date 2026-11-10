using UnityEditor;
using UnityEditor.UI;

namespace UI.Widgets.Editor
{
    [CustomEditor(typeof(CustomButton), true)]
    [CanEditMultipleObjects]
    public class CustomButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("normal"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pressed"), true);
            serializedObject.ApplyModifiedProperties();
            
            base.OnInspectorGUI();
        }
    }
}