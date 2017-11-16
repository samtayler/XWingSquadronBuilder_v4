using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Factories;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Repositories
{
    public class SavedSquadronsRepository : INotifyPropertyChanged
    {
        private ObservableCollection<ISquadron> savedSquadrons { get; set; }

        public SavedSquadronsRepository()
        {
            savedSquadrons = new ObservableCollection<ISquadron>();
            GenerateSquadronsFromData().ContinueWith(CompleteLoadingSavedSquadrons);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CompleteLoadingSavedSquadrons(Task<ObservableCollection<ISquadron>> loadSquadronsTask)
        {
            savedSquadrons = loadSquadronsTask.Result;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SavedSquadronsRepository)));
        }

        public IReadOnlyList<ISquadron> GetSquadrons(IFaction faction)
        {
            return savedSquadrons.Where(squadrion => squadrion.Faction.Equals(faction)).ToList().AsReadOnly();
        }

        public IReadOnlyList<ISquadron> GetSquadrons()
        {
            return savedSquadrons.ToList().AsReadOnly();
        }

        public bool SaveSquadron(ISquadron squadron)
        {
            if (savedSquadrons.Any(x => x.Name == squadron.Name)) return false;

            savedSquadrons.Add(squadron);

            return true;
        }

        private async Task<ObservableCollection<ISquadron>> GenerateSquadronsFromData()
        {
            //var squadrons = new ObservableCollection<ISquadron>();
            //foreach (var squadronData in await XWingRepository.Instance.PersistanceRepository.GetSavedSquadrons())
            //{
            //    var faction = XWingRepository.Instance.FactionRepository.GetFaction(squadronData.Faction);
            //    var pilots = new List<IPilot>();

            //    foreach (var pilotData in squadronData.Pilots)
            //    {
            //        var pilot = XWingRepository.Instance.PilotRepository.GetPilot(pilotData.Name, pilotData.ShipName);

            //        var upgrades = new List<IUpgrade>();
            //        foreach (var upgradeData in pilotData.Upgrades)
            //        {
            //            upgrades.Add(XWingRepository.Instance.UpgradeRepository.GetUpgrade(upgradeData.Name, upgradeData.UpgradeType));

            //        }

            //        foreach (var upgrade in upgrades.OrderByDescending(x => x.AddUpgradeModifiers.Count))
            //        {
            //            pilot.Upgrades.First(x => x.IsNullUpgrade && x.UpgradeType.Equals(upgrade.UpgradeType)).SetUpgrade(upgrade);
            //        }

            //        pilots.Add(pilot);
            //    }

            //    var squadron = SquadronFactory.CreateSquadron(faction);

            //    foreach (var pilot in pilots)
            //    {
            //        squadron.AddPilot(pilot);
            //    }

            //    squadrons.Add(squadron);
            //}
            await Task.CompletedTask;
            return new ObservableCollection<ISquadron>();
        }
    }
}
