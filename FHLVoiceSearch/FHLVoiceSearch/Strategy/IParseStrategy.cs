using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLVoiceSearch.Strategy
{
    interface IParseStrategy
    {
        ISpeechParser GetParser(string speechText);
    }
}
