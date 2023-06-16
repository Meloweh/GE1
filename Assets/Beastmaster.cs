using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Beastmaster : EntityLiving
{
    public Transform[] points;
    public float speed = 10f;

    private int currentPoint = 0;
    private Vector2 moveDirection;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    private int index;

    public GameObject continueButton;
    public float wordSpeed;
    public bool playerIsClose;

    private Coroutine movingCoroutine;

    public Button dialogueOption1;
    public Button dialogueOption2;
    public List<string> dialogueOption1Response;
    public List<string> dialogueOption2Response;

    private int optionResponseIndex = 0;
    private List<string> currentOptionResponse;

    public override void Start() {
        base.Start();
        movingCoroutine = StartCoroutine(MoveToNextPoint());

        dialogueOption1.GetComponentInChildren<TextMeshProUGUI>().text = "Ask about the Vampire City";
        dialogueOption2.GetComponentInChildren<TextMeshProUGUI>().text = "Ask about the powerful Lich";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                StopMoving();
                dialoguePanel.SetActive(true);
                if (points[currentPoint].GetComponent<DialogueWP>().dialogues.Count > 0)
                    StartCoroutine(Typing());
            }
        }

        if (points[currentPoint].GetComponent<DialogueWP>().dialogues.Count > 0)
        {
            if (dialogueText.text == points[currentPoint].GetComponent<DialogueWP>().dialogues[index])
            {
                continueButton.SetActive(true);
            }
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        if (movingCoroutine == null)
        {
            movingCoroutine = StartCoroutine(MoveToNextPoint());
        }
    }


    IEnumerator MoveToNextPoint()
    {
        while (true)  // Loop forever
        {
            if (Vector2.Distance(transform.position, points[currentPoint].position) > 0.1f)
            {
                // Calculate the direction to the current point
                moveDirection = (points[currentPoint].position - transform.position).normalized;
                SetDirection(moveDirection);
                // Move towards the current point
                Vector2 newPosition = Vector2.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);
                GetRigid().MovePosition(newPosition);
                GetAnimator().SetBool("isWalking", true);  // Start the walking animation
                yield return null;  // Wait for the next frame
            }
            else
            {
                // Stop the walking animation
                GetAnimator().SetBool("isWalking", false);
                // Wait at the current point
                yield return new WaitForSeconds(points[currentPoint].GetComponent<DialogueWP>().waitTime);
                // Go to the next point
                currentPoint = (currentPoint + 1) % points.Length;
            }
        }
    }

    private void StopMoving()
    {
        if (movingCoroutine != null)
        {
            StopCoroutine(movingCoroutine);
            movingCoroutine = null;
        }
    }

    public override void SetDirection(Vector2 dir) {
        this.moveDirection = dir;
        GetAnimator().SetFloat("X", dir.x);
        GetAnimator().SetFloat("Y", dir.y);
    }

    IEnumerator Typing()
    {
        foreach (char letter in points[currentPoint].GetComponent<DialogueWP>().dialogues[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        continueButton.SetActive(false);
        dialogueText.text = "";

        // Check if we are currently in a dialogue option response
        if (currentOptionResponse != null)
        {
            // If there are more lines in the dialogue option response
            if (optionResponseIndex < currentOptionResponse.Count)
            {
                StartCoroutine(TypingOptionResponse(currentOptionResponse));
            }
            else
            {
                zeroText();
                currentOptionResponse = null;
                optionResponseIndex = 0;
            }
        }
        else
        {
            if (index < points[currentPoint].GetComponent<DialogueWP>().dialogues.Count - 1)
            {
                index++;
                StartCoroutine(Typing());
            }
            else
            {
                zeroText();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }

    public void OnDialogueOption1Selected()
    {
        currentOptionResponse = dialogueOption1Response;
        optionResponseIndex = 0;
        dialogueText.text = "";
        StartCoroutine(TypingOptionResponse(currentOptionResponse));
    }

    public void OnDialogueOption2Selected()
    {
        currentOptionResponse = dialogueOption2Response;
        optionResponseIndex = 0;
        dialogueText.text = "";
        StartCoroutine(TypingOptionResponse(currentOptionResponse));
    }

    IEnumerator TypingOptionResponse(List<string> dialogueOptionResponse)
    {
        foreach (char letter in dialogueOptionResponse[optionResponseIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        // increment the option response index
        optionResponseIndex++;
        // show continue button if there are more lines in the dialogue option response
        continueButton.SetActive(optionResponseIndex < dialogueOptionResponse.Count);
    }
}
