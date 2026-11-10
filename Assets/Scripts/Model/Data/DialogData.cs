using System;
using UnityEngine;

namespace Model.Data
{
    [Serializable]
    public class DialogData
    {
        [SerializeField] private string[] sentences;
        public string[] Sentences => sentences;
    }
}