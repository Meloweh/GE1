using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour {
    public TextMeshProUGUI dialogText;
    public Image speakerFace;
    private Queue<string> sentences;
    public bool isTyping = false;
    public static DialogManager instance;
    public GameObject dialogBox;
    private string prevSentence;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of DialogManager found!");
            return;
        }
        instance = this;
        dialogText.richText = true;
    }

    private void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog) {
        if (IsBusy()) return;
        prevSentence = "";
        sentences.Clear();
        dialogBox.SetActive(true);

        foreach (string sentence in dialog.GetSentences()) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        SetSpeakerFace(dialog.GetSpeakersFace());
    }

    private void DisplayNextSentence() {
        if (isTyping) {
            StopAllCoroutines();
            dialogText.text = prevSentence;
            //Debug.Log("A: " + prevSentence);
            isTyping = false;
        } else {
            if (sentences.Count == 0) {
                EndDialog();
                return;
            }
            //Debug.Log("B: " + sentences.Peek());
            string sentence = sentences.Dequeue();
            prevSentence = sentence;
            StartCoroutine(TypeSentence(sentence));
        }
    }


    private IEnumerator TypeSentence(string sentence) {
        isTyping = true;
        dialogText.text = "";

        bool isTag = false;
        string tagContent = "";

        foreach (char letter in sentence.ToCharArray()) {
            if (letter == '<') {
                isTag = true;
                tagContent = "<";
                continue;
            } else if (letter == '>') {
                isTag = false;
                tagContent += '>';
                dialogText.text += tagContent;
                tagContent = "";
                continue;
            }

            if (isTag) {
                tagContent += letter;
                continue;
            }

            dialogText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        isTyping = false;
    }

    public bool IsBusy() {
        return dialogBox.activeSelf;
    }

    private void SetSpeakerFace(Sprite sprite) {
        speakerFace.sprite = sprite;
    }

    private void EndDialog() {
        dialogBox.SetActive(false);
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            DisplayNextSentence();
        }
    }

}