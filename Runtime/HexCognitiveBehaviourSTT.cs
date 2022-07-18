using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Hexagram.TextToSpeech;

namespace Hexagram.SpeechToText
{
    public class HexCognitiveBehaviourSTT : MonoBehaviour
    {
        static string YourSubscriptionKey = "e47077c55e9541d383c71ac1a6a9a154";
        static string YourServiceRegion = "eastus";
        bool voiceHeard = false;




        static string OutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
        {
            switch (speechRecognitionResult.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    Debug.Log($"RECOGNIZED: Text={speechRecognitionResult.Text}");
                    return speechRecognitionResult.Text;
                    break;
                case ResultReason.NoMatch:
                    Debug.Log($"NOMATCH: Speech could not be recognized.");
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                    Debug.Log($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Debug.Log($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Debug.Log($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Debug.Log($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    break;
            }
            return "hmm";
        }



        // Start is called before the first frame update
        async void Awake()
        {
        var speechConfig = SpeechConfig.FromSubscription(YourSubscriptionKey, YourServiceRegion);
        speechConfig.SpeechRecognitionLanguage = "en-US";

        using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

        while(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Speak into your microphone.");
            var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
            //string inText = speechRecognitionResult.ToString();
            string inText = speechRecognitionResult.Text;
            string sendText;
            Debug.Log(inText);
            if (inText.Contains("Stan"))
            {
                Debug.Log("!TTS!Solarboy!/TTS!: Hi Stan!");
                sendText = "Hi Stan";
                HexTTS.HexSpeak(sendText);
            }
            else if (inText.Contains("Hey"))
            {
                Debug.Log("!TTS!Solarboy!!: Yeah?");
                sendText = "What's up?";
                HexTTS.HexSpeak(sendText);
            }
            else if (inText.Contains("How") && inText.Contains("are"))
            {
                Debug.Log("!TTS!Solarboy!/TTS!: Living the dream!");
                sendText = "Living the dream!";
                HexTTS.HexSpeak(sendText);
            }
            //HexTTS.HexSpeak(OutputSpeechRecognitionResult(speechRecognitionResult));
        }
            
        }
    }
}
