using Microsoft.Office.Interop.Outlook;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FHLVoiceSearch.SpeechDecorator
{
    class FlagDecorator : ISpeechParser
    {
        private ISpeechParser speechParser;

        public FlagDecorator(ISpeechParser speechParser)
        {
            this.speechParser = speechParser;
        }

        public string ParseSpeechText(string speechText)
        {
            return this.speechParser.ParseSpeechText(speechText);
        }


        public void PerformAction(string speechText)
        {
            speechText = Utility.TruncateActionString(speechText, "Flag");

            var item = Utility.GetNThItem(speechText);
            
            if (item is MailItem)
            {
                MailItem mailItem = (MailItem)item;
                mailItem.FlagRequest = "Flag";
                mailItem.FlagStatus = OlFlagStatus.olFlagMarked;
                mailItem.FlagDueBy = DateTime.Today;
                mailItem.MarkAsTask(OlMarkInterval.olMarkToday);
                VoiceSearch.speakItOut(" Flagged the mail with subject: " + mailItem.Subject);
            }
            else
            {
                Console.WriteLine(item.GetType());
            }
            
            Globals.ThisAddIn.Application.ActiveExplorer().ClearSelection();

        }
    }
}