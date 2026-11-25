using System.Collections.Generic;
using UnityEngine;

namespace UI.Widgets
{
    public class DataGroup<TDataType, TItemType> where TItemType : MonoBehaviour, IItemRenderer<TDataType>
    {
        protected readonly List<TItemType> CreatedItems = new List<TItemType>();
        private readonly TItemType prefab;
        private readonly Transform container;

        public DataGroup(TItemType prefab, Transform container)
        {
            this.prefab = prefab;
            this.container = container;
        }

        public virtual void SetData(IList<TDataType> data)
        {
            for (var i = CreatedItems.Count; i < data.Count; i++)
            {
                var item = Object.Instantiate(prefab, container);
                CreatedItems.Add(item);
            }

            for (var i = 0; i < data.Count; i++)
            {
                CreatedItems[i].SetData(data[i], i);
                CreatedItems[i].gameObject.SetActive(true);
            }

            for (var i = data.Count; i < CreatedItems.Count; i++)
            {
                CreatedItems[i].gameObject.SetActive(false);
            }
        }
    }

    public interface IItemRenderer<in TDataType>
    {
        void SetData(TDataType data, int index);
    }
}
