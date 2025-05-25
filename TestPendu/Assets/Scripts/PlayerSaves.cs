using System.Collections.Generic;
using UnityEngine;

// tu pourrais te passer d'instances et behaviours ici
public static class PlayerSaves
{

    public static List<string> guesses = new();

    public static void SaveGuess(string guess)
    {
        if (guess != " " || guess != "")
        {
            guesses.Add(guess.ToLower());
            Debug.Log("entry save : " + guess);
        }
    }

    public static void ResetGuesses()
    {
        guesses.Clear();
    }
}
