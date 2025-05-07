using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PenduManager : MonoBehaviour
{
    private string Word;
    private List<char> DisplayedWord = new List<char>();
    [SerializeField] private TMP_Text textToDisplay;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject inputFieldObj;
    [SerializeField] private Image[] imageList;
    [SerializeField] private GameManager GM;
    [SerializeField] private GameObject EndOfGameButton;
    private int MissedTrys = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            String upperText = inputField.text.ToUpper();
            if (inputField.text.Length == 1)
            {
                if (!TryLetter(upperText[0]))
                {
                    MissedTry();
                }
            }
            else if (inputField.text.Length > 1)
            {
                if (!TryWord(upperText))
                {
                    MissedTry();
                }
            }

            inputField.text = null;
        }
    }

    public void StartGame()
    {
        EndOfGameButton.SetActive(false);
        inputFieldObj.SetActive(true);
        Word = GM.WordToGuess;
        for (int i = 0; i < Word.Length; i++)
        {
            DisplayedWord.Add('_');
        }
        UpdateDisplayedWord();
    }

    bool TryLetter(char Letter)
    {
        bool result = false;
        string upperWord = Word.ToUpper();
        for (int i = 0; i < Word.Length; i++)
        {
            if (upperWord[i] == Letter)
            {
                DisplayedWord[i] = Word[i];
                result = true;
            }
        }
        UpdateDisplayedWord();
        return result;
    }

    bool TryWord(string wordTried)
    {
        string upperWord = Word.ToUpper();
        if (wordTried == upperWord)
        {
            textToDisplay.text = Word;
            Win();
            return true;
        }
        return false;
    }

    void UpdateDisplayedWord()
    {
        string text = null;
        foreach (var VARIABLE in DisplayedWord)
        {
            text += VARIABLE;
        }

        textToDisplay.text = text;
        if (textToDisplay.text == Word)
        {
            Win();
        }
    }

    void MissedTry()
    {
        imageList[MissedTrys].enabled = true;
        MissedTrys += 1;
        if (MissedTrys >= imageList.Length)
        {
            Lose();
        }
    }

    void Win()
    {
        resultText.enabled = true;
        resultText.text = GM.J2Name + ", vous avez gagné";
        GM.WinOrLoss = "Won";
        EndGame();
    }

    void Lose()
    {
        resultText.enabled = true;
        resultText.text = GM.J2Name + ", vous avez perdu";
        GM.WinOrLoss = "Lost";
        EndGame();
    }

    void EndGame()
    {
        inputFieldObj.SetActive(false);
        foreach (var img in imageList)
        {
            img.enabled = false;
        }
        EndOfGameButton.SetActive(true);

        MissedTrys = 0;
    }

    public void BackToMenu()
    {
        GM.MenuObj.SetActive(true);
        GM.SaveGame();
        resultText.enabled = false;
        GM.PenduGameObj.SetActive(false);
    }
}
