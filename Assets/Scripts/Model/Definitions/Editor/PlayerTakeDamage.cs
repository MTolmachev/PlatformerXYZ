using Components.Health;
using Creatures.Character;
using UnityEngine;
using UnityEditor;

namespace Model.Definitions.Editor
{
    [CustomEditor(typeof(HealthComponent))]
    public class PlayerTakeDamage : UnityEditor.Editor
    {
        private int dmgInput = 1;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var player = (HealthComponent)target;

            GUILayout.Space(15);
            GUILayout.Label("Damage for test", EditorStyles.boldLabel);

            // Отрисовка поля ввода в одну строку с кнопкой
            GUILayout.BeginHorizontal();
            dmgInput = EditorGUILayout.IntField("Value", dmgInput);
        
            if (GUILayout.Button("Damage"))
            {
                player.TakeDamage(dmgInput);
            }
            GUILayout.EndHorizontal();
        }
    }
}