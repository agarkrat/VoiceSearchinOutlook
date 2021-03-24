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

        private string GetDigits(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            string cleaned = new string(s.Where(char.IsDigit).ToArray());
            return cleaned;
        }

        public void PerformAction(string speechText)
        {
            speechText = speechText.Trim();
            if (speechText.StartsWith("Reply"))
            {
                speechText = speechText.Substring(5);
            }

            speechText = GetDigits(speechText);


            bool isParsable = Int32.TryParse(speechText, out int index);
            if (!isParsable)
            {
                index = 1;
            }
            Globals.ThisAddIn.Application.ActiveExplorer().SelectAllItems();
            var item = Globals.ThisAddIn.Application.ActiveExplorer().Selection[index];
            
            if (item is MailItem)
            {
                MailItem mailItem = (MailItem)item;
                // To-do: Explore if a mail can be flaged
                // mailItem.MarkAsTask(OlMarkInterval.olMarkNoDate);
                MailItem replyItem = mailItem.Reply();

                VoiceSearch.speakItOut(" The Subject of the mail is : " + replyItem.Subject + ", You are Replying to " + replyItem.To);

                replyItem.Display(true);
            }
            else
            {
                Console.WriteLine(item.GetType());
            }

            /*
            // Todo: unselect selection
            int length = Globals.ThisAddIn.Application.ActiveExplorer().Selection.Count;
            for (int i=1; i < length; i++)
            {
                MailItem mailItem = (MailItem) Globals.ThisAddIn.Application.ActiveExplorer().Selection[i]; ;
                Globals.ThisAddIn.Application.ActiveExplorer().RemoveFromSelection(mailItem);
            }
            */
        }
    }
}
