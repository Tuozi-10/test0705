using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //WTS = word to search
    public List<string> guessedCharacters;
    public List<string> lettersToGuess;
    public string wordToSearch;
    public int wrongGuessCount;
    public int Winner = 0; // 0 = any, 1 = J1 won, 2 = J2 won
    
    public string J1name = "J1";
    public string J2name = "J2";
    public bool isJ1turn = true;
    
    public static GameManager Instance;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Update()
    {
        if (wrongGuessCount == 6) Winner = 2;
    }
}
