using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Phase2UI : MonoBehaviour
{
    [Header("Script Links")]
    public GamePhase gamePhaseLink;
    
    [Header("Canvas links")]
    public GameObject phase2Canva;
    public GameObject gamePhase;
    
    [Header("GO links")]
    public GameObject p1NameInputGO;
    public GameObject p1WordInputGO;
    public GameObject p2NameInputGO;
    
    [Header("Game string input")]
    public string p1NameInput;
    public string p1WordInput;
    public string p2NameInput;

    [Header("Data")] 
    public UnallowedLetterScriptable unallowedLetterData;

    private void Start()
    {
        
        p1NameInputGO.SetActive(true);
        p1WordInputGO.SetActive(false);
        p2NameInputGO.SetActive(false);
        
    }

    public void ReadPlayer1Name(string p1Name)
    {
        if (p1Name != null && p1Name.Length < 20)
        {
            p1NameInput = p1Name;
            Debug.Log(p1Name);
        
            p1NameInputGO.SetActive(false);
            p1WordInputGO.SetActive(true);
        }
        else
        {
            Debug.Log("Must enter a name not empty");
        }
    }
    
    public void ReadPlayer1Word(string p1Word)
    {
        if (p1Word != null && p1Word.Length < 20 && CheckLetterInWord(p1Word) == false)
        {
            p1Word = p1Word.ToLower();
            p1WordInput = p1Word;
            Debug.Log("The word chosed is : " + p1Word + " who have : " + p1Word.Length + " in lenght!");
        
            p1WordInputGO.SetActive(false);
            p2NameInputGO.SetActive(true);
        }
        else
        {
            Debug.Log("Word must be something not empty");
        }
    }
    
    public void ReadPlayer2Name(string p2Name)
    {
        if (p2Name != null && p2Name.Length < 20)
        {
            p2NameInput = p2Name;
            Debug.Log(p2Name);
            
            gamePhaseLink.LaunchGame();
            gamePhaseLink.UpdateUI();
        
            p2NameInputGO.SetActive(false);
            phase2Canva.SetActive(false);
        
            gamePhase.SetActive(true);
        }
        else
        {
            Debug.Log("Must enter a name not empty");
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
                    Debug.Log("Not allowed character detected");
                    return true;
                }
            }
        }
        
        return false;
    }


}
