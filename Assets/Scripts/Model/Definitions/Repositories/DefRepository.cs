using System;
using UnityEngine;

namespace Model.Definitions.Repositories
{
    public class DefRepository<TDefType> : ScriptableObject where TDefType : IHaveId
    {
        [SerializeField] protected TDefType[] _collection;

        public TDefType Get(string id)
        {
            if (string.IsNullOrEmpty(id) || _collection == null)
                return default;

            foreach (var definition in _collection)
            {
                if (definition.Id == id)
                    return definition;
            }

            return default;
        }

        public TDefType[] All => _collection == null
            ? Array.Empty<TDefType>()
            : (TDefType[]) _collection.Clone();
    }
}
