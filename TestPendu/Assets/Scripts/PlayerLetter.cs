using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLetter : MonoBehaviour
{
    [SerializeField] private TMP_InputField LetterField;
    [SerializeField] private TMP_InputField WordField;
    [SerializeField] private List<GameObject> bodyParts;
    [SerializeField] private TMP_Text validText;
    
  
    private int stateIndex;
    private int maxBodyPart;
    private PlayerState state;

    public bool asWin;
    public bool asLose;

    public void Start()
    {
        initializeBodyParts();

    }

    public void GuessLetter()
    {
        if (LetterField.text.Length > 0)
        {
            char guessLetter = char.ToUpper(LetterField.text[0]);
            GameManager.Instance.RevealLetter(guessLetter);
            asWin = GameManager.Instance.findWord;
            if (!GameManager.Instance.findLetter)
            {
                AddBodyParts();
            }
            
        }
    }

    public void UpdateValidText()
    {
        if (LetterField.text.Length > 0 || WordField.text.Length > 0)
        {
            validText.enabled = true;
        }
        else
        {
            validText.enabled = false;
        }
    }

    public void GuessWord()
    {
        if (WordField.text.Length > 0)
        {
            GameManager.Instance.RevealWord(WordField.text);
            asWin = GameManager.Instance.findWord;
        }
    }
    

    private void AddBodyParts()
    {
        if (stateIndex < maxBodyPart)
        {
            stateIndex++;
            state = (PlayerState)stateIndex;
            for (int i = 0; i < stateIndex; i++)
            {
                bodyParts[i].SetActive(true);
            }
        }
        else
        {
            stateIndex++;
            state = (PlayerState)stateIndex;
            for (int i = 0; i < stateIndex; i++)
            {
                bodyParts[i].SetActive(true);
            }

            asLose = true;
        }
        
    }

    private void initializeBodyParts()
    {
        stateIndex = 0;
        state = (PlayerState)stateIndex;
        maxBodyPart = (int)PlayerState.Leg2 ;
        foreach (var part in bodyParts)
        {
            part.SetActive(false);
        }
    }
}
