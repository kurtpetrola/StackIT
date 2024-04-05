using UnityEngine;
using TMPro;
using LMNT;
using System.Collections;

public class Dialogues : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [TextArea(3, 15)]
    public string[] lines;
    public float textSpeed;

    public LMNT.LMNTSpeech lmntSpeech;

    private int index;
    private bool lineDisplayed = true; // This should be a private field.

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse click detected!");

            if (!lineDisplayed)
            {
                DisplayFullLine(); // Call a method to immediately display the full line.
            }
            else
            {
                StartCoroutine(SpeakLine());
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        lineDisplayed = false; // Set to false to start typing the line.
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        lineDisplayed = true; // Set to true when the line is fully displayed.
    }

    void DisplayFullLine()
    {
        StopAllCoroutines(); // Stop the typing coroutine.
        textComponent.text = lines[index]; // Display the full line immediately.
        lineDisplayed = true; // Now the full line is displayed.
    }

    void nextLine()
    {
        index++;
        if (index < lines.Length)
        {
            textComponent.text = string.Empty;
            lineDisplayed = false; // Set to false to start typing the next line.
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    IEnumerator SpeakLine()
    {
        DisplayFullLine(); // Ensure the full line is displayed before speaking.

        lmntSpeech.dialogue = lines[index];
        yield return StartCoroutine(lmntSpeech.Talk());

        nextLine();
    }
}