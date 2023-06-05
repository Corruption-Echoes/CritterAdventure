using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Battler
{
    internal class Combat
    {
        public Party Player;
        public Party Enemy;
        public Combat(Party enemy) 
        { 
            Enemy = enemy;

        }

    }
}
