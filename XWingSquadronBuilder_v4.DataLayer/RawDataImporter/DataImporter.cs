﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using XWingSquadronBuilder_v4.DataLayer.RawData;

namespace XWingSquadronBuilder_v4.DataLayer.RawDataImporter
{
    internal class DataImporter
    {
        internal static ActionJson[] LoadActions()
        { 
            var root = GetDataFromJson<ActionsRootobject>("Actions");
            return root.Actions.Action;
        }

        internal static PilotJson[] GetPilots()
        {
            var root = GetDataFromJson<PilotsJsonRoot>("Pilots");
            return root.Pilots;
        }

        internal static UpgradeJson[] LoadUpgrades()
        {
            var root = GetDataFromJson<UpgradeRootobject>("Upgrades");
            return root.Upgrades.Upgrade;

        }

        internal static FactionJson[] LoadFactions()
        {
            var root = GetDataFromJson<FactionJsonRoot>("Factions");
            return root.Factions;
        }

        internal static UpgradeTypeJson[] LoadUpgradesTypes()
        {
            var root = GetDataFromJson<UpgradeTypesRootobject>("UpgradeTypes");
            return root.UpgradeTypes.UpgradeType;

        }

        private static T GetDataFromJson<T>(string dataname)
        {
            return Task.Run(async () =>
            {
                var storageFolder = Package.Current.InstalledLocation;                

                StorageFile file = await storageFolder.GetFileAsync($"XWingSquadronBuilder_v4.DataLayer\\Json\\{dataname}.json").AsTask();

                using (var inputStream = await file.OpenReadAsync().AsTask())
                using (var stream = inputStream.AsStreamForRead())
                {
                    DataContractJsonSerializer mySerializer = new DataContractJsonSerializer(typeof(T));
                    var root = (T)mySerializer.ReadObject(stream);
                    return root;
                }
            }).Result;
        }

    }
}
