using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Battler
{
    internal class Action_Battle : Action
    {
        public Action_Battle() : base("Battle","Find a wild creature to fight!")
        {
            
        }
        public override bool PerformAction(wildListing foeDesign)
        {
            return false;
        }
    }
}
