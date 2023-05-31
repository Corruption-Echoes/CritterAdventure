using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Battler
{
    internal class Action
    {
        public string Name;
        public string Description;
        public Action(string name, string description) 
        { 
            Name = name;
            Description = description;
        }
        public string printMenu()
        {
            return(Name + ": " + Description);
        }
        public virtual bool PerformAction()
        {//Return false when the action is complete!
            return false;
        }
        public virtual bool PerformAction(wildListing foeDesign)
        {
            return false;
        }
    }
}
