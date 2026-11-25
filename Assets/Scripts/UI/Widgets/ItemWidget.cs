using Model.Definitions;
using Model.Definitions.Repositories;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class ItemWidget : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text value;

        public void SetData(ItemWithCount price)
        {
            var def = DefsFacade.I.Items.Get(price.ItemId);
            icon.sprite = def.Icon;
            
            value.text = price.Count.ToString();
        }
    }
}