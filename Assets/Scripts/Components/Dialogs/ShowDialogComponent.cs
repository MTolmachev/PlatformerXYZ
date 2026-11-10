using System;
using Model.Data;
using Model.Definitions;
using UI.Hud.Dialogs;
using UnityEngine;

namespace Components.Dialogs
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] private Mode mode;
        [SerializeField] private DialogData bound;
        [SerializeField] private DialogDef external;

        private DialogBoxController dialogBox;
        public void Show()
        {
            if(dialogBox == null)
                dialogBox = FindObjectOfType<DialogBoxController>();
            
            dialogBox.ShowDialog(Data);
        }

        public void Show(DialogDef def)
        {
            external = def;
            Show();
        }

        public DialogData Data
        {
            get
            {
                switch (mode)
                {
                    case Mode.Bound:
                        return bound;
                    case Mode.External:
                        return external.Data;
                    default:
                        throw new ArgumentOutOfRangeException();
                }  
            } 
        }

        public enum Mode
        {
            Bound, 
            External
        }
        
    }
}