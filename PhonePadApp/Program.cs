using System;
using System.Collections.Generic;
using System.Text;

public class PhonePad
{
    private static readonly Dictionary<char, string> keypad = new Dictionary<char, string>
    {
        {'1', "&'("},
        {'2', "ABC"},
        {'3', "DEF"},
        {'4', "GHI"},
        {'5', "JKL"},
        {'6', "MNO"},
        {'7', "PQRS"},
        {'8', "TUV"},
        {'9', "WXYZ"},
        {'0', " "}
    };

    public static void Main(string[] args)
    {
        Console.WriteLine(OldPhonePad("4433555 555666096667775553*#"));
    }

    public static string OldPhonePad(string input)
    {
        Stack<char> dialStack = new Stack<char>();
        char lastChar = '\0';
        int count = 0;
        foreach (char c in input)
        {
            if (c == '#')
            {
                break;
            }
            else if (c == '*')
            {
                if (lastChar != '\0')
                {
                    dialStack.Push(GetMappedCharacter(lastChar, count));
                    lastChar = '\0';
                    count = 0;
                }
                if (dialStack.Count > 0)
                    dialStack.Pop(); 
                
            }
            else if (c == ' ')
            {
                if (lastChar != '\0')
                    dialStack.Push(GetMappedCharacter(lastChar, count));
                
                lastChar = '\0';
                count = 0;
            }
            else if (keypad.ContainsKey(c))
            {
                if (c == lastChar)
                    count++;
                
                else
                {
                    if (lastChar != '\0')
                        dialStack.Push(GetMappedCharacter(lastChar, count));
                    
                    lastChar = c;
                    count = 1;
                }
            }
        }

        if (lastChar != '\0')
            dialStack.Push(GetMappedCharacter(lastChar, count));
        
        char[] resultArray = dialStack.ToArray();
        Array.Reverse(resultArray);

        return new string(resultArray);
    }

    private static char GetMappedCharacter(char key, int count)
    {
        string chars = keypad[key];
        int index = count - 1;
        if (index >= chars.Length)
            index = 0; 

        return chars[index % chars.Length];
    }
}
