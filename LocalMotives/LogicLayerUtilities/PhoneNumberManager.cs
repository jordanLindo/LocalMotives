using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicLayerUtilities
{
    public static class PhoneNumberManager
    {

        public static string PhoneNumberFixer(string phoneNumber)
        {
            string pattern = @"\d";

            char[] characters = phoneNumber.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < characters.Length; i++)
            {
                if (Regex.IsMatch(characters[i].ToString(), pattern))
                {
                    builder.Append(characters[i]);
                }
            }
            phoneNumber = builder.ToString();
            return phoneNumber;
        }

        public static bool IsGoodLengthAfterFix(string phoneNumber)
        {
            bool result = false;

            if (phoneNumber.Length == 7
                || phoneNumber.Length == 10
                || phoneNumber.Length == 11
                || phoneNumber.Length == 12)
            {
                result = true;
            }

            return result;
        }

        public static string FormatPhoneNumberForOutput(string phoneNumber)
        {
            int numberLength = phoneNumber.Length;
            StringBuilder sb = new StringBuilder();

            switch (numberLength)
            {
                case 7:
                    for (int i = 0; i < phoneNumber.Length; i++)
                    {
                        sb.Append(phoneNumber[i]);
                        if(i == 2)
                        {
                            sb.Append("-");
                        }
                    }
                    break;
                case 10:
                    for (int i = 0; i < phoneNumber.Length; i++)
                    {
                        if(i == 0)
                        {
                            sb.Append("(");
                        }
                        sb.Append(phoneNumber[i]);
                        if (i == 2)
                        {
                            sb.Append(")-");
                        }
                        else if(i == 5)
                        {
                            sb.Append("-");
                        }
                    }
                    break;
                case 11:
                    for (int i = 0; i < phoneNumber.Length; i++)
                    {
                        sb.Append(phoneNumber[i]);
                        if(i == 0 || i == 3 || i == 6)
                        {
                            sb.Append("-");
                        }
                    }
                    break;
                case 12:
                    for (int i = 0; i < phoneNumber.Length; i++)
                    {
                        sb.Append(phoneNumber[i]);
                        if (i == 1 || i == 4 || i == 7)
                        {
                            sb.Append("-");
                        }
                    }
                    break;
                default:
                    break;
            }
            return sb.ToString();
        }
    }
}
