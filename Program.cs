using System;
using System.Text;
using System.IO;

namespace DVRKeyBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] matrix = new char[, ] { {'A', 'B','C','D','E','F'}, 
            {'G', 'H','I','J','K','L'}, {'M', 'N','O','P','Q','R'},{'S', 'T','U','V','W','X'},
            {'Y', 'Z','1','2','3','4'}, {'5', '6','7','8','9','0'} };

            //Get file
            string[] lines = System.IO.File.ReadAllLines(@"C:\Development\OnScreenKeyBoard\Input.txt");
            //output File
            string outputFile = "C:\\Development\\OnScreenKeyboard\\output.csv";
            var currPosWidth = 0;
            var currPosHeight = 0;
            var result = "";
            StringBuilder sbOutput = new StringBuilder();
            foreach (string line in lines)
            {
                currPosWidth = 0;
                currPosHeight = 0;
                result = "";
                foreach (char c in line.ToUpper())
                {
                    if (c == ' ')
                    {
                        result = result + "S,";
                    }
                    else
                    {
                        for (int w = 0; w < matrix.GetLength(0); w++)
                        {
                            for (int h = 0; h < matrix.GetLength(0); h++)
                            {
                                if (matrix[w, h] == c)
                                {
                                    result = result + GetResult(currPosWidth, currPosHeight, w, h);
                                    currPosWidth = w;
                                    currPosHeight = h;
                                }
                            }
                        }
                    }           
                }
                //remove last character in result
                result = result.Remove(result.Length - 1);
                sbOutput.AppendLine(result);
            }
            File.WriteAllText(outputFile, sbOutput.ToString());
        }

        private static string GetResult(int currPosWidth, int currPosHeight, int w, int h)
        {
            var value = "";
            var width = currPosWidth - w;
            var height = currPosHeight - h;
            while (width != 0)
            {
                if (width < 0)
                {
                    value = value + "D,";
                    width++;
                }
                else if (width > 0)
                {
                    value = value + "U,";
                    width--;
                }
            }
            while (height != 0)
            {
                if (height < 0)
                {
                    value = value + "R,";
                    height++;
                }
                else if (height > 0)
                {
                    value = value + "L,";
                    height--;
                }
            }
            value = value + "#,";
            return value;
        }
    }
}
