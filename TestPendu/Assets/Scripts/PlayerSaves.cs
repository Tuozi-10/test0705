using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSaves : MonoBehaviour
{
    public static PlayerSaves Instance;

    public List<string> guesses = new();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGuess(string guess)
    {
        if (guess != " " || guess != "")
        {
            guesses.Add(guess.ToLower());
            Debug.Log("entry save : " + guess);
        }
    }

    public void ResetGuesses()
    {
        guesses.Clear();
    }
}
