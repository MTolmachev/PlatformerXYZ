using System.Collections.Generic;
using UnityEngine;

namespace UI.Widgets
{
    public class DataGroup<TDataType, TItemType> where TItemType : MonoBehaviour, IItemRenderer<TDataType>
    {
        private readonly List<TItemType> createdItems = new List<TItemType>();
        private readonly TItemType prefab;
        private readonly Transform container;

        public DataGroup(TItemType prefab, Transform container)
        {
            this.prefab = prefab;
            this.container = container;
        }

        public void SetData(IList<TDataType> data)
        {
            for (var i = createdItems.Count; i < data.Count; i++)
            {
                var item = Object.Instantiate(prefab, container);
                createdItems.Add(item);
            }

            for (var i = 0; i < data.Count; i++)
            {
                createdItems[i].SetData(data[i], i);
                createdItems[i].gameObject.SetActive(true);
            }

            for (var i = data.Count; i < createdItems.Count; i++)
            {
                createdItems[i].gameObject.SetActive(false);
            }
        }
    }

    public interface IItemRenderer<in TDataType>
    {
        void SetData(TDataType data, int index);
    }
}
