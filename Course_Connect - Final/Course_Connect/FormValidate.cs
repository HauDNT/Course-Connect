using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Connect
{
    class FormValidate
    {
        public static string FormatString(string stringInput)
        {
            string formatted = "";

            if (stringInput != "")
            {
                string[] splitCharactersArray = stringInput.Split(' ');

                for (int i = 0; i < splitCharactersArray.Length; i++)
                {
                    if (splitCharactersArray[i] != "")
                    {
                        string firstCharacter = splitCharactersArray[i].Substring(0, 1).ToUpper();
                        string anotherCharacters = splitCharactersArray[i].Substring(1).ToLower();

                        formatted += firstCharacter + anotherCharacters + " ";
                    }
                }

                formatted = formatted.Trim();
            }

            return formatted;
        }

        public static bool IsNumeric(string stringInput)
        {
            foreach (char c in stringInput)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
