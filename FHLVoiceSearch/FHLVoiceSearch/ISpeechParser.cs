using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLVoiceSearch
{
    interface ISpeechParser
    {
        string ParseSpeechText(string speechText);

        void PerformAction(string speechText);
    }
}
