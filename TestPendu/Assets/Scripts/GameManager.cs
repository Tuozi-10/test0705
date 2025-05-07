using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string PlayerOne;
    public string PlayerTwo;
    public string Word;
    public List<LetterSlot> LetterSlots = new ();
    public bool findLetter;
    public bool findWord = false;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Instance = this;
        findLetter = false;
    }
    public void RevealLetter(char guessedLetter)
    {
        findLetter = false;
        foreach (LetterSlot letterSlot in LetterSlots)
        {
            if (letterSlot.Letter == guessedLetter)
            {
                letterSlot.Reveal();
                findLetter = true;
            }
        }

        Win();
    }

    public void RevealWord(string guessedWord)
    {
        findWord = false;
        if (guessedWord.ToUpper() == Word.ToUpper())
        {
            findWord = true;
            foreach (var letter in LetterSlots)
            {
                letter.Reveal();
            }
            Win();
        }
    }

    public void Win()
    {
        int letterNumber = 0;
        int letterNumberFind = 0;
        foreach (var letter in LetterSlots)
        {
            if (!letter.coverImage.enabled)
            {
                letterNumberFind++;
            }

            letterNumber++;
        }

        if (letterNumberFind >= letterNumber)
        {
            findWord = true;
        }
    }
}

