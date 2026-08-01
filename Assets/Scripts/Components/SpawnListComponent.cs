using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[] spawners;

        public void Spawn(string id)
        {
            var spawner = spawners.FirstOrDefault(element => element.Id == id);
            spawner?.Component.Spawn();
        }

        [Serializable]
        public class SpawnData
        {
            public string Id;
            public SpawnComponent Component;
        }
    }
}