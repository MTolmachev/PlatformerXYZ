using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Model.Data
{
    [Serializable]
    public class DialogData
    {
        [FormerlySerializedAs("sentences")]
        [SerializeField] private string[] sentenceKeys;

        public string[] SentenceKeys => sentenceKeys;
    }
}
