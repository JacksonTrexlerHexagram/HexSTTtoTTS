using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class HexSTT : MonoBehaviour
{

    private DictationRecognizer dictationRecognizer;

    // Start is called before the first frame update
    void Awake()
    {
        dictationRecognizer = new DictationRecognizer();
        subscribeToEvents();
    }

    public void startRecognition()
    {
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

    private void processDictationResult(string text,
      ConfidenceLevel confidence)
    {
        Debug.LogFormat("Dictation result: {0}", text);
        string[] words = text.Split(' ');
        if (words[0] == "Hexagram")
        {
            OnApplicationQuit().Quit();
        }
    }

    private void subscribeToEvents()
    {
        dictationRecognizer.DictationResult += processDictationResult;
        dictationRecognizer.DictationHypothesis += (text) => {
            Debug.LogFormat("Dictation hypothesis: {0}", text);
        };
        dictationRecognizer.DictationComplete += (completionCause) => {
            if (completionCause != DictationCompletionCause.Complete)
            {
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
            }
        };
        dictationRecognizer.DictationError += (error, hresult) => {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
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
