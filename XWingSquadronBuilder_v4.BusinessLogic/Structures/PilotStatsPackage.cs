using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.BusinessLogic.Structures
{
    public struct PilotStatPackage
    {
        public int Agility { get; }
        public int Attack { get; }
        public int Hull { get; }
        public int PilotSkill { get; }
        public int Shield { get; }

        public PilotStatPackage(int attack, int agility, int hull, int shield, int pilotSkill)
        {
            this.Attack = attack;
            this.Agility = agility;
            this.Hull = hull;
            this.Shield = shield;
            this.PilotSkill = pilotSkill;
        }
    }
}
