using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Battler
{
    public class Move
    {
        public int ID;
        public int CPP;
        public Move(int iD)
        {
            ID = iD;
            CPP = Program.XMLLoader.moveTemplates[ID].PP;
        }
        public moveTemplate MT()
        {
            return Program.XMLLoader.moveTemplates[ID];
        }
        public bool isUsable()
        {
            if(CPP>0) { 
                return true;
            }
            return false;
        }
    }
}
