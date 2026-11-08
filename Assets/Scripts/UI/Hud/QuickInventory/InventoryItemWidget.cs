using Model;
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

        private void Start()
        {
            var session = FindObjectOfType<GameSession>();
            trash.Retain(session.QuickInventory.SelectedIndex.SubscribeAndInvoke(OnIndexChanged));
        }

        private void OnIndexChanged(int newValue, int _)
        {
            selection.SetActive(index == newValue);
        }

        public void SetData(InventoryItemData item, int index)
        {
            this.index = index;
            var def = DefsFacade.I.Items.Get(item.Id);
            icon.sprite = def.Icon;
            value.text = def.HasTag(ItemTag.Stackable) ? item.Value.ToString() : string.Empty;
        }

        private void OnDestroy()
        {
            trash.Dispose();
        }
    }
}
