using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Specifications
{
    [DataContract]
    public abstract class XWingSpecification<T> : Specification<T>, IXWingSpecification<T>
    {
        [DataMember]
        public abstract SpecificationType SpecType { get; }
        [DataMember]
        public abstract string ErrorMessage { get; }

        public bool IsSatisfiedBy(T entity, List<string> errors)
        {
            if (errors == null) throw new ArgumentNullException(nameof(errors));

            Func<T, bool> predicate = ToExpression().Compile();

            if (predicate(entity)) return true;

            errors.Add(ErrorMessage);

            return false;
        }

    }

    [DataContract]
    public sealed class UpgradeUniqueSpecification : XWingSpecification<IUpgrade>
    {
        public override string ErrorMessage => "Upgrade card must be unique.";
        public override SpecificationType SpecType => SpecificationType.NonCritical;

        public override Expression<Func<IUpgrade, bool>> ToExpression()
        {
            return upgrade => upgrade.Unique;
        }
    }

    [DataContract]
    public sealed class PilotSkillGreaterThanSpecification : XWingSpecification<IPilot>
    {
        [DataMember]
        private int requiredPilotSkill;

        public PilotSkillGreaterThanSpecification(int requiredPilotSkill)
        {
            this.requiredPilotSkill = requiredPilotSkill;
        }

        public override string ErrorMessage => "Pilot does not have a high enough pilot skill.";

        public override SpecificationType SpecType => SpecificationType.NonCritical;

        public override Expression<Func<IPilot, bool>> ToExpression()
        {
            return pilot => pilot.PilotSkill > requiredPilotSkill;
        }
    }

    [DataContract]
    public sealed class ContainsShipNameSpecification : XWingSpecification<IPilot>
    {
        [DataMember]
        private string requiredShipName;

        public ContainsShipNameSpecification(string requiredShipName)
        {
            this.requiredShipName = requiredShipName;
        }

        public override SpecificationType SpecType => SpecificationType.Critical;

        public override string ErrorMessage => "This upgrade is not for this ship.";

        public override Expression<Func<IPilot, bool>> ToExpression()
        {
            return pilot => pilot.ShipName.Contains(requiredShipName);
        }
    }

    [DataContract]
    public sealed class EqualsShipSizeSpecification : XWingSpecification<IPilot>
    {
        [DataMember]
        private string shipSize;

        public EqualsShipSizeSpecification(string shipSize)
        {
            this.shipSize = shipSize;
        }

        public override string ErrorMessage => "This ship is not the correct size for this upgrade.";
        public override SpecificationType SpecType => SpecificationType.Critical;

        public override Expression<Func<IPilot, bool>> ToExpression()
        {
            return pilot => pilot.ShipSize.ToString() == shipSize;
        }
    }

    [DataContract]
    public sealed class ContainsActionSpecification : XWingSpecification<IPilot>
    {
        [DataMember]
        private IAction action;

        public ContainsActionSpecification(IAction action)
        {
            this.action = action;
        }

        public override string ErrorMessage => $"This pilot does not contain the {action.Name} action.";
        public override SpecificationType SpecType => SpecificationType.NonCritical;

        public override Expression<Func<IPilot, bool>> ToExpression()
        {
            return pilot => pilot.Actions.Contains(action);
        }
    }

    [DataContract]
    public sealed class PilotHasRequiredUpgradeSlotsSpecification : XWingSpecification<IPilot>
    {
        [DataMember]
        private IUpgradeType upgradeType;
        [DataMember]
        private int slotsRequired;

        public PilotHasRequiredUpgradeSlotsSpecification(IUpgradeType upgradeType, int slotsRequired)
        {
            this.upgradeType = upgradeType;
            this.slotsRequired = slotsRequired;
        }

        public override string ErrorMessage => "This pilot does not have the required number of upgrade slots.";
        public override SpecificationType SpecType => SpecificationType.NonCritical;

        public override Expression<Func<IPilot, bool>> ToExpression()
        {
            return pilot => pilot.Upgrades.Where(uSlot => uSlot.UpgradeType.Equals(upgradeType) && (uSlot.Upgrade.CardText == "")).Count() >= slotsRequired;
            // make sure there are enough slots avaliable for the upgrade
        }
    }

    [DataContract]
    public sealed class NotContainsUpgradeSlotsSpecification : XWingSpecification<IPilot>
    {
        [DataMember]
        private IUpgradeType upgradeType;        

        public NotContainsUpgradeSlotsSpecification(IUpgradeType upgradeType)
        {
            this.upgradeType = upgradeType;            
        }

        public override string ErrorMessage => $"This pilot has a upgrade slot of type {upgradeType.Name}.";
        public override SpecificationType SpecType => SpecificationType.NonCritical;

        public override Expression<Func<IPilot, bool>> ToExpression()
        {
            return pilot => pilot.Upgrades.Where(upgrade => upgrade.UpgradeType.Equals(upgradeType)).Count() == 0;            
        }
    }

    [DataContract]
    public sealed class ContainsUpgradeSlotsSpecification : XWingSpecification<IPilot>
    {
        [DataMember]
        private IUpgradeType upgradeType;

        public ContainsUpgradeSlotsSpecification(IUpgradeType upgradeType)
        {
            this.upgradeType = upgradeType;
        }

        public override string ErrorMessage => $"This pilot does not have a upgrade slot of type {upgradeType.Name}.";
        public override SpecificationType SpecType => SpecificationType.NonCritical;

        public override Expression<Func<IPilot, bool>> ToExpression()
        {
            return pilot => pilot.Upgrades.Where(upgrade => upgrade.UpgradeType.Equals(upgradeType)).Count() > 0;
        }
    }

    [DataContract]
    public sealed class UpgradeCostLessOrEqualToThanSpecification : XWingSpecification<IUpgrade>
    {
        [DataMember]
        private int costRestriction;

        public UpgradeCostLessOrEqualToThanSpecification(int costRestriction)
        {
            this.costRestriction = costRestriction;
        }

        public override string ErrorMessage => "This upgrade cost is higher than is allows for this upgrade slot.";
        public override SpecificationType SpecType => SpecificationType.NonCritical;

        public override Expression<Func<IUpgrade, bool>> ToExpression()
        {
            return upgrade => upgrade.Cost <= costRestriction;
        }
    }
}
