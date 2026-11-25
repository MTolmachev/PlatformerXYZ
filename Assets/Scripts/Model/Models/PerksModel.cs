using System;
using System.Net.Mime;
using System.Collections.Generic;
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
        private readonly Dictionary<string, Cooldown> cooldowns = new Dictionary<string, Cooldown>();
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
        public bool TryUse(string id)
        {
            if (!IsUsed(id)) return false;

            var cooldown = GetCooldown(id);
            if (!cooldown.IsReady) return false;

            cooldown.Reset();
            return true;
        }

        public float GetCooldownProgress(string id)
        {
            var cooldown = GetCooldown(id);
            return cooldown.Value <= 0f ? 0f : cooldown.TimeLasts / cooldown.Value;
        }

        private Cooldown GetCooldown(string id)
        {
            if (cooldowns.TryGetValue(id, out var cooldown))
                return cooldown;

            var def = DefsFacade.I.Perks.Get(id);
            cooldown = new Cooldown {Value = def.Cooldown};
            cooldowns.Add(id, cooldown);
            return cooldown;
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
