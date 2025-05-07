using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text player1Text;
    [SerializeField] private TMP_Text player2Text;
    [SerializeField] private TMP_Text hiddenWordText;
    [SerializeField] private TMP_InputField letterInput;
    [SerializeField] private TMP_Text turnIndicator;
    [SerializeField] private GameObject RestartButton;

    private string secretWord;
    private string currentWordDisplay;
    private string wordGiver;
    private string winner;
    private string loser;
    private int incorrectGuesses;
    private int maxIncorrectGuesses = 6;

    void Start()
    {
        string player1 = PlayerPrefs.GetString("Player1Name");
        string player2 = PlayerPrefs.GetString("Player2Name");
        wordGiver = PlayerPrefs.GetString("WordGiver"); 
        string guesser = PlayerPrefs.GetString("Guesser");

        string wordGiverName = PlayerPrefs.GetString(wordGiver + "Name");
        string guesserName = PlayerPrefs.GetString(guesser + "Name");

        secretWord = PlayerPrefs.GetString("WordToGuess");

        player1Text.text = player1;
        player2Text.text = player2;

        currentWordDisplay = new string('_', secretWord.Length);
        hiddenWordText.text = currentWordDisplay;

        incorrectGuesses = 0;

        turnIndicator.text = $"Le mot a été choisi par {wordGiverName}. {guesserName} commence à deviner.";
        
        RestartButton.SetActive(false);
    }

    public void GuessLetter()
{
    string input = letterInput.text.ToLower();
    letterInput.text = "";
    
    if (input.Length != 1 || !char.IsLetter(input[0])) 
    {
        turnIndicator.text = "Veuillez entrer une lettre valide !";
        return;
    }

    char guessedLetter = input[0];

    bool found = false;
    char[] displayChars = currentWordDisplay.ToCharArray();
    
    for (int i = 0; i < secretWord.Length; i++)
    { 
        if (char.ToLower(secretWord[i]) == guessedLetter)
        {
            displayChars[i] = secretWord[i];
            found = true;
        }
    }

    currentWordDisplay = new string(displayChars);
    hiddenWordText.text = currentWordDisplay;

    
    if (currentWordDisplay == secretWord)
    {
        winner = wordGiver == "Player1" ? PlayerPrefs.GetString("Player2Name") : PlayerPrefs.GetString("Player1Name");
        turnIndicator.text = $"{winner} a trouvé le mot et a gagné !";
        RestartButton.SetActive(true);
        return;
    }

    
    if (!found)
    {
        incorrectGuesses++;

        if (incorrectGuesses >= maxIncorrectGuesses)
        {
            loser = wordGiver == "Player1" ? PlayerPrefs.GetString("Player2Name") : PlayerPrefs.GetString("Player1Name");
            turnIndicator.text = $"{loser} a perdu, il a fait trop d'erreurs !";
            RestartButton.SetActive(true);
        }
        else
        {
            if (wordGiver == "Player1")
            {
                turnIndicator.text = $"Mauvaise lettre ! {PlayerPrefs.GetString("Player2Name")} doit continuer à deviner.";
            }
            else
            {
                turnIndicator.text = $"Mauvaise lettre ! {PlayerPrefs.GetString("Player1Name")} doit continuer à deviner.";
            }
        }
    }
    else
    {
        
        if (wordGiver == "Player1")
        {
            turnIndicator.text = $"C'est toujours le tour de {PlayerPrefs.GetString("Player2Name")} pour deviner.";
        }
        else
        {
            turnIndicator.text = $"C'est toujours le tour de {PlayerPrefs.GetString("Player1Name")} pour deviner.";
        }
    }
}


    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
