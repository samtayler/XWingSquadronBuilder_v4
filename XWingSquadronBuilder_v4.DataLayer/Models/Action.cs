using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.DataLayer.Models
{
    [DataContract]
    public class Action : IAction
    {
        public Action(string name, string image)
        {
            Name = name;
            Image = image;
        }

        [DataMember]
        public string Name { get; }

        [DataMember]
        public string Image { get; }

        public IAction DeepClone()
        {
            return new Action(this.Name, this.Image);
        }

        public bool Equals(IAction other) => Name == other.Name;

        public override string ToString() => Name;
       

        

    }
}
