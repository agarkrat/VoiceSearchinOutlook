using FHLVoiceSearch.Strategy;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FHLVoiceSearch
{
    public partial class VoiceSearch : Form
    {
        private static SpeechConfig speechConfig = SpeechConfig.FromSubscription("5e2fd87cefd448ab8e5d3d6d31b25d87", "centralindia");

        private static AudioConfig audioConfig = AudioConfig.FromDefaultMicrophoneInput();

        private static SpeechRecognizer recognizer = new SpeechRecognizer(speechConfig, audioConfig);

        public VoiceSearch()
        {
            InitializeComponent();
        }

        private async void pictureBox1_ClickAsync(object sender, EventArgs e)
        {
            speechConfig.EnableDictation();

            //var result = await recognizer.RecognizeOnceAsync();
            //MessageBox.Show($"Here's what you said : {result.Text}");

            await recognizer.StartContinuousRecognitionAsync();
            var stopRecognition = new TaskCompletionSource<int>();
            recognizer.Recognized += (s, eg) =>
            {
                if (eg.Result.Reason == ResultReason.RecognizedSpeech)
                {
                    stopRecognition.TrySetResult(0);
                    //MessageBox.Show($"RECOGNIZED: Text={eg.Result.Text}");
                    string resultText = eg.Result.Text;
                    ISpeechParser speechParser = new ParserStrategy().GetParser(resultText);
                    resultText = speechParser.ParseSpeechText(resultText);
                    speechParser.PerformAction(resultText);

                }
                else if (eg.Result.Reason == ResultReason.NoMatch)
                {
                    //MessageBox.Show($"NOMATCH: Speech could not be recognized.");
                }
            };

            recognizer.Canceled += (s, eg) =>
            {
                //Console.WriteLine($"CANCELED: Reason={eg.Reason}");

                if (eg.Reason == CancellationReason.Error)
                {
                    //MessageBox.Show($"CANCELED: ErrorCode={eg.ErrorCode}");
                    //MessageBox.Show($"CANCELED: ErrorDetails={eg.ErrorDetails}");
                    //MessageBox.Show($"CANCELED: Did you update the subscription info?");
                }

                stopRecognition.TrySetResult(0);
            };

            recognizer.SessionStopped += (s, eg) =>
            {
                //Console.WriteLine("\n    Session stopped event.");
                stopRecognition.TrySetResult(0);
            };

            Task.WaitAny(new[] { stopRecognition.Task });
        }

        private async void stopButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("Stopping search");
            await recognizer.StopContinuousRecognitionAsync();
        }
    }
}
