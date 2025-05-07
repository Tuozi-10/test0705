using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePhase : MonoBehaviour
{
    [Header("Script Links")]
    public Phase2UI phase2UILink;

    [Header("Text links")] 
    public TMP_Text p1TextName;
    public TMP_Text p2TextName;
    public TMP_Text wordText;
    
    [Header("Data")] 
    public UnallowedLetterScriptable unallowedLetterData;
    
    [Header("Canvas links")]
    public GameObject gamePhaseCanva;
    public GameObject p1VictoryMenu;
    public GameObject p2VictoryMenu;

    [Header("Dessin du pendue GO")] 
    public List<GameObject> imagePart;
    
    private string _actualWord;
    private string _actualWordTempo;
    private char[] _actualWordHidden;

    private int countDown = 10;

    private void Start()
    {
        p1VictoryMenu.SetActive(false);
        p2VictoryMenu.SetActive(false);
    }

    public void LaunchGame()
    {
        _actualWord = phase2UILink.p1WordInput;
        _actualWordHidden = new char[_actualWord.Length];
        
        for (int i = 0; i < _actualWord.Length; i++)
        {
            _actualWordHidden[i] = '-';
        }

        _actualWordTempo = new string(_actualWordHidden);
    }
    
    public void TryLetters(string wordTry)
    {
        
        
        if (wordTry != null)
        {
            if(CheckLetterInWord(wordTry) == true)
                return;
            
            wordTry = wordTry.ToLower();
            
            if (wordTry.Length == 1)
            {
                char letterTry = wordTry[0];
                
                
                for (int i = 0; i < _actualWordHidden.Length; i++)
                {
                    if (letterTry == _actualWord[i])
                    {
                        _actualWordHidden[i] = _actualWord[i];
                    }
                }

                UpdateUI();
                CheckWin();
            }
            else if (wordTry == _actualWord)
            {
                CheckWin();
            }
        }
        else
        {
            Debug.Log("null try");
        }
    }

    private bool CheckLetterInWord(string word)
    {
        for (int i = 0; i < word.Length; i++)
        {
            for (int j = 0; j < unallowedLetterData.unallowedLetters.Count; j++)
            {
                if (unallowedLetterData.unallowedLetters.Contains(word[i]))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void UpdateUI()
    {
        _actualWordTempo = new string(_actualWordHidden);
        
        p1TextName.text = phase2UILink.p1NameInput;
        p2TextName.text = phase2UILink.p2NameInput;
        wordText.text = _actualWordTempo;
        
    }

    private void CheckWin()
    {
        string winTry = new string(_actualWordHidden);
        
        if (winTry == _actualWord && countDown > 0)
        {
            StartCoroutine(P2VictoryScreen());
        }
        else
        {
            countDown--;
        }

        if (countDown == 0)
        {
            imagePart[9].SetActive(true);
            StartCoroutine(P1VictoryScreen());
        }
        
    }

    private void Update()
    {
        if (countDown == 10)
        {
            imagePart[0].SetActive(false);
            imagePart[1].SetActive(false);
            imagePart[2].SetActive(false);
            imagePart[3].SetActive(false);
            imagePart[4].SetActive(false);
            imagePart[5].SetActive(false);
            imagePart[6].SetActive(false);
            imagePart[7].SetActive(false);
            imagePart[8].SetActive(false);
            imagePart[9].SetActive(false);
        }
        else if (countDown == 9)
        {
            imagePart[0].SetActive(true);
        }
        else if (countDown == 8)
        {
            imagePart[1].SetActive(true);
        }
        else if (countDown == 7)
        {
            imagePart[2].SetActive(true);
        }
        else if (countDown == 6)
        {
            imagePart[3].SetActive(true);
        }
        else if (countDown == 5)
        {
            imagePart[4].SetActive(true);
        }
        else if (countDown == 4)
        {
            imagePart[5].SetActive(true);
        }
        else if (countDown == 3)
        {
            imagePart[6].SetActive(true);
        }
        else if (countDown == 2)
        {
            imagePart[7].SetActive(true);
        }
        else if (countDown == 1)
        {
            imagePart[8].SetActive(true);
        }
        
    }

    IEnumerator P1VictoryScreen()
    {

        yield return new WaitForSeconds(2f);

        p1VictoryMenu.SetActive(true);
        gamePhaseCanva.SetActive(false);
        
        
    }
    
    IEnumerator P2VictoryScreen()
    {

        yield return new WaitForSeconds(2f);

        p2VictoryMenu.SetActive(true);
        gamePhaseCanva.SetActive(false);
        
        
    }
}
