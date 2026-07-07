using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SearchService;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    [Header("Dialogue Settings")]
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private GameObject dialoguePanel;
    private Story story;
    [SerializeField] private GameObject[] diaogueOptions;
    private TextMeshProUGUI[] optionTexts;

    [Header("Auto Play Settings")]
    private bool isAutoPlay;
    private Coroutine autoPlayCoroutine;
    [SerializeField] private float autoPlayDelay = 2f;

    [Header("Typewriter Settings")]
    private Coroutine typeCoroutine;
    private string currentLine;
    [SerializeField] private float typeSpeed = 0.05f;
    private bool isTyping = false;


    [Header("Dialogue Speaker")]
    private const string SPEAKER_TAG = "speaker";
    private const string LAYOUT_TAG = "layout";
    private const string VISUAL_TAG = "visual";
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private Image visualSpeaker;
    [SerializeField] private List<CharacterPortrait> characterPortraits = new List<CharacterPortrait>();
    private Animator layoutAnimator;
    public bool isDialogueActive { get; private set;  }

    public bool choiceVisible { get; private set; }


    private void Start()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);

        //get the animator component from the dialogue panel
        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        optionTexts = new TextMeshProUGUI[diaogueOptions.Length];
        int optionIndex = 0;
        foreach (GameObject option in diaogueOptions)
        {
            optionTexts[optionIndex] = option.GetComponentInChildren<TextMeshProUGUI>();
            optionIndex++;
        }
    }

    public void StartDialogue(TextAsset inkFile, bool autoPlay = false)
    {
        if (inkFile != null)
        {
            story = new Story(inkFile.text);
            isDialogueActive = true;
            dialoguePanel.SetActive(true);
            isAutoPlay = autoPlay;

            ContinueDialogue();
            if (isAutoPlay)
                //StartAutoPlay();
                StartCoroutine(BeginAutoDelayed());

        }
    }

    //Fixes bug that causes first line of dialogue to be skipped faster than the autoPlayDelay when autoPlay is enabled
    private IEnumerator BeginAutoDelayed()
    {
        yield return new WaitUntil(() => !isTyping);

        StartAutoPlay();
    }

    private void ContinueDialogue()
    {
        if (story != null && story.canContinue)
            {
                //  displayText.text = story.Continue(); // type text instantly
                
                // type text with typewriter effect
                StartTyping(story.Continue());

                // check if there are choices to be made
                ShowOptions();

                HandleTags(story.currentTags);

        }
        else
            {
                // if the story has ended, end the dialogue
                EndDialogue();
            } 

    }

    private void HandleTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogWarning("Tag could not be parsed: " + tag);
                continue;
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    speakerName.text = tagValue;
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                case VISUAL_TAG:
                   SetPortrait(tagValue);
                    break;
                default:
                    Debug.LogWarning("Unknown tag: " + tagKey);
                    break;
            }
        }
    }

    private void SetPortrait(string characterName)
    {
        //fail safe check to make sure the character name is not null or empty
        if (string.IsNullOrEmpty(characterName))
        {
            Debug.LogWarning("Character name is null or empty.");
            return;
        }

        foreach (CharacterPortrait portrait in characterPortraits)
        {
            if (portrait.characterName == characterName)
            {
                visualSpeaker.sprite = portrait.characterSprite;
                return;
            }
        }

        Debug.LogWarning("Character portrait not found for character: " + characterName);
    }


    private void StartTyping(string line)
    {
        if (typeCoroutine != null)
            StopCoroutine(typeCoroutine);
        typeCoroutine = StartCoroutine(TypeText(line));
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        currentLine = line;
        displayText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            displayText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }

    private void StartAutoPlay()
    {
        if (autoPlayCoroutine != null)
            StopCoroutine(autoPlayCoroutine);

        autoPlayCoroutine = StartCoroutine(AutoPlayCoroutine());
    }

    private IEnumerator AutoPlayCoroutine()
    {
        while (isAutoPlay)
        {
            if(story == null)
                yield break;

            yield return new WaitUntil(() => !isTyping); // wait until typing is done

            if (!story.canContinue && story.currentChoices.Count == 0)
            {
                yield return new WaitForSeconds(autoPlayDelay); // wait for a moment before ending dialogue
                EndDialogue();
                yield break;
            }
            if(story.currentChoices.Count > 0)
                yield break; // stop auto play if there are choices to be made
            yield return new WaitForSeconds(autoPlayDelay);

            ContinueDialogue();
        }
    }
    public void OnContinue()
    {
        if (!isDialogueActive)
            return;

        if(choiceVisible)
            return;

        if(isTyping && !isAutoPlay)
        {
            InstantlyCompleteTyping();
            return;
        }

        if(isAutoPlay)
            return;


        ContinueDialogue();
    }


    private void InstantlyCompleteTyping()
    {
        if (typeCoroutine != null)
            StopCoroutine(typeCoroutine);
        displayText.text = currentLine;
        isTyping = false;
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        displayText.text = "";

        // enable player movement after dialogue ends
        PauseManager.instance.SetPause();
    }

    private void ShowOptions()
    {
        choiceVisible = story.currentChoices.Count > 0;

        List<Choice> currentChoices = story.currentChoices;

        // check if the number of choices is greater than the number of options we have
        if (currentChoices.Count > diaogueOptions.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            diaogueOptions[index].SetActive(true);
            optionTexts[index].text = choice.text;
            index++;
        }

        // hide all unused options
        for (int i = index; i < diaogueOptions.Length; i++)
        {
            diaogueOptions[i].SetActive(false);
        }
    }

    public void ChooseOption(int optionIndex)
    {
        choiceVisible = false;  

        story.ChooseChoiceIndex(optionIndex);

        ContinueDialogue();

        if(isAutoPlay)
            StartAutoPlay();
    }


}
