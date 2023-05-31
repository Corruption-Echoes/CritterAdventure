using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Battler
{
    internal class Creature
    {
        public int currentHP;
        public int level;
        public int xp;
        public int[] EVS;
        public int[] IVS;
        public int[] buffStates;
        public int species;
        public string nickname;
        public List<Move> moves;
        
        public Creature(int species, int[] dvs, int[] ivs)
        {
            this.species= species;
            EVS = dvs;
            IVS = ivs;
            currentHP=determineStat(Species.Health);
        }
        public void setEVS(int[] evIn)
        {
            EVS = evIn;
        }
        public string getName()
        {
            if(nickname != null)
            {
                return nickname;
            }else 
            {
                return Species.speciesNames[species];
            }
        }
        public void grantEV(int amount, int stat)
        {
            EVS[stat] += amount;
            if (EVS[stat] > Mechanics.EVCAP)
            {
                EVS[stat]=Mechanics.EVCAP;
            }
        }
        public bool grantXP(int amount)
        {
            xp += amount;
            if (xp > Species.xpTiers[species][level])
            {
                level++;
                xp = 0;
                currentHP = determineStat(Species.Health);
                return true;
            }
            return false;
        }
        public int determineStat(int stat)
        {
            return (int)((Species.speciesStatBlocks[species][stat] / 50 * level) + (EVS[stat] /100*level) + (IVS[stat] / 400 * level) * Mechanics.statMultiplierStages[buffStates[stat]]);
        }
    }
}
