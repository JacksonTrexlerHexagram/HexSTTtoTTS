using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

//THIS SCRIPT REQUIRES THE UNITY PACKAGE DOWNLOADABLE FROM HERE
//https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/quickstarts/setup-platform?tabs=windows%2Cubuntu%2Cunity%2Cjre%2Cmaven%2Cnodejs%2Cmac%2Cpypi&pivots=programming-language-csharp

namespace Hexagram.TextToSpeech
{
    public class HexTTS : MonoBehaviour
    {
        static string YourSubscriptionKey = "e47077c55e9541d383c71ac1a6a9a154";
        static string YourServiceRegion = "eastus";

        static void OutputSpeechSynthesisResult(SpeechSynthesisResult speechSynthesisResult, string text)
        {
            switch (speechSynthesisResult.Reason)
            {
                case ResultReason.SynthesizingAudioCompleted:
                    Debug.Log($"Speech synthesized for text: [{text}]");
                    break;
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                    Debug.Log($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Debug.Log($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Debug.Log($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        Debug.Log($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    break;
                default:
                    break;
            }
        }

        //async static Task Main(string[] args)
        async void Awake()
        {
            Debug.Log("Hello");
            var speechConfig = SpeechConfig.FromSubscription(YourSubscriptionKey, YourServiceRegion);

            // The language of the voice that speaks.
            speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural";

            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {
                // Get text from the console and synthesize to the default speaker.
                string text = "Greetings!";



                int hour = DateTime.Now.Hour;
                if (hour >= 6 && hour <= 11)
                {
                    text = "Good Morning!";
                }
                else if (hour >= 12 && hour <= 17)
                {
                    text = "Good Afternoon!";
                }
                else
                {
                    text = "Good Evening!";
                }
                var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
                OutputSpeechSynthesisResult(speechSynthesisResult, text);
            }
        }

        public static async void HexSpeak(string myInput)
        {
            Debug.Log("Speaking");
            var speechConfig = SpeechConfig.FromSubscription(YourSubscriptionKey, YourServiceRegion);

            // The language of the voice that speaks.
            speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural";

            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {
                // Get text from the console and synthesize to the default speaker.

                var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(myInput);
                OutputSpeechSynthesisResult(speechSynthesisResult, myInput);
            }
        }
    }
}