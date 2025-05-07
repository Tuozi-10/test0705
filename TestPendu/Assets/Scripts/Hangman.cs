using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hangman : MonoBehaviour
{
    public static Hangman Instance;
    
    #region Base Parameters
    
    public string Player1Name { private get; set; } = "DefaultPlayer1Name";
    public string Player2Name { private get; set; } = "DefaultPlayer2Name";
    public string Word { private get; set; } = "DefaultWord";
    
    #endregion
    
    #region Serialized Fields

    [Header("References")]
    [SerializeField] private GuessWordDisplay guessWordDisplay;
    [SerializeField] private TMP_Text player1NameField;
    [SerializeField] private TMP_Text player2NameField;
    [SerializeField] private TMP_InputField letterInputField;
    [SerializeField] private TMP_Text usedLettersField;
    [SerializeField] private Image hangmanDisplay;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TMP_Text gameOverField;
    [SerializeField] private TMP_Text revealField;

    [Header("Parameters")]
    [SerializeField] private int maxHangState;
    [SerializeField] private List<Sprite> hangSprites;
    [SerializeField] private string winText = " wins!";
    [SerializeField] private string wordRevealText = "The word was ";
    
    #endregion
    
    private int hangState;

    private HashSet<char> usedLetters = new();

    private void Awake()
    {
        Instance = this;
        
        letterInputField.onSubmit.AddListener(OnFieldInputSubmit);
    }

    public void Init()
    {
        guessWordDisplay.Init(Word);
        
        player1NameField.text = Player1Name;
        player2NameField.text = Player2Name;
    }
    
    private void GuessLetter(char guess)
    {
        if (!usedLetters.Add(guess)) // If letter has already been guessed, will stop.
        {
            return;
        }

        usedLettersField.text += guess + " ";

        if (Word.Contains(guess))
        {
            AccurateGuess(guess);
        }
        else
        {
            WrongGuess();
        }
    }

    private void AccurateGuess(char guess)
    {
        guessWordDisplay.AddLetterToGuessWord(guess);

        bool win = true;
        
        foreach (char letter in Word)
        {
            if (!usedLetters.Contains(letter))
            {
                win = false;
            }
        }
        
        if (win) GameOver(true);
    }

    private void WrongGuess()
    {
        hangState++;

        hangmanDisplay.sprite = hangSprites[hangState];
        
        if (hangState == maxHangState)
        {
            GameOver(false);
        }
    }

    private void GameOver(bool win)
    {
        gameOver.SetActive(true);
        gameOverField.text = (win ? Player2Name : Player1Name) + winText;
        if (!win) revealField.text = wordRevealText + Word;
        
        PlayerPrefsTools.CycleSaves();
        PlayerPrefsTools.SaveGame(Player1Name, Player2Name, Word, win ? 2 : 1);
    }
    
    #region Input Field
    
    private void OnFieldInputSubmit(string input)
    {
        input = input.ToUpper();
        
        if (IsInputValid(input))
        {
            GuessLetter(input[0]);
        }

        letterInputField.text = "";
        
        letterInputField.Select();
        letterInputField.ActivateInputField();
    }
    
    private static bool IsInputValid(string input) // Assumes given letter is uppercased.
    {
        if (input.Length != 1) return false;

        return 'A' <= input[0] && input[0] <= 'Z';
    }

    #endregion
}