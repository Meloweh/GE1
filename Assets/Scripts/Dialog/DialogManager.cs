using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public Image speakerFace;
    private Queue<string> sentences;
    public bool isTyping = false;
    public static DialogManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DialogManager found!");
            return;
        }
        instance = this;
        dialogText.richText = true;
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        SetSpeakerFace(dialog.speakerFace);
    }

    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogText.text = sentences.Peek();  // Display full sentence
            isTyping = false;
        }
        else
        {
            if (sentences.Count == 0)
            {
                EndDialog();
                return;
            }

            string sentence = sentences.Dequeue();
            StartCoroutine(TypeSentence(sentence));
        }
    }


    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogText.text = "";

        bool isTag = false;
        string tagContent = "";

        foreach (char letter in sentence.ToCharArray())
        {
            if (letter == '<')
            {
                isTag = true;
                tagContent = "<";
                continue;
            }
            else if (letter == '>')
            {
                isTag = false;
                tagContent += '>';
                dialogText.text += tagContent;
                tagContent = "";
                continue;
            }

            if (isTag)
            {
                tagContent += letter;
                continue;
            }

            dialogText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        isTyping = false;
    }




    public void SetSpeakerFace(Sprite sprite)
    {
        speakerFace.sprite = sprite;
    }

    void EndDialog()
    {
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }

}