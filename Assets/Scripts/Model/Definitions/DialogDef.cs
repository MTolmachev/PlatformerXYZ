using Model.Data;
using UnityEngine;

namespace Model.Definitions
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Defs/Dialog")]
    public class DialogDef : ScriptableObject
    {
        [SerializeField] private DialogData data;
        public DialogData Data => data;
    }
}