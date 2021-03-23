using FHLVoiceSearch.SpeechDecorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLVoiceSearch.Strategy
{
    class ParserStrategy : IParseStrategy
    {
        private static Dictionary<string, ISpeechParser> supportedActions = new Dictionary<string, ISpeechParser>
        {
            { "search", new SearchDecorator( new SpeechParser()) }
            /*
             * { "compose", },
            { "filter", },
            {"sort by" }
            */
        };

        public ISpeechParser GetParser(string speechText)
        {
            foreach (string action in supportedActions.Keys)
            {
                if (speechText.StartsWith(action))
                {
                    supportedActions.TryGetValue(action, out ISpeechParser thisParser);
                    return thisParser;
                }
            }

            Console.WriteLine("No specific action prefix used, returning the default search parser");
            return new SearchDecorator(new SpeechParser());
        }
    }
}
