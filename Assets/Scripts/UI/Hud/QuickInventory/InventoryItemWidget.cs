using Model.Data;
using Model.Definitions;
using UnityEngine;
using UnityEngine.UI;
using Utils.Disposables;

namespace UI.Hud.QuickInventory
{
    public class InventoryItemWidget : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private GameObject selection;
        [SerializeField] private Text value;
        
        private readonly CompositeDisposable trash = new CompositeDisposable();

        private int index;
        
        public void SetData(InventoryItemData item, int index)
        {
            this.index = index;
            var def = DefsFacade.I.Items.Get(item.Id);
            icon.sprite = def.Icon;
            value.text = def.IsStackable ? item.Value.ToString() : string.Empty;
        }

        
    }
}