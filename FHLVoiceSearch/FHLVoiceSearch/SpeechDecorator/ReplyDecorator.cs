using Microsoft.Office.Interop.Outlook;
using System;
using System.Linq;
using System.Threading.Tasks;
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

                replyItem.Display(false);

                VoiceSearch.speakItOut(" The Subject of the mail is : " + replyItem.Subject + ", You are Replying to " + replyItem.To).GetAwaiter().GetResult();
                //Task.Delay(1000).Wait();
                VoiceSearch.speakItOut("Tell me what you want to reply.").GetAwaiter().GetResult();
                //Task.Delay(2000).Wait();
                string body = VoiceSearch.RecognizeSpeechAsync().GetAwaiter().GetResult();
                replyItem.Body = body;

                VoiceSearch.speakItOut("Your Reply is ready. Do you want to Send or Discard?").GetAwaiter().GetResult();
                //Task.Delay(2000).Wait();

                string action = VoiceSearch.RecognizeSpeechAsync().GetAwaiter().GetResult();
                if (action.ToLower().Contains("send"))
                {
                    replyItem.Send();
                }
                else
                {
                    replyItem.Close(OlInspectorClose.olDiscard);
                }

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
