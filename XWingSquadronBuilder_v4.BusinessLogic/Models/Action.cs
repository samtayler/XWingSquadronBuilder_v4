using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    [DataContract]
    public class Action : IAction
    {
        private Action(string name, string imageUri)
        {
            Name = name;
            ImageUri = imageUri;
        }

        [DataMember]
        public string Name { get; }
        [DataMember]
        public string ImageUri { get; }

        internal static IAction CreateAction(string name, string imageUri)
        {
            return new Action(name, imageUri);
        }

        public int CompareTo(IAction other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public IAction DeepClone()
        {
            return new Action(this.Name, this.ImageUri);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public bool Equals(IAction other)
        {
            return Name == other.Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
