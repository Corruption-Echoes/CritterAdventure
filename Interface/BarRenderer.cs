using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Battler.Interface
{
    public static class BarRenderer
    {
        
        public static void drawHPBar(int min,int max, bool WriteValue)
        {
            drawbar(min,max,WriteValue,InterfaceOptions.HighlightColor,InterfaceOptions.WarningColor);
        }
        public static void drawXPBar(int min,int max,bool WriteValue)
        {
            drawbar(min, max, WriteValue, InterfaceOptions.xpBlue, InterfaceOptions.darkBlue);
        }
        public static void drawbar(int min, int max, bool WriteValue, ConsoleColor foreground, ConsoleColor background)
        {
            int Percent = (int)((float)min / max * 10f);
            for (int i = 0; i < InterfaceOptions.BarLengths; i++)
            {
                if (i < Percent)
                {
                    Console.ForegroundColor = foreground;
                }
                else
                {
                    Console.ForegroundColor = background;
                }
                Console.Write("■");
            }
            if (WriteValue)
            {
                Console.Write(min + "/" + max);
            }
        }
    }
}
