using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]private List<string> words = new();

    [HideInInspector]
    public string currentWord;
    
    private char[] hiddenWordArray;
    private int currentErrorCount = 0;

    private string currentPlayer;
    public string looser;
    public string winner;

    public static GameManager SINGLETON { get; private set; }

    void Awake()
    {
        if (SINGLETON != null)
        {
            Debug.LogError("Trying to instantiate another GameManager SINGLETON");
            Destroy(gameObject);
            return;
        }
        SINGLETON = this;
    }

    void Start()
    {
        if (PlayerManager.SINGLETON.playerNames.Count < 2)
        {
            Debug.LogError("Il faut deux joueurs pour commencer !");
            return;
        }
        currentPlayer = PlayerManager.SINGLETON.playerNames[0];
        Ui_Updater.SINGLETON.UpdateVersusText();

        ResetWord();
    }

    public void ResetWord()
    {
        currentWord = GetRandom();
        hiddenWordArray = new string('_', currentWord.Length).ToCharArray();
        currentErrorCount = 0;
        Ui_Updater.SINGLETON.UpdateWordDisplay(string.Join(" ", hiddenWordArray));
        Ui_Updater.SINGLETON.LetterArea.enabled = true;

        Debug.Log($"Tour de {currentPlayer}.");
    }

    public void LetterCheck()
    {
        string input = Ui_Updater.SINGLETON.GetInputLetter();
        
        if (string.IsNullOrWhiteSpace(input))
        {
            Ui_Updater.SINGLETON.ShowMessage("Champ vide !");
            Ui_Updater.SINGLETON.ClearInputField();
            return;
        }

        input = input.ToLower();

        if (input.Length == 1)
        {
            char guessedChar = input[0];

            bool alreadyGuessed = new string(hiddenWordArray).ToLower().Contains(guessedChar);
            if (alreadyGuessed)
            {
                Ui_Updater.SINGLETON.ShowMessage($"Lettre '{guessedChar}' déjà proposée.");
            }
            else
            {
                bool found = false;

                for (int i = 0; i < currentWord.Length; i++)
                {
                    if (char.ToLower(currentWord[i]) == guessedChar)
                    {
                        hiddenWordArray[i] = currentWord[i];
                        found = true;
                    }
                }

                if (found)
                {
                    Ui_Updater.SINGLETON.UpdateWordDisplay(string.Join(" ", hiddenWordArray));
                    Ui_Updater.SINGLETON.ShowMessage("Lettre trouvée !");
                }
                else
                {
                    currentErrorCount++;
                    Ui_Updater.SINGLETON.ShowNextHangmanStep();
                }
            }

            if (new string(hiddenWordArray) == currentWord)
            {
                Ui_Updater.SINGLETON.ShowMessage($"Mot trouvé par {currentPlayer} !");
                Ui_Updater.SINGLETON.LetterArea.enabled = false;
                Debug.Log($" {currentPlayer} a gagné !");
                Debug.Log($"{GetOtherPlayer()} a perdu !");
                winner = currentPlayer;
                looser = GetOtherPlayer();
                Ui_Updater.SINGLETON.Save();
            }
            else
            {
                SwitchPlayer();
            }
        }
        else
        {
            if (input == currentWord.ToLower())
            {
                Ui_Updater.SINGLETON.UpdateWordDisplay(string.Join(" ", currentWord.ToCharArray()));
                Ui_Updater.SINGLETON.ShowMessage($"Mot complet trouvé par {currentPlayer} !");
                Ui_Updater.SINGLETON.LetterArea.enabled = false;
                Debug.Log($"{currentPlayer} a gagné !");
                Debug.Log($"{GetOtherPlayer()} a perdu !");
                winner = currentPlayer;
                looser = GetOtherPlayer();
                Ui_Updater.SINGLETON.Save();
            }
            else
            {
                Ui_Updater.SINGLETON.ShowMessage("Mot incorrect !");
                currentErrorCount++;
                Ui_Updater.SINGLETON.ShowNextHangmanStep();
                SwitchPlayer();
            }
        }
        
        Ui_Updater.SINGLETON.ClearInputField();
    }

    
    public void SwitchPlayer()
    {
        switch (currentPlayer)
        {
            case var player when player == PlayerManager.SINGLETON.playerNames[0]:
                currentPlayer = PlayerManager.SINGLETON.playerNames[1];
                break;

            case var player when player == PlayerManager.SINGLETON.playerNames[1]:
                currentPlayer = PlayerManager.SINGLETON.playerNames[0];
                break;

            default:
                Debug.LogWarning("Nom de joueur inconnu");
                break;
        }

        Debug.Log($"C'est maintenant au tour de {currentPlayer}");
    }

    public string GetOtherPlayer()
    {
        return currentPlayer == PlayerManager.SINGLETON.playerNames[0]
            ? PlayerManager.SINGLETON.playerNames[1]
            : PlayerManager.SINGLETON.playerNames[0];
    }

    public string GetRandom()
    {
        if (words.Count == 0)
            return "wordlist_vide";

        int index = Random.Range(0, words.Count);
        return words[index];
    }
}