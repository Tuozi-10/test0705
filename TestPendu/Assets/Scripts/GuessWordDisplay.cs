using TMPro;
using UnityEngine;

public class GuessWordDisplay : MonoBehaviour
{
    #region Serialized Fields
    
    [SerializeField] private TMP_Text wordGuess;
    
    #endregion

    private string word;
    private char[] guessArray;

    public void Init(string word)
    {
        this.word = word;
        InitGuessArray();
        UpdateGuessWord();
    }

    public void AddLetterToGuessWord(char guess)
    {
        for (var l = 0; l < word.Length; l++)
        {
            if (word[l] == guess)
            {
                guessArray[l] = guess;
            }
        }

        UpdateGuessWord();
    }
    
    #region Tools

    private void InitGuessArray()
    {
        guessArray = new char[word.Length];
        
        for (var i = 0; i < guessArray.Length; i++)
        {
            guessArray[i] = '_';
        }
    }

    private void UpdateGuessWord()
    {
        wordGuess.text = guessArray.ArrayToString();
    }
    
    #endregion
}