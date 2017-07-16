using System;
using System.Collections.Generic;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.BusinessLogic.Specifications;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;

namespace XWingSquadronBuilder_v4.BusinessLogic.Factories
{
    public class UpgradeRestrictionParser
    {
        public static IReadOnlyList<IXWingSpecification<IPilot>> ParseRestrictionsForUpgrade(UpgradeJson upgradeJson)
        {
            var specList = new List<IXWingSpecification<IPilot>>();
            var upgradeType = XWingRepository.Instance.UpgradeTypesRepository.GetUpgradeType(upgradeJson.Type);
            foreach (var restriction in upgradeJson.Restrictions)
            {
                switch (restriction.Attribute)
                {
                    case "ShipName":
                        {
                            if (restriction.Operand == "Contains")
                            {
                                specList.Add(new ContainsShipNameSpecification(restriction.Value));
                            }
                            else
                            {
                                throw new NotImplementedException($"Found a restriction not implemented\n {restriction.Operand}");
                            }
                            break;
                        }
                    case "ShipSize":
                        {
                            if (restriction.Operand == "Equals")
                            {
                                specList.Add(new EqualsShipSizeSpecification(restriction.Value));
                            }
                            else
                            {
                                throw new NotImplementedException($"Found a restriction not implemented\n {restriction.Operand}");
                            }
                            break;
                        }
                    case "PilotSkill":
                        {
                            if (restriction.Operand == ">")
                            {
                                specList.Add(new PilotSkillGreaterThanSpecification(int.Parse(restriction.Value)));
                            }
                            else
                            {
                                throw new NotImplementedException($"Found a restriction not implemented\n {restriction.Operand}");
                            }
                            break;
                        }
                    case "UpgradeSlots":
                        {                            
                            if (restriction.Operand == "Not Contains")
                            {
                                specList.Add(new NotContainsUpgradeSlotsSpecification(XWingRepository.Instance.UpgradeTypesRepository.GetUpgradeType(restriction.Value)));
                            }
                            else if (restriction.Operand == "Contains")
                            {
                                specList.Add(new ContainsUpgradeSlotsSpecification(XWingRepository.Instance.UpgradeTypesRepository.GetUpgradeType(restriction.Value)));
                            }
                            else
                            {
                                throw new NotImplementedException($"Found a restriction not implemented\n {restriction.Operand}");
                            }
                            break;
                        }
                    case "SlotsRequired":
                        {
                            if (restriction.Operand == "Count")
                            {
                                specList.Add(new PilotHasRequiredUpgradeSlotsSpecification(upgradeType, int.Parse(restriction.Value)));
                            }
                            else
                            {
                                throw new NotImplementedException($"Found a restriction not implemented\n {restriction.Operand}");
                            }
                            break;
                        }
                    case "Actions":
                        {
                            if (restriction.Operand == "Contains")
                            {
                                specList.Add(new ContainsActionSpecification(XWingRepository.Instance.ActionRepository.GetAction(restriction.Value)));
                            }
                            else
                            {
                                throw new NotImplementedException($"Found a restriction not implemented\n {restriction.Operand}");
                            }
                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException($"Found a restriction not implemented\n {restriction.Attribute}");
                        }
                }

            }
            return specList;
        }
        public static IReadOnlyList<IXWingSpecification<IUpgrade>> ParseRestrictionsForUpgradeSlot(List<RestrictionJson> restrictions)
        {
            var specList = new List<IXWingSpecification<IUpgrade>>();            
            foreach (var restriction in restrictions)
            {
                switch (restriction.Attribute)
                {
                    case "Cost":
                        {
                            if (restriction.Operand == "<=")
                            {
                                specList.Add(new UpgradeCostLessOrEqualToThanSpecification(int.Parse(restriction.Value)));
                            }
                            else
                            {
                                throw new NotImplementedException($"Found a restriction not implemented\n {restriction.Operand}");
                            }
                            break;
                        }                    
                    default:
                        {
                            throw new NotImplementedException($"Found a restriction not implemented\n {restriction.Attribute}");
                        }
                }

            }
            return specList;
        }
    }
}
