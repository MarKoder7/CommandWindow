using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommandWindow
{
    class CommandPrompt
    {
        ConsoleColor backgroundColor;
        ConsoleColor foregroundColor;
        string[] screenText;
        int height;

        public CommandPrompt(int height)
        {
            this.height = height;

            // set the backgroundColor to some default
            backgroundColor = ConsoleColor.Black;

            // set the foregroundColor to some default
            foregroundColor = ConsoleColor.Red;

            // create the screen to hold the number of rows passed in with the height parameter
            screenText = new string[height];

            // we will use the C# object to set the size of our window.
            Console.SetWindowSize(80, height + 7);

            // let's set the initial screen rows to all blank lines
            ClearScreen();
        } // end of CommandPrompt constructor

        /*
         * This will clear the screen, reset the colors and display the current text 
         */
        public void Display()
        {
            // set the foreground and background colors
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Clear();     //  the Console object is available to us to control aspects of our terminal window. 
                                 //  The Clear method will blank our window
                                 //  The Clear method has blanked the screen and left the cursor at the top of the window.

            // We will now loop through the screenText array to put out text on the screen.
            for (int i = 0; i < screenText.Length; i++)
            {
                Console.WriteLine(screenText[i]);
            }
        } // end of Display method

        /*
         * clear out the text buffer
         */
        public void ClearScreen()
        {
            // let's set the initial screen rows to all blank lines
            for (int i = 0; i < screenText.Length; i++)
            {
                screenText[i] = "";
            }
            Display();
        }

        /*
         * Set some text on the selected line number
         *
         * <param name="lineNumber"></param>
         * <param name="lineOfText"></param>
         */
        public void SetScreenText(int lineNumber, string lineOfText)
        {
            screenText[lineNumber] = lineOfText;
        } // end of SetScreenText method

        /*
         * set the Background color. but first we have to convert from the string name to the Console Color
         *
         * <param name="color"></param>
         */
        public void SetBackgroundColor(string color)
        {
            backgroundColor = ConvertColor(color);
        } // end of SetBackgroundColor

        /*
         * set the Foreground color. but first we have to convert from the string name to the Console Color
         *
         * <param name="color"></param>
         */
        public void SetForegroundColor(string color)
        {
            foregroundColor = ConvertColor(color);
        } // end of SetForegroundColor

        /*
         * convert the text from the user to a ConsoleColor object
         *
         * <param name="strColor"></param>
         * <returns></returns>
         */
        public static ConsoleColor ConvertColor(string strColor)
        {
            ConsoleColor color;
            switch (strColor.ToLower())
            {
                case "red": color = ConsoleColor.Red; break;
                case "yellow": color = ConsoleColor.Yellow; break;
                case "green": color = ConsoleColor.Green; break;
                case "cyan": color = ConsoleColor.Cyan; break;
                case "blue": color = ConsoleColor.Blue; break;
                case "magenta": color = ConsoleColor.Magenta; break;

                case "darkred": color = ConsoleColor.DarkRed; break;
                case "darkyellow": color = ConsoleColor.DarkYellow; break;
                case "darkgreen": color = ConsoleColor.DarkGreen; break;
                case "darkcyan": color = ConsoleColor.DarkCyan; break;
                case "darkblue": color = ConsoleColor.DarkBlue; break;
                case "darkmagenta": color = ConsoleColor.DarkMagenta; break;

                case "white": color = ConsoleColor.White; break;
                case "gray": color = ConsoleColor.Gray; break;
                case "darkgray": color = ConsoleColor.DarkGray; break;
                case "black": color = ConsoleColor.Black; break;

                default: color = ConsoleColor.DarkGray; break;
            }
            return color;
        } // end of ConvertColor method

        public void SaveScreen(string fileName)
        {
            StreamWriter textOut = null;
            try
            {
                fileName = "C:/Projects/" + fileName;
                textOut = new StreamWriter(fileName);
                textOut.WriteLine(height);
                textOut.WriteLine(foregroundColor);
                textOut.WriteLine(backgroundColor);
                for (int i = 0; i < screenText.Length; i++)
                {
                    textOut.WriteLine(screenText[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Creating file: \n" + ex.ToString());
            }
            finally
            {
                if (textOut != null)
                    textOut.Close();
            }
        }   //  this is the end of the SaveScreen code

        public void ReloadScreen(string fileName)
        {
            StreamReader textIn = null;
            try
            {
                fileName = "C:/Projects/" + fileName;
                textIn = new StreamReader(fileName);
                string text = textIn.ReadLine();
                Int32.TryParse(text, out height);
                screenText = new string[height];
                SetForegroundColor(textIn.ReadLine());
                SetBackgroundColor(textIn.ReadLine());

                int i = 0;
                while (true)
                {
                    text = textIn.ReadLine();
                    if (text == null)
                        break;
                    screenText[i++] = text;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Opening file: \n" + ex.ToString());
            }
            finally
            {
                if (textIn != null)
                    textIn.Close();
            }
        }
    } // end of CommandPrompt class
}