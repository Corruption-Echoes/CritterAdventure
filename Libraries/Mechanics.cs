using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tiny_Battler
{
    public static class Mechanics
    {
        public static List<float> statMultiplierStages = new List<float>() { 0.08f, 0.12f, 0.18f, 0.28f, 0.43f, 0.66f, 1f, 1.33f, 1.75f, 2.35f, 3.12f, 4.16f, 5.53f };
        public static int EVCAP = 252;
        
        public static Random randomGen=new Random();
        public static Dictionary<string, string[]> keyMaps = new Dictionary<string, string[]>() { {"Left", new string[]{ "LeftArrow", "A" }},{ "Right", new string[] { "RightArrow", "D" } },{ "Up", new string[] { "UpArrow", "W" } }, { "Down", new string[] { "DownArrow", "S" } }, { "Confirm", new string[] { "Enter", "Spacebar" } }, { "Cancel", new string[] { "Backspace", "Z" } } };
        public static List<xpScales> XPScales=new List<xpScales>();
        public static string[] types = { "Fire", "Water", "Air", "Earth", "Lightning", "Poison", "Nature", "Ice", "Steel", "Dark", "Light" ,"Null"};
        public static string[] stats = { "Health","Attack","Defense","Magic","Shield","Speed"};
        public static string[] statusEffects = { "Poison", "Paralysis", "Sleep", "Frozen", "Bleeding", "Concussed" ,"Burning"};
        public static Loader loader = new Loader();
        private static string CheckInput(string input)
        {
            foreach(string key in keyMaps.Keys)
            {
                if(keyMaps[key].Contains(input))
                {
                    return key;
                }
            }
            return null;
        }
        public static int generateIV()
        {
            return randomGen.Next(0, 32);
        }
        public static string getPlayerInput()
        {
            string intake = CheckInput(Console.ReadKey().Key.ToString());
            return intake;
        }
        public static int PickFromWeightedList(List<int> weights)
        {
            int sum=weights.Sum();
            int choice = randomGen.Next(0, sum);
            for(int i=0;i < weights.Count; i++)
            {
                choice -= weights[i];
                if (choice < 1)
                {
                    return i;
                }
            }
            return weights.Count - 1;
        }
        public static int[] stringToBST(string bst)
        {
            int[] toReturn = new int[6];
            //Console.WriteLine(bst);
            string[] split = bst.Split('/');
            for(int i=0;i<split.Length;i++)
            {
                //Console.WriteLine(split[i]);
                toReturn[i] = int.Parse(split[i]);
            }
            return toReturn;
        }
        public static int getStatNum(string stat)
        {
            for(int i = 0; i < stats.Length; i++)
            {
                if (stats[i] == stat)
                {
                    return i;
                }
            }
            return 0;
        }
        public static string getTypeFromSpeciesTemplate(int species, int Slot)
        {
            try
            {
                string type = Mechanics.types[Mechanics.loader.speciesTemplates[species].Types[Slot]];
                return type;
            }
            catch
            {
                return "null";
            }
        }
        public static string TypeEffectivenessToString(float multi)
        {
            if(multi > 1)
            {
                return "Super Effective!";
            }if (multi < 1)
            {
                return "Not very Effective!";
            }
            return "Effective.";
        }
        public static float CalculateTypeEffectiveness(string type, int species)
        {
            float multiplier = 1;
            string type1 = Mechanics.getTypeFromSpeciesTemplate(species, 0);
            string type2 = Mechanics.getTypeFromSpeciesTemplate(species, 1);

            //Welcome to hell, population 1 type effectiveness chart
            /*
             Fire is weak to Water, Poison and Earth
             Fire resists Light, Ice, Nature
             Fire beats Nature, Steel, Air, Ice
            */
            if ((type1 == "Fire" || type2 == "Fire") && (type == "Water" || type == "Earth" || type == "Poison"))
            {
                multiplier *= 2;
            }
            if ((type1 == "Fire" || type2 == "Fire") && (type == "Light" || type == "Ice" || type == "Nature"))
            {
                multiplier *= 0.5f;
            }
            /*
             Water is weak to Nature, Lightning, and Dark
             Water resists Ice, Air, and Fire
             Water beats Fire, Steel and Poison
            */
            if ((type1 == "Water" || type2 == "Water") && (type == "Nature" || type == "Lightning" || type == "Dark"))
            {
                multiplier *= 2;
            }
            if ((type1 == "Water" || type2 == "Water") && (type == "Ice" || type == "Air" || type == "Fire"))
            {
                multiplier *= 0.5f;
            }
            /*
             Air is weak to Nature, Ice, and Lightning
             Air resists Dark, Light, Earth
             Air beats Light, Earth and Dark
            */
            if ((type1 == "Air" || type2 == "Air") && (type == "Nature" || type == "Lightning" || type == "Ice"))
            {
                multiplier *= 2;
            }
            if ((type1 == "Air" || type2 == "Air") && (type == "Dark" || type == "Light" || type == "Earth"))
            {
                multiplier *= 0.5f;
            }
            /*
             Earth is weak to Air, Ice and Poison
             Earth resists Dark, Light and Nature
             Earth beats Fire, Lightning and Steel
            */
            if ((type1 == "Earth" || type2 == "Earth") && (type == "Air" || type == "Poison" || type == "Ice"))
            {
                multiplier *= 2;
            }
            if ((type1 == "Earth" || type2 == "Earth") && (type == "Dark" || type == "Light" || type == "Nature"))
            {
                multiplier *= 0.5f;
            }
            /*
             Lightning is weak to Earth, Steel, and Dark
             Lightning resists Fire, Light, Water
             Lightning beats Water, Air, Dark
            */
            if ((type1 == "Lightning" || type2 == "Lightning") && (type == "Earth" || type == "Steel" || type == "Dark"))
            {
                multiplier *= 2;
            }
            if ((type1 == "Lightning" || type2 == "Lightning") && (type == "Fire" || type == "Light" || type == "Water"))
            {
                multiplier *= 0.5f;
            }
            /*
             Poison is weak to Water, Steel, Light
             Poison resists Fire, Nature, Dark
             Poison beats Fire, Earth, Nature
            */
            if ((type1 == "Poison" || type2 == "Poison") && (type == "Water" || type == "Steel" || type == "Light"))
            {
                multiplier *= 2;
            }
            if ((type1 == "Poison" || type2 == "Poison") && (type == "Fire" || type == "Nature" || type == "Dark"))
            {
                multiplier *= 0.5f;
            }
            /*
             Nature is weak to Fire, Poison, and ice
             Nature resists Water, Air, and Dark
             Nature beats Water, Air and Light
            */
            if ((type1 == "Nature" || type2 == "Nature") && (type == "Fire" || type == "Poison" || type == "Ice"))
            {
                multiplier *= 2;
            }
            if ((type1 == "Nature" || type2 == "Nature") && (type == "Water" || type == "Air" || type == "Dark"))
            {
                multiplier *= 0.5f;
            }
            /*
             Ice is weak to Fire, Steel, and Light
             Ice resists Water, Dark and Poison
             Ice beats Nature, Earth, Air
            */
            if ((type1 == "Ice" || type2 == "Ice") && (type == "Fire" || type == "Steel" || type == "Light"))
            {
                multiplier *= 2;
            }
            if ((type1 == "Ice" || type2 == "Ice") && (type == "Water" || type == "Poison" || type == "Dark"))
            {
                multiplier *= 0.5f;
            }
            /*
             Steel is weak to Fire, Water and Earth
             Steel resists Air, Lightning and Poison
             Steel beats Lightning, Poison, Ice
            */
            if ((type1 == "Steel" || type2 == "Steel") && (type == "Fire" || type == "Water" || type == "Earth"))
            {
                multiplier *= 2;
            }
            if ((type1 == "Steel" || type2 == "Steel") && (type == "Air" || type == "Lightning" || type == "Poison"))
            {
                multiplier *= 0.5f;
            }
            /*
             Dark is weak to Light, Lightning and Air
             Dark resists Steel, Water and Ice
             Dark beats Light, Lightning, Water
            */
            if ((type1 == "Dark" || type2 == "Dark") && (type == "Light" || type == "Lightning" || type == "Air"))
            {
                multiplier *= 2;
            }
            if ((type1 == "Dark" || type2 == "Dark") && (type == "Steel" || type == "Water" || type == "Ice"))
            {
                multiplier *= 0.5f;
            }
            /*
             Light is weak to Dark, Nature, and Air
             Light resists Fire, Lightning and Ice
             Light beats Dark, Ice, Poison
             */
            if ((type1 == "Light" || type2 == "Light") && (type == "Dark" || type == "Nature" || type == "Air"))
            {
                multiplier *= 2;
            }
            if ((type1 == "Light" || type2 == "Light") && (type == "Fire" || type == "Lightning" || type == "Ice"))
            {
                multiplier *= 0.5f;
            }
            return multiplier;
        }
    }
    public class Loader{
        public List<moveTemplate> moveTemplates;
        public List<speciesTemplate> speciesTemplates;
        public Dictionary<string, List<learnPair>> levelupMoves;
        public Loader()
        { 
            moveTemplates = new List<moveTemplate>();
            loadMoveTemplates();
            speciesTemplates = new List<speciesTemplate>();
            loadSpeciesTemplates();
            levelupMoves=new Dictionary<string, List<learnPair>>();
            loadLearnLists();
        }
        public void loadMoveTemplates()
        {
            XmlTextReader textReader = new XmlTextReader("XMLStorage/Moves/moves.xml");
            textReader.Read();
            while (textReader.Read())
            {
                if (textReader.NodeType != XmlNodeType.EndElement)
                {
                    if (textReader.Name == "move")
                    {
                        moveTemplate m = new moveTemplate();
                        m.init();
                        moveTemplates.Add(m);
                    }
                    if (textReader.Name == "name")
                    {
                        textReader.Read();
                        moveTemplate m = moveTemplates[moveTemplates.Count - 1];
                        m.Name = textReader.Value;
                        moveTemplates[moveTemplates.Count - 1] = m;
                    }
                    if (textReader.Name == "description")
                    {
                        textReader.Read();
                        moveTemplate m = moveTemplates[moveTemplates.Count - 1];
                        m.Description = textReader.Value;
                        moveTemplates[moveTemplates.Count - 1] = m;
                    }
                    if (textReader.Name == "scale")
                    {
                        textReader.Read();
                        moveTemplate m = moveTemplates[moveTemplates.Count - 1];
                        m.Scale = textReader.Value;
                        moveTemplates[moveTemplates.Count - 1] = m;
                    }
                    if (textReader.Name == "mitigation")
                    {
                        textReader.Read();
                        moveTemplate m = moveTemplates[moveTemplates.Count - 1];
                        m.Mitigator = textReader.Value;
                        moveTemplates[moveTemplates.Count - 1] = m;
                    }
                    if (textReader.Name == "power")
                    {
                        textReader.Read();
                        moveTemplate m = moveTemplates[moveTemplates.Count - 1];
                        m.Power = int.Parse(textReader.Value);
                        moveTemplates[moveTemplates.Count - 1] = m;
                    }
                    if (textReader.Name == "basePP")
                    {
                        textReader.Read();
                        moveTemplate m = moveTemplates[moveTemplates.Count - 1];
                        m.PP = int.Parse(textReader.Value);
                        moveTemplates[moveTemplates.Count - 1] = m;
                    }
                    if (textReader.Name == "accuracy")
                    {
                        textReader.Read();
                        moveTemplate m = moveTemplates[moveTemplates.Count - 1];
                        m.Accuracy = int.Parse(textReader.Value);
                        moveTemplates[moveTemplates.Count - 1] = m;
                    }
                    if (textReader.Name == "type")
                    {
                        textReader.Read();
                        moveTemplate m = moveTemplates[moveTemplates.Count - 1];
                        //Console.WriteLine(Array.IndexOf(Mechanics.types, textReader.Value));
                        m.Type=Array.IndexOf(Mechanics.types,textReader.Value);
                        moveTemplates[moveTemplates.Count - 1] = m;
                    }
                    if (textReader.Name == "secondaryEffect")
                    {
                        textReader.Read();
                        moveTemplate m = moveTemplates[moveTemplates.Count - 1];
                        effect e = new effect();
                        e.init();

                        while (textReader.Name != "secondaryEffect")
                        {
                            if (textReader.NodeType != XmlNodeType.EndElement)
                            {
                                if(textReader.Name == "stat")
                                {
                                    textReader.Read();
                                    e.Status= Array.IndexOf(Mechanics.stats, textReader.Value);
                                }
                                else if (textReader.Name == "effect")
                                {
                                    textReader.Read();
                                    //Console.WriteLine(textReader.Value);
                                    e.Effect = int.Parse(textReader.Value);
                                }
                                else if (textReader.Name == "status")
                                {
                                    textReader.Read();
                                    e.Status = Array.IndexOf(Mechanics.statusEffects, textReader.Value);
                                }
                                else if (textReader.Name == "chance")
                                {
                                    textReader.Read();
                                    e.Chance = int.Parse(textReader.Value);
                                }
                            }
                            textReader.Read();
                        }
                        m.secondaryEffects.Add(e);
                    }
                }
            }
        }
        public void loadSpeciesTemplates()
        {
            XmlTextReader textReader = new XmlTextReader("XMLStorage/Species/species.xml");
            textReader.Read();
            while (textReader.Read())
            {
                if (textReader.NodeType != XmlNodeType.EndElement)
                {
                    if (textReader.Name == "species")                    {
                        speciesTemplate s = new speciesTemplate();
                        s.init();
                        speciesTemplates.Add(s);
                    }
                    else if (textReader.Name == "name")
                    {
                        textReader.Read();
                        speciesTemplate m = speciesTemplates[speciesTemplates.Count - 1];
                        m.Name = textReader.Value;
                        speciesTemplates[speciesTemplates.Count - 1] = m;
                    }
                    else if (textReader.Name == "type")
                    {
                        textReader.Read();
                        speciesTemplate m = speciesTemplates[speciesTemplates.Count - 1];
                        m.addType(Array.IndexOf(Mechanics.types, textReader.Value));
                        speciesTemplates[speciesTemplates.Count - 1] = m;
                    }
                    else if (textReader.Name == "ev")
                    {
                        textReader.Read();
                        speciesTemplate m = speciesTemplates[speciesTemplates.Count - 1];
                        m.EVGranted = Array.IndexOf(Mechanics.stats, textReader.Value);
                        speciesTemplates[speciesTemplates.Count - 1] = m;
                    }
                    else if (textReader.Name == "yield")
                    {
                        textReader.Read();
                        speciesTemplate m = speciesTemplates[speciesTemplates.Count - 1];
                        m.EVAmount = int.Parse(textReader.Value);
                        speciesTemplates[speciesTemplates.Count - 1] = m;
                    }
                    else if (textReader.Name == "bst")
                    {
                        textReader.Read();
                        speciesTemplate m = speciesTemplates[speciesTemplates.Count - 1];
                        m.BaseStats=Mechanics.stringToBST(textReader.Value);
                        speciesTemplates[speciesTemplates.Count - 1] = m;
                    }
                }
            }
        }
        public List<wildListing> GetWildListings(int zone)
        {
            XmlTextReader textReader = new XmlTextReader("XMLStorage/zones/zone"+zone+".xml");
            List<wildListing> wildListings = new List<wildListing>();
            textReader.Read();
            while (textReader.Read())
            {
                if (textReader.NodeType != XmlNodeType.EndElement)
                {
                    if (textReader.Name == "encounter")
                    {
                        wildListings.Add(new wildListing());
                    }
                    if (textReader.Name == "species")
                    {
                        textReader.Read();
                        wildListing w = wildListings[wildListings.Count - 1];
                        w.Species = int.Parse(textReader.Value);
                        wildListings[wildListings.Count - 1] = w;
                    }
                    if (textReader.Name == "min")
                    {
                        textReader.Read();
                        wildListing w = wildListings[wildListings.Count - 1];
                        w.LevelMin = int.Parse(textReader.Value);
                        wildListings[wildListings.Count - 1] = w;
                    }
                    if (textReader.Name == "max")
                    {
                        textReader.Read();
                        wildListing w = wildListings[wildListings.Count - 1];
                        w.LevelMax = int.Parse(textReader.Value);
                        wildListings[wildListings.Count - 1] = w;
                    }
                    if (textReader.Name == "weight")
                    {
                        textReader.Read();
                        wildListing w = wildListings[wildListings.Count - 1];
                        w.Weight = int.Parse(textReader.Value);
                        wildListings[wildListings.Count - 1] = w;
                    }
                    if (textReader.Name == "count")
                    {
                        Console.Write("count=");
                        textReader.Read();
                        Console.WriteLine(textReader.Value);
                    }
                }
            }
            return wildListings;
        }
        public void loadLearnLists()
        {
            XmlTextReader textReader = new XmlTextReader("XMLStorage/Species/MoveList.xml");
            textReader.Read();
            string currentSpecies = "";
            while (textReader.Read())
            {
                if (textReader.NodeType != XmlNodeType.EndElement)
                {
                    if (textReader.Name == "species")
                    {
                        textReader.Read();
                        textReader.Read();
                        textReader.Read();
                        List<learnPair> moves = new List<learnPair>();
                        levelupMoves.Add(textReader.Value, moves);
                        currentSpecies= textReader.Value;
                    }
                    else if (textReader.Name == "movename")
                    {
                        textReader.Read();
                        string name= textReader.Value;
                        textReader.Read();
                        Console.WriteLine(textReader.Value);
                        textReader.Read();
                        Console.WriteLine(textReader.Value);
                        textReader.Read();
                        textReader.Read();
                        Console.WriteLine(textReader.Value);
                        int level = int.Parse(textReader.Value);
                        learnPair lp=new learnPair(name,level);
                        levelupMoves[currentSpecies].Add(lp);
                    }
                }
            }
        }
        public List<Move> GetDefaultMoves(string species, int level)
        {
            List<Move> toReturn=new List<Move>();
            foreach(learnPair lp in levelupMoves[species])
            {
                if (lp.Level <= level)
                {
                    toReturn.Add(new Move(getMoveIndexByName(lp.MoveName)));
                }
            }
            return toReturn;
        }
        public moveTemplate getMoveByName(string name)
        {
            foreach(moveTemplate mt in moveTemplates)
            {
                if(mt.Name == name)
                {
                    return mt;
                }
            }
            return moveTemplates[0];
        }
        public int getMoveIndexByName(string name)
        {
            for(int i = 0; i < moveTemplates.Count; i++)
            {
                if (moveTemplates[i].Name == name)
                {
                    return i;
                }
            }
            return 0;
        }
    }
    public struct wildListing
    {
        public int Species;
        public int LevelMin;
        public int LevelMax;
        public int Weight;

        public wildListing(int species,int min,int max,int weight)
        {
            Species = species;
            LevelMin = min;
            LevelMax = max;
            Weight = weight;
        }
        public int pickLevel()
        {
            return Mechanics.randomGen.Next(LevelMin, LevelMax + 1);
        }
    }
    public struct point 
    {
        public int X;
        public int Y;
        public point(int x,int y)
        {
            X = x;
            Y = y;
        }
    }
    public struct moveTemplate
    {
        public string Name;
        public string Description;
        public int Power;
        public int PP;
        public int Accuracy;
        public int Type;
        public string Scale;
        public string Mitigator;
        public List<effect> secondaryEffects;
        public void init()
        {
            Type = 0;
            secondaryEffects = new List<effect>();
            Power = 0;
            PP = 40;
            Accuracy = 100;
            Scale = "null";
            Mitigator= "null";
        }
    }
    public struct learnPair
    {
        public string MoveName;
        public int Level;
        public learnPair(string move, int level)
        {
            MoveName = move;
            Level = level;
        }
    }
    public struct effect
    {
        public int Chance;
        public int StatEffected;
        public int Effect;
        public int Status;
        public void init()
        {
            Chance = 0;
            StatEffected = 0;
            Effect = 0;
            Status = 0;
        }
    }
    public struct speciesTemplate
    {
        public string Name;
        public int[] BaseStats;
        public int[] Types;
        public int XPScale;
        public int XPgranted;
        public int EVGranted;
        public int EVAmount;
        bool firstType;
        public void init()
        {
            XPScale = 0;
            EVGranted = 0;
            EVAmount = 1;
            XPgranted = 25;
            Types = new int[2];
            Types[0] = 0;
            Types[1] = 18;
            firstType= true; 
        }
        public void addType(int type)
        {
            if (firstType)
            {
                Types[0] = type;
            }
            else
            {
                Types[1] = type;
            }
        }
    }
    public struct xpScales
    {
        public int[] LevelTiers;
        public void init(double scale)
        {
            LevelTiers = new int[100];
            LevelTiers[0] = 1;
            LevelTiers[1] = 100;
            for(int i = 2; i < LevelTiers.Length; i++)
            {
                LevelTiers[i] = (int)(LevelTiers[i - 1] * i*((i-1)*scale));
            }
        }
    }

}
