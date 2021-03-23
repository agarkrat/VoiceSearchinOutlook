using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLVoiceSearch.SpeechDecorator
{
    class SearchDecorator : ISpeechParser
    {
        private static List<string> searchActionPrefix = new List<string> { "from", "to", "subject" };
        private static Dictionary<string, Char> punctuations = new Dictionary<string, char>
        {
            { "colon", ':'},
            { "comma", ','}
        };

        private ISpeechParser speechParser;

        public SearchDecorator (ISpeechParser speechParser)
        {
            this.speechParser = speechParser;
        }

        public string ParseSpeechText(string input)
        {
            Console.WriteLine("Search string was: " + input);
            input = input.Trim();
            if (input.StartsWith("Search"))
            {
                input=input.Substring(7);
            }

            foreach (string punctuationStr in punctuations.Keys)
            {
                int index = input.IndexOf(punctuationStr);
                if (-1 != index)
                {
                    punctuations.TryGetValue(punctuationStr, out char punctuation);
                    input = $"{input.Substring(0, index)}{punctuation}{input.Substring(index + punctuationStr.Length)}";
                }
            }
            Console.WriteLine("After punctuation replacement, Search string is: " + input);

            foreach(string action in searchActionPrefix)
            {
                if(input.StartsWith(action))
                {
                    input = $"{action}:{input.Substring(action.Length)}";
                }
            }
            Console.WriteLine("After action replacement, Search string is: " + input);

            return this.speechParser.ParseSpeechText(input);
        }
    }
}
