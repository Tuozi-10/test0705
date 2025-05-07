using UnityEngine;

namespace Script.Manager
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void Save()
        {
            PlayerPrefs.SetString("namePlayer1_3",PlayerPrefs.GetString("namePlayer1_2"));
            PlayerPrefs.SetString("namePlayer2_3",PlayerPrefs.GetString("namePlayer2_2"));
            PlayerPrefs.SetString("word_3",PlayerPrefs.GetString("word_2"));
            PlayerPrefs.SetString("win_3",PlayerPrefs.GetString("win_2"));
            
            PlayerPrefs.SetString("namePlayer1_2", PlayerPrefs.GetString("namePlayer1_1"));
            PlayerPrefs.SetString("namePlayer2_2",PlayerPrefs.GetString("namePlayer2_1"));
            PlayerPrefs.SetString("word_2",PlayerPrefs.GetString("word_1"));
            PlayerPrefs.SetString("win_2",PlayerPrefs.GetString("win_1"));
            
            PlayerPrefs.SetString("namePlayer1_1",PlayerManager.instance.players.nameFirstPlayer);
            PlayerPrefs.SetString("namePlayer2_1",PlayerManager.instance.players.nameSecondPlayer);
            PlayerPrefs.SetString("word_1",PlayerManager.instance.wordToGuess);
            PlayerPrefs.SetString("win_1",PlayerManager.instance.winner);

            
            
            
        }
    }
}