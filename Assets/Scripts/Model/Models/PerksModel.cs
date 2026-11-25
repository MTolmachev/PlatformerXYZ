using System;
using System.Net.Mime;
using Model.Data;
using Model.Data.Properties;
using Model.Definitions;
using Utils;
using Utils.Disposables;

namespace Model.Models
{
    public class PerksModel : IDisposable
    {
        private readonly PlayerData data;
        public readonly StringProperty InterfaceSelection = new StringProperty();

        private readonly CompositeDisposable trash = new CompositeDisposable();
        private readonly Cooldown cooldown = new Cooldown();
        public event Action OnChanged;
        public PerksModel(PlayerData data)
        {
            this.data = data;
            InterfaceSelection.Value = DefsFacade.I.Perks.All[0].Id;
            
            trash.Retain(data.Perks.Used.Subscribe((x,y) => OnChanged?.Invoke()));
            trash.Retain(InterfaceSelection.Subscribe((x,y) => OnChanged?.Invoke()));
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public string Used => data.Perks.Used.Value;
        public bool IsSuperThrowSupported => data.Perks.Used.Value == "super-throw"; 
        public bool IsDoubleJumpSupported => data.Perks.Used.Value == "double-jump";
        public float CooldownProgress => cooldown.Value <= 0f ? 0f : cooldown.TimeLasts / cooldown.Value;

        public bool TryUse(string id)
        {
            if (!IsUsed(id) || !cooldown.IsReady) return false;

            var def = DefsFacade.I.Perks.Get(id);
            cooldown.Value = def.Cooldown;
            cooldown.Reset();
            return true;
        }

        public void Unlock(string id)
        {
            var def = DefsFacade.I.Perks.Get(id);
            var isEnoughResources = data.Inventory.IsEnough(def.Price);

            if (isEnoughResources)
            {
                data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
                data.Perks.AddPerk(id);
                
                OnChanged?.Invoke();
            }
        }
        
        public void UsePerk(string id)
        {
            data.Perks.Used.Value = id;
        }
        

        public bool IsUsed(string perkId)
        {
            return data.Perks.Used.Value == perkId;
        }

        public bool IsUnlocked(string perkId)
        {
            return data.Perks.IsUnlocked(perkId);
        }

        public bool CanBuy(string perkId)
        {
            var def = DefsFacade.I.Perks.Get(perkId);
            return data.Inventory.IsEnough(def.Price);
        }
        
        public void Dispose()
        {
            trash.Dispose();
        }
    }
}
