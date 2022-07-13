using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using static StaticHexLog;

public class HexSTT : MonoBehaviour
{

    private DictationRecognizer dictationRecognizer;

    void Awake()
    {
        dictationRecognizer = new DictationRecognizer();
        subscribeToEvents();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startRecognition();
        }
    }

    public void startRecognition()
    {
        Debug.Log("Start");
        if (dictationRecognizer.Status != SpeechSystemStatus.Running) 
        {
            dictationRecognizer.Start();
        }
    }

    public void stopRecognition()
    {
        if (dictationRecognizer != null)
        {
            dictationRecognizer.Stop();
        }
    }

    private void processDictationResult(string text, ConfidenceLevel confidence)
    {
        Info("Dictation result: " + text);
        string[] words = text.Split(' ');
        /*
        if (words[3] == "stan")
        {
            //Application.Quit();
            Debug.Log("!!Solarboy!!: Hi Stan!");
        }
        else if (words[0] == "test")
        {
            //Application.Quit();
            Debug.Log("!!Solarboy!!: Hi Test!");
        }

        foreach (string i in words)
        {
            Debug.Log(i);
        }
        */

        if (words.Contains("stan"))
        {
            Debug.Log("!!Solarboy!!: Hi Stan!");
        }
        else if (words.Contains("hey"))
        {
            Debug.Log("!!Solarboy!!: Yeah?");
        }
    }

    private void subscribeToEvents()
    {
        dictationRecognizer.DictationResult += processDictationResult;
        dictationRecognizer.DictationHypothesis += (text) => {
            Info("Dictation result: " + text);
            Debug.Log("Dictation result: " + text);
        };
        dictationRecognizer.DictationComplete += (completionCause) => {
            if (completionCause != DictationCompletionCause.Complete)
            {
                Info("Dictation failed: "+ completionCause);
                Debug.Log("Dictation failed: " + completionCause);
            }
        };
        dictationRecognizer.DictationError += (error, hresult) => {
            Warn("Dictation error:" + error + " hresult: " + hresult);
            Debug.Log("Dictation error:" + error + " hresult: " + hresult);
        };
    }




    void OnApplicationQuit()
    {
        if (dictationRecognizer != null)
        {
            dictationRecognizer.Dispose();
        }
    }


}
