using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameViewController : MonoBehaviour
{
    [SerializeField] private TMP_Text wordToSearchText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text log;
    
    private void Start()
    {
        wordToSearchText.text = "";
        for (int i = 0; i < GameManager.Instance.wordToSearch.Length; i++)
        {
            GameManager.Instance.lettersToGuess.Add(GameManager.Instance.wordToSearch[i].ToString());
        }
    }

    private void FixedUpdate()
    {
        wordToSearchText.text = "";
        //update text according to current guess state
        for (int i = 0; i < GameManager.Instance.lettersToGuess.Count; i++)
        {
            if (GameManager.Instance.lettersToGuess[i] == null) wordToSearchText.text += GameManager.Instance.wordToSearch[i];
            else wordToSearchText.text += "_";
        }
        
        //check for win
        foreach (var element in GameManager.Instance.lettersToGuess)
        {
            if (element != null) break;
        }
        GameManager.Instance.Winner = 1;

        if (GameManager.Instance.Winner != 0)
        {
            //bestSequenceOfTheGame();
        }
    }

    //public async void bestSequenceOfTheGame()
    //{
    //    log.text = $"J{GameManager.Instance.Winner} Won !";
    //    await Task.Delay(5000);
    //    log.text = "3";
    //    await Task.Delay(1000);
    //    log.text = "2";
    //    await Task.Delay(1000);
    //    log.text = "1";
    //    await Task.Delay(1000);
    //    log.text = ";)";
    //    await Task.Delay(1000);
    //    Application.Quit();
    //}
    
    public void Guess(string guess)
    {
        inputField.text = "";
        if (guess == null) guess = inputField.text;
        if (inputField.text.Length > 1) log.text = "can't guess more than one character !";
        else
        {
            int i = 0;
            foreach (var letterToGuess in GameManager.Instance.lettersToGuess)
            {
                i++;
                if (letterToGuess == guess)
                {
                    GameManager.Instance.guessedCharacters.Add(letterToGuess);
                    GameManager.Instance.lettersToGuess[i] = null;
                    log.text = "good guess !";
                    return;
                }
                
                foreach (var alreadyGuessedChar in GameManager.Instance.guessedCharacters)
                {
                    if (letterToGuess == alreadyGuessedChar)
                    {
                        log.text = "already guessed...";
                        return;
                    }
                }
            }
            GameManager.Instance.wrongGuessCount++;
            log.text = "wrong guess :(";
        }
    }
}
