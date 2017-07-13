using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.Interfaces;
using System.Linq.Dynamic;
using XWingSquadronBuilder_v4.BusinessLogic.Specifications;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;

namespace XWingSquadronBuilder_v4.BusinessLogic.Factories
{

    public class UpgradeRestrictionParser
    {
        public static List<Specification<IPilot>> ParseRestrictionsForUpgrade(UpgradeJson upgradeJson)
        {
            var li = new List<Specification<IPilot>>();
            var upgradeType = XWingRepository.Instance.UpgradeTypesRepository.GetUpgradeType(upgradeJson.Type);
            foreach (var restriction in upgradeJson.Restrictions)
            {
                switch (restriction.Attribute)
                {
                    case "ShipName":
                        {
                            if (restriction.Operand == "Contains")
                            {
                                li.Add(new ContainsShipNameSpecification(restriction.Value));
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
                                li.Add(new EqualsShipSizeSpecification(restriction.Value));
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
                                li.Add(new PilotSkillGreaterThanSpecification(int.Parse(restriction.Value)));
                            }
                            else
                            {
                                throw new NotImplementedException($"Found a restriction not implemented\n {restriction.Operand}");
                            }
                            break;
                        }
                    case "UpgradeSlots":
                        {
                            if (restriction.Operand == "Count")
                            {
                                li.Add(new PilotHasRequiredUpgradeSlotsSpecification(upgradeType, int.Parse(restriction.Value)));
                            }
                            if (restriction.Operand == "Not Contains")
                            {
                                li.Add(new ContainsUpgradeSlotsSpecification(upgradeType).Not());
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
            return li;
        }
        public static List<Specification<IUpgrade>> ParseRestrictionsForUpgradeSlot(List<RestrictionJson> restrictions)
        {
            var li = new List<Specification<IUpgrade>>();            
            foreach (var restriction in restrictions)
            {
                switch (restriction.Attribute)
                {
                    case "Cost":
                        {
                            if (restriction.Operand == "<=")
                            {
                                li.Add(new UpgradeCostLessOrEqualToThanSpecification(int.Parse(restriction.Value)));
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
            return li;
        }
    }
}
