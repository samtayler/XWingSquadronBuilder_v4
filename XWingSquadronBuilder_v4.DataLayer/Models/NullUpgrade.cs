﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.DataLayer.Models.Models;
using XWingSquadronBuilder_v4.DataLayer.Repositories;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.DataLayer.Models
{
    public class NullUpgrade : IUpgrade
    {
        public NullUpgrade(IUpgradeType upgradeType)
        {
            UpgradeType = upgradeType;
        }

        public IEnumerable<IAction> AddActionModifiers => new List<IAction>();

        public IEnumerable<IAction> RemoveActionModifiers => new List<IAction>();

        public IEnumerable<IUpgradeModifier> AddUpgradeModifiers => new List<IUpgradeModifier>();

        public IEnumerable<IUpgradeType> RemoveUpgradeModifiers => new List<IUpgradeType>();

        public IDictionary<string, int> PilotAttributeModifiers => new Dictionary<string, int>();

        public string Name => string.Empty;

        public int Cost => 0;

        public int SlotsRequired => 0;

        public string CardText => UpgradeType.Name;

        public bool Unique => false;

        public bool Limited => false;

        public string ShipLimited => string.Empty;

        public string SizeRestriction => string.Empty;

        public IFaction Faction => XWingRepository.Instance.FactionRepository.GetFaction("Any");

        public IUpgradeType UpgradeType { get; }

        public IUpgrade DeepClone()
        {
            return new NullUpgrade(this.UpgradeType);
        }

        public bool Equals(IUpgrade other)
        {
            return this.UpgradeType.Equals(other.UpgradeType);
        }
    }
}