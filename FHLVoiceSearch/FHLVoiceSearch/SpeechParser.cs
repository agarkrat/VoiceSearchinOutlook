using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLVoiceSearch
{
    class SpeechParser : ISpeechParser
    {
        public string ParseSpeechText(string speechText)
        {
            return speechText;
        }

        public void PerformAction(string speechText)
        {
            return;
        }
    }
}
