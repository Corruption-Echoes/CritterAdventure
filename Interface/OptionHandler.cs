using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny_Battler.Interface
{
    internal class OptionHandler
    {
        public int currentSelection;
        public List<string> Options;
        public bool selected = false;
        public OptionHandler(List<string> options)
        {
            currentSelection = 0;
            Options= options;
        }
        public int getSelection()
        {
            while (!selected)
            {
                Console.SetCursorPosition(Mechanics.OptionsOffset.X, Mechanics.OptionsOffset.Y);
                for (int i = 0; i < Options.Count; i++)
                {
                    if (i == currentSelection)
                    {
                        Console.BackgroundColor = Mechanics.HighlightColor;
                        Console.ForegroundColor = Mechanics.HighlightText;
                    }
                    else
                    {
                        Console.BackgroundColor = Mechanics.DefaultColor;
                        Console.ForegroundColor = Mechanics.TextColor;
                    }
                    Console.WriteLine(Options[i]);
                }
                string input = Mechanics.getPlayerInput();
                if (input == "Up")
                {
                    currentSelection--;
                }
                else if (input == "Down")
                {
                    currentSelection++;
                }
                else if (input == "Confirm")
                {
                    selected = true;
                }
            }
            return currentSelection;
        }
    }
}
