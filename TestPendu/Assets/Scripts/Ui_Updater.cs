using System.Collections;
using TMPro;
using UnityEngine;

public class Ui_Updater : MonoBehaviour
{
    public static Ui_Updater SINGLETON { get; private set; }
    
    public TextMeshProUGUI wordZone;
    public TMP_InputField LetterArea;
    
    public TextMeshProUGUI messageZone;
    public TextMeshProUGUI versusText;
    [SerializeField] private GameObject[] hangmanStages;

    void Awake()
    {
        if (SINGLETON != null)
        {
            Debug.LogError("Multiple Ui_Updater instances");
            Destroy(gameObject);
            return;
        }
        SINGLETON = this;
    }

    public void UpdateWordDisplay(string display)
    {
        wordZone.text = display;
        UpdateVersusText();
    }

    public void ClearInputField()
    {
        LetterArea.text = "";
    }

    public string GetInputLetter()
    {
        string input = LetterArea.text.ToLower();
        PlayerSaves.Instance.SaveGuess(input);
        return input;
    }
    
    public void ShowMessage(string msg)
    {
        messageZone.text = msg;
    }
    
    public void ShowNextHangmanStep()
    {
        for (int i = 0; i < hangmanStages.Length; i++)
        {
            if (!hangmanStages[i].activeSelf)
            {
                hangmanStages[i].SetActive(true);
                break;
            }
        }
    }

    public void UpdateVersusText()
    {
        if (PlayerManager.SINGLETON.playerNames.Count >= 2)
        {
            string player1 = PlayerManager.SINGLETON.playerNames[0];
            string player2 = PlayerManager.SINGLETON.playerNames[1];
            versusText.text = $"{player1} contre {player2}";
        }
        else
        {
            versusText.text = "Joueurs non définis";
        }
    }

    public void Save()
    {
        const int resultCount = 3;
        for (int i = 0; i < resultCount; i++)
        {
            PlayerPrefs.SetString($"nameP1_partie{i}", PlayerManager.SINGLETON.name1);
            PlayerPrefs.SetString($"nameP2_partie{i}", PlayerManager.SINGLETON.name2);
            PlayerPrefs.SetString($"mot_partie{i}", GameManager.SINGLETON.currentWord);
            PlayerPrefs.SetString($"winner_partie{i}", GameManager.SINGLETON.winner);
            PlayerPrefs.SetString($"looser_partie{i}", GameManager.SINGLETON.looser);
            PlayerPrefs.Save();
        }
    }
}