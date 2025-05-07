using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    [SerializeField] private PlayerLetter player;
    
    public void SaveMatchResult()
    {
        string winner = player.asWin ? GameManager.Instance.PlayerTwo : GameManager.Instance.PlayerOne;

        for (int i = 2; i > 0; i--)
        {
            PlayerPrefs.SetString($"score_{i}_word", PlayerPrefs.GetString($"score_{i - 1}_word", ""));
            PlayerPrefs.SetString($"score_{i}_p1", PlayerPrefs.GetString($"score_{i - 1}_p1", ""));
            PlayerPrefs.SetString($"score_{i}_p2", PlayerPrefs.GetString($"score_{i - 1}_p2", ""));
            PlayerPrefs.SetString($"score_{i}_winner", PlayerPrefs.GetString($"score_{i - 1}_winner", ""));
        }

        PlayerPrefs.SetString("score_0_word", GameManager.Instance.Word);
        PlayerPrefs.SetString("score_0_p1", GameManager.Instance.PlayerOne);
        PlayerPrefs.SetString("score_0_p2", GameManager.Instance.PlayerTwo);
        PlayerPrefs.SetString("score_0_winner", winner);
        PlayerPrefs.Save();
        player.asWin = false;
        player.asLose = false;
    }
}
