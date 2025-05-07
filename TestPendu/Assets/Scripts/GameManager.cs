using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    List<string> warhammerWords = new List<string>
    {
        "Sigmar", "Ulthuan", "Druchii", "Greenskin", "Skaven", "Vampire", "Necromancer", "Waaagh", "Grimgor", "Karak",
        "Norsca", "Chaos", "Tzeentch", "Slaanesh", "Khorne", "Nurgle", "Naggaroth", "Lustria", "Bretonnia", "Empire",
        "Reikland", "Helstorm", "Grudge", "Rune", "Ancestor", "Clanrat", "Stormvermin", "Bloodletting", "Black Ark",
        "Darkshard", "Tomb King", "Liche Priest", "Settra", "Slann", "Winds of Magic", "Witch Hunter", "Grave Guard",
        "Hexwraith", "Vargheist", "Cygor", "Hellpit", "Black Coach", "Doomwheel", "Ursun", "Warpstone", "Daemonhost", 
        "Runesmith", "Quarrelers", "Thane", "Slayer", "Kislev", "Ice Witch", "Tzarina", "Cathay", "Monkey King", 
        "Great Bastion", "Celestial Dragon", "Terracotta Sentinel", "Harmony", "Bloodthirster", "Keeper of Secrets"
    };

    [Header("Initial Canvas")] 
    public GameObject initialCanvasReference;
    public GameObject promptText;
    public GameObject player1NameInput, player2NameInput;

    [Header("Game Canvas")] 
    public GameObject gameCanvasReference;
    public GameObject player1UI, player2UI;
    public GameObject displayWordText;
    public GameObject player1NameText, player2NameText;
    public GameObject player1WordInput, player2WordInput;
    
    public GameObject player1ChancesText, player2ChancesText;
    public GameObject menuButton;
    
    public GameObject winnerText;

    public GameObject[] player1Body;
    public GameObject[] player2Body;

    private int chancesP1 = 7;
    private int chancesP2 = 7;
    private bool isPlayer1Turn = true;
    private bool player1Win;
    private string currentWord;
    private string displayWord;

    void Start()
    {
        promptText.GetComponent<TMP_Text>().text = "Please enter player names:";
        player1NameInput.SetActive(true);
        player2NameInput.SetActive(true);
    }

    public void StartGame()
    {
        if (string.IsNullOrEmpty(player1NameInput.GetComponent<InputField>().text) || string.IsNullOrEmpty(player2NameInput.GetComponent<InputField>().text))
        {
            StartCoroutine(ShowErrorMessage("Name mustn't be NULL or empty", 2f));
        }
        else
        {
            player1NameText.GetComponent<TMP_Text>().text = player1NameInput.GetComponent<InputField>().text;
            player2NameText.GetComponent<TMP_Text>().text = player2NameInput.GetComponent<InputField>().text;
            
            initialCanvasReference.SetActive(false);
            gameCanvasReference.SetActive(true);
            
            currentWord = warhammerWords[Random.Range(0, warhammerWords.Count)];
            displayWord = new string('_', currentWord.Length);
            displayWordText.GetComponent<TMP_Text>().text = displayWord;

            player1ChancesText.GetComponent<TMP_Text>().text = "Chances: " + chancesP1;
            player2ChancesText.GetComponent<TMP_Text>().text = "Chances: " + chancesP2;

            menuButton.SetActive(true);
            SetPlayerTurnUI();
        }
    }

    public void Player1Submit()
    {
        if (isPlayer1Turn)
        {
            string playerGuess = player1WordInput.GetComponent<InputField>().text;
            ProcessGuess(playerGuess);
        
            player1WordInput.GetComponent<InputField>().text = "";

            isPlayer1Turn = false;
            SetPlayerTurnUI();
        }
    }

    public void Player2Submit()
    {
        if (!isPlayer1Turn)
        {
            string playerGuess = player2WordInput.GetComponent<InputField>().text;
            ProcessGuess(playerGuess);

            player2WordInput.GetComponent<InputField>().text = "";

            isPlayer1Turn = true;
            SetPlayerTurnUI();
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
   
    private void ProcessGuess(string playerGuess)
    {
        if (string.IsNullOrEmpty(playerGuess) || playerGuess.Length > 1)
        {
            return;
        }

        bool correctGuess = false;
        string newDisplayWord = "";

        for (int i = 0; i < currentWord.Length; i++)
        {
            if (currentWord[i].ToString().ToLower() == playerGuess.ToLower())
            {
                newDisplayWord += currentWord[i];
                correctGuess = true;
            }
            else
            {
                newDisplayWord += displayWord[i];
            }
        }

        if (correctGuess)
        {
            displayWord = newDisplayWord;
            displayWordText.GetComponent<TMP_Text>().text = displayWord;
        }
        else
        {
            if (isPlayer1Turn)
            {
                chancesP1--;
                player1ChancesText.GetComponent<TMP_Text>().text = "Chances: " + chancesP1;
            }
            else
            {
                chancesP2--;
                player2ChancesText.GetComponent<TMP_Text>().text = "Chances: " + chancesP2;
            }
        }

        if (chancesP1 <= 0 || chancesP2 <= 0 || displayWord == currentWord)
        {
            EndGame();
            return;
        }

        Save();
    }


    
    private void EndGame()
    { 
        winnerText.SetActive(true);
        player1WordInput.GetComponent<InputField>().interactable = false;
        player2WordInput.GetComponent<InputField>().interactable = false;

        string winner = "";

        if (displayWord == currentWord)
        {
            winner = isPlayer1Turn ? player1NameText.GetComponent<TMP_Text>().text : player2NameText.GetComponent<TMP_Text>().text;
        }
        else
        {
            winner = chancesP1 > 0 ? player1NameText.GetComponent<TMP_Text>().text : player2NameText.GetComponent<TMP_Text>().text;
        }

        winnerText.GetComponent<TMP_Text>().text = $"{winner} wins! The Word was: {currentWord}";

    }
    
    private void SetPlayerTurnUI()
    {
        player1UI.SetActive(isPlayer1Turn); 
        player2UI.SetActive(!isPlayer1Turn);

        player1ChancesText.GetComponent<TMP_Text>().text = "Chances: " + chancesP1;
        player2ChancesText.GetComponent<TMP_Text>().text = "Chances: " + chancesP2;
    }


    private IEnumerator ShowErrorMessage(string message, float delay)
    {
        promptText.GetComponent<TMP_Text>().text = message;
        yield return new WaitForSeconds(delay);
        promptText.GetComponent<TMP_Text>().text = "Please enter player names:";
    }

    private void Save()
    {
        for (int i = 2; i >= 1; i--)
        {
            if (PlayerPrefs.HasKey($"Save{i}/Player1"))
            {
                PlayerPrefs.SetString($"Save{i - 1}/Player1", PlayerPrefs.GetString($"Save{i}/Player1"));
                PlayerPrefs.SetString($"Save{i - 1}/Player2", PlayerPrefs.GetString($"Save{i}/Player2"));
                PlayerPrefs.SetString($"Save{i - 1}/Word", PlayerPrefs.GetString($"Save{i}/Word"));
            }
        }

        PlayerPrefs.SetString("Save1/Player1", player1NameText.GetComponent<TMP_Text>().text);
        PlayerPrefs.SetString("Save1/Player2", player2NameText.GetComponent<TMP_Text>().text);
        PlayerPrefs.SetString("Save1/Word", currentWord);
        PlayerPrefs.Save();
    }
}
