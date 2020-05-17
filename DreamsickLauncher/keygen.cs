using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamsickLauncher
{
    class keygen
    {
        public static Random random = new Random(0);

        public static string getKey()
        {
            char[] tokens = {'0', '1', '2', '3', '4', '5', '7', '8', '9', 'A', 'B', 'C',
        'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
        'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

            char[] codeArray = new char[24];

            for (int i = 0; i < 24; i++)
            {
                int index = random.Next(tokens.Length - 1);
                codeArray[i] = tokens[index];
            }

            for (int i = 0; i < 24; i++)
            {
                if (i % 5 == 4)
                {
                    codeArray[i] = '-';
                }
                else
                {
                    int index = random.Next(tokens.Length - 1);
                    codeArray[i] = tokens[index];
                }
            }

            return new String(codeArray);
        }
    }
}
