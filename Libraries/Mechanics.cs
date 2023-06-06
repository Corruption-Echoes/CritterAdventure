using System;
using System.Collections.Generic;
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
        public static point OptionsOffset=new point(0,0);
        public static ConsoleColor HighlightColor = ConsoleColor.Green;
        public static ConsoleColor DefaultColor = ConsoleColor.Black;
        public static ConsoleColor TextColor=ConsoleColor.Yellow;
        public static ConsoleColor HighlightText = ConsoleColor.White;
        public static ConsoleColor WarningColor = ConsoleColor.Red;
        public static Random randomGen=new Random();
        public static Dictionary<string, string[]> keyMaps = new Dictionary<string, string[]>() { {"Left", new string[]{ "LeftArrow", "A" }},{ "Right", new string[] { "RightArrow", "D" } },{ "Up", new string[] { "UpArrow", "W" } }, { "Down", new string[] { "DownArrow", "S" } }, { "Confirm", new string[] { "Enter", "Spacebar" } }, { "Cancel", new string[] { "Backspace", "Z" } } };
        public static List<xpScales> XPScales=new List<xpScales>();
        public static string[] types = { "Normal","Fire","Water","Grass","Flying","Ice","Fairy","Steel","Dark","Dragon","Poison","Fighting","Rock","Ground","Ghost","Psychic","Electric","Bug","Null"};
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
    }
    public class Loader{
        public List<moveTemplate> moveTemplates;
        public List<speciesTemplate> speciesTemplates;
        public Loader()
        { 
            moveTemplates = new List<moveTemplate>();
            loadMoveTemplates();
            speciesTemplates = new List<speciesTemplate>();
            loadSpeciesTemplates();
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
        public List<effect> secondaryEffects;
        public void init()
        {
            Type = 0;
            secondaryEffects = new List<effect>();
            Power = 0;
            PP = 40;
            Accuracy = 100;
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
