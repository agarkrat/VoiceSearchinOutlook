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

        private bool isStillSearching = true;

        public VoiceSearch()
        {
            InitializeComponent();
        }

        public static async Task<string> RecognizeSpeechAsync()
        {
            string text = string.Empty;
            using (var recognizer2 = new SpeechRecognizer(speechConfig))
            {
                var result = await recognizer2.RecognizeOnceAsync();

                // Checks result.
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    text = result.Text;
                }
            }

            return text;
        }

        public static async Task<string> speakItOut(string text)
        {
            if ("Searching for ".Equals(text))
            {
                return "";
            }
            // Creates a speech synthesizer using the default speaker as audio output.
            using (var synthesizer = new SpeechSynthesizer(speechConfig))
            {
                // Receive a text from console input and synthesize it to speaker.
                //Console.WriteLine("Type some text that you want to speak...");
                //Console.Write("> ");
                //string text = Console.ReadLine();

                using (var result = await synthesizer.SpeakTextAsync(text))
                {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }
                    }
                }
            }

            return "";
        }

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //Copy changes done for picture box here
            if (checkBox1.Checked)
            {
                // This is for the case when you want to start recording

                label1.Text = "Click to Pause or Stop";
                checkBox1.ImageIndex = 0;
                checkBox1.BackgroundImage = null;
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

                        if (isStillSearching)
                        {
                            recognizer.StopContinuousRecognitionAsync().GetAwaiter().GetResult();
                        }

                        ISpeechParser speechParser = new ParserStrategy().GetParser(resultText);
                        resultText = speechParser.ParseSpeechText(resultText);
                        speechParser.PerformAction(resultText);
                        //Task.Delay(3000).Wait();


                        ////speakItOut("Searching for " + eg.Result.Text);
                        //Task.Delay(3000).Wait();

                        if (isStillSearching)
                        {
                            recognizer.StartContinuousRecognitionAsync().GetAwaiter().GetResult();
                        }
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
            else
            {
                label1.Text = "Tap the microphone to start";
                checkBox1.ImageIndex = 1;
                isStillSearching = false;
                await recognizer.StopContinuousRecognitionAsync();
            }
        }
    }
}
