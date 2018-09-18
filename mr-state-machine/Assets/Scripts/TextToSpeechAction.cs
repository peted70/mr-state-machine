using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;

[RequireComponent(typeof(TextToSpeech))]
public class TextToSpeechAction : StateAction
{
    TextToSpeech _tts;
    public string Text;

    // Use this for initialization
    void Start()
    {
    }

    public override bool IsCompleted()
    {
#if UNITY_EDITOR
        return true;
#else
        return _tts.IsSpeaking();
#endif
    }

    public override void Init(StateBase parent)
    {
        _tts = GetComponent<TextToSpeech>();
        _tts.StartSpeaking(Text);
    }

    public override void Reset()
    {
    }

    public override IEnumerable<StateAction> Children()
    {
        return null;
    }
}
