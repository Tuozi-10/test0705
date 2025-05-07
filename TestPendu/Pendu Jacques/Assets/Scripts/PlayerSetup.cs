using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerSetup : MonoBehaviour
{
    public TMP_InputField player1Field;
    public TMP_InputField player2Field;
    public TMP_InputField wordtoGuess;
    public TMP_Dropdown wordGiverDropdown;
    
    
    void Start()
    {
        if (PlayerPrefs.HasKey("Player1Name"))
        {
            player1Field.text = PlayerPrefs.GetString("Player1Name");
        }
        
        if (PlayerPrefs.HasKey("Player2Name"))
        {
            player2Field.text = PlayerPrefs.GetString("Player2Name");
        }


        if (PlayerPrefs.HasKey("WordToGuess"))
        {
            wordtoGuess.text = PlayerPrefs.GetString("WordToGuess");
        }
        
    }


    public void SaveInputs()
    {
        PlayerPrefs.SetString("Player1Name", player1Field.text);
        PlayerPrefs.SetString("Player2Name", player2Field.text);
        PlayerPrefs.SetString("WordToGuess", wordtoGuess.text);
        if (wordGiverDropdown.value == 0)
        {
            PlayerPrefs.SetString("WordGiver", "Player1");
            PlayerPrefs.SetString("Guesser", "Player2");
        }
        else
        {
            PlayerPrefs.SetString("WordGiver", "Player2");
            PlayerPrefs.SetString("Guesser", "Player1");
        }
        SceneManager.LoadScene(2);
    }
}