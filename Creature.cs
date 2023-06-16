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
        public int Level;
        public int xp;
        public int[] EVS;
        public int[] IVS;
        public int[] buffStates;
        public int species;
        public string nickname;
        public List<Move> moves;
        
        public Creature(int species, int[] dvs, int[] ivs,int level)
        {
            this.species= species;
            EVS = dvs;
            IVS = ivs;
            Level = level;
            buffStates = new int[]{ 6,6,6,6,6};
            currentHP=determineStat(Species.Health);
            moves = Mechanics.loader.GetDefaultMoves(Mechanics.loader.speciesTemplates[species].Name,level);
        }
        public bool isFainted()
        {
            if (currentHP > 0)
            {
                return false;
            }
            return true;
        }
        public int calculateDamage(int move)
        {//Damage=(Power/100)*stat*(STAB)
            float SameType = 1;
            if (Mechanics.loader.moveTemplates[move].Type == Mechanics.loader.speciesTemplates[species].Types[0]|| Mechanics.loader.moveTemplates[move].Type == Mechanics.loader.speciesTemplates[species].Types[1])
            {
                SameType = 1.2f;
            }
            int damage = (int)((float)(Mechanics.loader.moveTemplates[move].Power/100f+1)* determineStat(Mechanics.getStatNum(Mechanics.loader.moveTemplates[move].Scale))* SameType);
            //Console.WriteLine(damage+" initially calculated.");
            return damage;
        }
        public void dealDamage(float amount, string damageType, string mitigationStat)
        {//Damage=(incoming*TypeEffectiveness/(stat/100))
            int stat = Mechanics.getStatNum(mitigationStat);
            //Console.WriteLine(stat+" : "+mitigationStat);
            //Console.WriteLine(amount);
            //Console.WriteLine(Mechanics.CalculateTypeEffectiveness(damageType, species));
            //Console.WriteLine();
            currentHP -= (int)(amount * (float)(Mechanics.CalculateTypeEffectiveness(damageType,species)/(determineStat(stat)/Level)));
            Console.WriteLine((int)(amount * (float)(Mechanics.CalculateTypeEffectiveness(damageType, species) / (determineStat(stat) / Level)))+" Damage was dealt!");
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
                return Mechanics.loader.speciesTemplates[species].Name;
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
            if (xp > Mechanics.XPScales[Mechanics.loader.speciesTemplates[species].XPScale].LevelTiers[Level])
            {
                Level++;
                xp = 0;
                currentHP = determineStat(Species.Health);
                return true;
            }
            return false;
        }
        public int getXPToLevel()
        {
            return Mechanics.XPScales[Mechanics.loader.speciesTemplates[species].XPScale].LevelTiers[Level];
        }
        public int determineStat(int stat)
        {
            //Console.WriteLine("Determing a stat: "+stat+ "It was"+ ((Mechanics.loader.speciesTemplates[species].BaseStats[stat] / 50 * Level) + (EVS[stat] / 100 * Level) + (IVS[stat] / 400 * Level)));
            //Console.WriteLine("This was determing by adding "+ (Mechanics.loader.speciesTemplates[species].BaseStats[stat] / 50 * Level) +"To "+ (EVS[stat] / 100 * Level) +"and "+ (IVS[stat] / 400 * Level));
            if(stat == 0)
            {
                return (int)((Mechanics.loader.speciesTemplates[species].BaseStats[stat] / 50 * Level) + (EVS[stat] / 100 * Level) + (IVS[stat] / 400 * Level))*4;
            }
            return (int)((Mechanics.loader.speciesTemplates[species].BaseStats[stat] / 50 * Level) + (EVS[stat] /100*Level) + (IVS[stat] / 400 * Level) * Mechanics.statMultiplierStages[buffStates[stat-1]]);
        }
        public static int[] generateIVS()
        {
            return new int[]{
            Mechanics.generateIV(),
            Mechanics.generateIV(),
            Mechanics.generateIV(),
            Mechanics.generateIV(),
            Mechanics.generateIV(),
            Mechanics.generateIV()
            };
        }
        public List<string> getMoveNames()
        {
            List<string> toReturn = new List<string>();
            //Now we need to handle giving pokemon moves!
            //Which means actually doing level learn lists per species
            foreach (Move m in moves)
            {
                toReturn.Add(Mechanics.loader.moveTemplates[m.ID].Name);
            }
            return toReturn;
        }
        public static int[] ZeroIV = new int[] { 0, 0, 0, 0, 0, 0 };
        public static int[] ZeroBuff = new int[] { 6, 6, 6, 6, 6 };
    }
}
