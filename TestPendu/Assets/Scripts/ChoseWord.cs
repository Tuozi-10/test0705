using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChoseWord : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Transform validateButton;
    [SerializeField] private TypewriterByCharacter typeWriter;
    [SerializeField] private TMP_Text playerChoseTxt;
    [SerializeField] private TMP_Text placeHolder;
    [SerializeField] private bool choseWord;
    private int playerTurn;
    
    private void Start()
    {
        validateButton.gameObject.SetActive(false);
        playerTurn = 1;
        SwitchPlayerChoseName();
        choseWord = false;
        typeWriter.StartShowingText();
    }

    public void UpdateDisplay()
    {
        if (inputField.text.Length >= 1)
        {
            validateButton.gameObject.SetActive(true);
        }
        else
        {
            validateButton.gameObject.SetActive(false);
        }
    }
    
    public void Valid()
    {
        if (choseWord)
        {
            GameManager.Instance.Word = inputField.text;
            SceneManager.LoadScene("PenduGame");
            return;
        }
        if (playerTurn == 1)
        {
            GameManager.Instance.PlayerOne = inputField.text;
            return;
        }
        if (playerTurn > 1 )
        {
            GameManager.Instance.PlayerTwo = inputField.text;
            
        }
        

        
    }

    public void EndValid()
    {
        playerTurn++;
        SwitchPlayerChoseName();
        if (playerTurn >= 3)
        {
            SwitchToChoseWord();
        }
    }


    private void SwitchPlayerChoseName()
    {
        playerChoseTxt.text = $"Joueur {playerTurn} choisis ton nom";
        inputField.text = "";
        placeHolder.text = "Joueur";
        typeWriter.StartShowingText();
    }

    private void SwitchToChoseWord()
    {
        playerChoseTxt.text = $"{GameManager.Instance.PlayerOne} choisis le mot";
        inputField.text = "";
        placeHolder.text = "Pendu";
        choseWord = true;
        typeWriter.StartShowingText();
    }
    
}
