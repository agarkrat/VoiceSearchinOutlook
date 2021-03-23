using Microsoft.Office.Interop.Outlook;
using System;
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
            var item = Globals.ThisAddIn.Application.ActiveExplorer().Selection[1];
            if (item is MailItem)
            {
                MailItem mailItem = (MailItem)item;
                MailItem replyItem = mailItem.Reply();

                replyItem.Display(true);
            }
            else
            {
                Console.WriteLine(item.GetType());
            }
        }
    }
}
