using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]private TMP_Text textDisplay;
    
    public void init(int i)
    {
        string word = PlayerPrefs.GetString($"score_{i}_word", "");
        if (word.Length > 0)
        {
            string playerOne = PlayerPrefs.GetString($"score_{i}_p1", "?");
            string playerTwo = PlayerPrefs.GetString($"score_{i}_p2", "?");
            string winner = PlayerPrefs.GetString($"score_{i}_winner", "?");

            textDisplay.text = $"Le mot était : {word}, les joueurs étaient : {playerOne} et {playerTwo}, le gagnant était {winner}";
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

   
}
