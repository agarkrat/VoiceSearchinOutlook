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

        public void PerformAction(string speechText)
        {
            speechText = Utility.TruncateActionString(speechText, "Reply");

            var item = Utility.GetNThItem(speechText);
            if (item is MailItem)
            {
                MailItem mailItem = (MailItem)item;
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

            Globals.ThisAddIn.Application.ActiveExplorer().ClearSelection();
        }
    }
}
