using System;

namespace CommandWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            int menuOption;

            int windowSize = GetNumber("How big do you want your window? ", 30);
            CommandPrompt monitor = new CommandPrompt(windowSize);

            do
            {
                monitor.Display();
                menuOption = GetNumber(
                                  "\t0 - Power\n" +
                                  "\t1 - Background Color\t2 - Foreground Color\n" +
                                  "\t3 - Screen Text     \t4 - Clear Screen\n" +
                                  "\t5 - Save Screen     \t6 - Reload Screen\n" +
                                  "Menu Option: ", 7);
                switch (menuOption)
                {
                    case 1:
                        monitor.SetBackgroundColor(GetInput("Color? "));
                        break;
                    case 2:
                        monitor.SetForegroundColor(GetInput("Color? "));
                        break;
                    case 3:
                        int lineNum = GetNumber("Enter Line Number ", windowSize - 1);
                        string text = GetInput("? ");
                        monitor.SetScreenText(lineNum, text);
                        break;
                    case 4:
                        monitor.ClearScreen();
                        break;
                    case 5:
                        monitor.SaveScreen(GetInput("File: "));
                        break;
                    case 6:
                        monitor.ReloadScreen(GetInput("File: "));
                        break;
                    default:
                        monitor.ClearScreen();
                        break;
                }
            } while (menuOption != 0);
        }

        public static int GetNumber(string prompt, int maxNum)
        {
            int userNumber;
            string strNumber;
            do
            {
                strNumber = GetInput(prompt);
                if (Int32.TryParse(strNumber, out userNumber) && userNumber <= maxNum)
                {
                    break;
                }
                Console.WriteLine("Not an integer or is > " + maxNum + ".  Please Reenter.");
            } while (true);

            return userNumber;
        }

        public static string GetInput(string prompt)
        {
            Console.Write(prompt);
            string str = Console.ReadLine();
            return str;
        }
    }       //  end of the Program class. Above here is where ALL the methods are
}
