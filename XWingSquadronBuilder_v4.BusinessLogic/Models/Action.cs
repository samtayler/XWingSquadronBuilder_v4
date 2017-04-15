﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    public class Action : IAction
    {
        public Action(string name, string image)
        {
            Name = name;
            Image = image;
        }

        public string Name { get; }

        public string Image { get; }

        public IAction DeepClone()
        {
            return new Action(this.Name, this.Image);
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