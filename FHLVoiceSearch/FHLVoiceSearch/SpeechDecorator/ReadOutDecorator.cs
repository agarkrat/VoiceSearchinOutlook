using Microsoft.Office.Interop.Outlook;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FHLVoiceSearch.SpeechDecorator
{
    class ReadOutDecorator : ISpeechParser
    {
        private ISpeechParser speechParser;

        public ReadOutDecorator(ISpeechParser speechParser)
        {
            this.speechParser = speechParser;
        }

        public string ParseSpeechText(string speechText)
        {
            return this.speechParser.ParseSpeechText(speechText);
        }
        
        public void PerformAction(string speechText)
        {
            speechText = Utility.TruncateActionString(speechText, "Read out");

            var item = Utility.GetNThItem(speechText);

            if (item is MailItem)
            {
                MailItem mailItem = (MailItem)item;
                VoiceSearch.speakItOut(" The mail says: " + mailItem.Body).GetAwaiter().GetResult();
            }
            else
            {
                Console.WriteLine(item.GetType());
            }
            
            Globals.ThisAddIn.Application.ActiveExplorer().ClearSelection();

        }
    }
}
