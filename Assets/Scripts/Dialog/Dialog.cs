using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog {
    private Sprite speakerFace;
    private List<string> sentences;

    public Dialog(Sprite speakerFace) {
        this.speakerFace = speakerFace;
        sentences = new List<string>();
    }

    public void Add(string str) {
        sentences.Add(str);
    }

    public Sprite GetSpeakersFace() {
        return speakerFace;
    }

    public List<string> GetSentences() {
        return sentences;
    }
}