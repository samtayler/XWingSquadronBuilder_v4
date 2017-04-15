using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.ViewModels
{
    public class PilotViewModel : INotifyPropertyChanged, IDisposable
    {
        public PilotViewModel(IPilot pilot)
        {
            Pilot = pilot;
            abilityEngine = new PilotAbilityEngine(new PilotStatPackage(pilot.Attack, pilot.Agility, pilot.Hull, pilot.Shield, pilot.PilotSkill), pilot.Actions, pilot.Upgrades);
            abilityEngine.PropertyChanged += AbilityEngine_PropertyChanged;
        }

        private void AbilityEngine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IPilot Pilot { get; }
        private PilotAbilityEngine abilityEngine { get; }

        public int Cost => Pilot.Cost + abilityEngine.Cost;       

        public IShipSize ShipSize { get; }

        public int Attack => abilityEngine.Attack;

        public int Agility => abilityEngine.Agility;

        public int Hull => abilityEngine.Hull;

        public int Shield => abilityEngine.Shield;

        public int PilotSkill => abilityEngine.PilotSkill;

        public IEnumerable<IAction> Actions => abilityEngine.Actions;

        public IEnumerable<UpgradeSlotViewModel> Upgrades => abilityEngine.Upgrades;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            abilityEngine.PropertyChanged -= AbilityEngine_PropertyChanged;
            abilityEngine.Dispose();
        }
    }
}
