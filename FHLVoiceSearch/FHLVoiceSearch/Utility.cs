using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLVoiceSearch
{
    class Utility
    {
        public static string TruncateActionString(string speechText, string actionString)
        {
            speechText = speechText.Trim();
            if (speechText.StartsWith(actionString))
            {
                speechText = speechText.Substring(actionString.Length);
            }
            return speechText;
        }
        
        public static string GetDigits(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            string cleaned = new string(s.Where(char.IsDigit).ToArray());
            return cleaned;
        }

        public static object GetNThItem(string speechText)
        {
            string indexString = Utility.GetDigits(speechText);

            bool isParsable = Int32.TryParse(indexString, out int index);
            if (!isParsable)
            {
                index = 1;
            }
            else
            {
                Globals.ThisAddIn.Application.ActiveExplorer().SelectAllItems();
            }

            return Globals.ThisAddIn.Application.ActiveExplorer().Selection[index];
        }
    }
}
