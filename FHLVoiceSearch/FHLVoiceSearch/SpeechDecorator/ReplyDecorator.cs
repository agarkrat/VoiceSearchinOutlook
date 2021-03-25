using Microsoft.Office.Interop.Outlook;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FHLVoiceSearch.SpeechDecorator
{
    class ReplyDecorator : ISpeechParser
    {

        private ISpeechParser speechParser;

        public ReplyDecorator(ISpeechParser speechParser)
        {
            this.speechParser = speechParser;
        }

        public string ParseSpeechText(string speechText)
        {
            return this.speechParser.ParseSpeechText(speechText);
        }

        public void PerformAction(string speechText)
        {
            speechText = Utility.TruncateActionString(speechText, "Reply");

            var item = Utility.GetNThItem(speechText);
            if (item is MailItem)
            {
                MailItem mailItem = (MailItem)item;
                MailItem replyItem = mailItem.Reply();
                Globals.ThisAddIn.Application.ActiveExplorer().ClearSelection();
                VoiceSearch.speakItOut(" The Subject of the mail is : " + replyItem.Subject + ", You are Replying to " + replyItem.To);

                replyItem.Display(true);
            }
            else
            {
                Console.WriteLine(item.GetType());
            }


        }
    }
}
