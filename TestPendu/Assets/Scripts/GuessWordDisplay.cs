using TMPro;
using UnityEngine;

public class GuessWordDisplay : MonoBehaviour
{
    #region Serialized Fields
    
    [SerializeField] private TMP_Text wordGuess;
    
    #endregion

    // attention au double naming ici et dans l'init, ca rend confusant l'utilisation
    // de word dans la fonction Init, surtout si la fonction devient longue apres
    private string word;
    private char[] guessArray;

    public void Init(string word)
    {
        this.word = word;
        InitGuessArray();
        UpdateGuessWord();
    }

    // Très bonne feature !
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