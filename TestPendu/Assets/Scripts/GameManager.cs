using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;

    public string playerOneName { get; private set; }
    public string playerTwoName { get; private set; }
    
    [HideInInspector] public TMP_Text actualText;
    [HideInInspector] public GameObject[] hangedMan;
    
    private string word;
    [HideInInspector] public int numberOfTurn;
    
    
    void Start()
    {
        if (INSTANCE != null)
        {
            Destroy(gameObject);
        }
        else
        {
            INSTANCE = this;
            DontDestroyOnLoad(INSTANCE);
        }
    }
    public void SetPlayerName(int playerNumber, string playerName)
    {
        if (playerNumber == 1)
        {
            playerOneName = playerName;
        }
        else
        {
            playerTwoName = playerName;
        }
        
    }
    public void SetWord(string newWord)
    {
        word = newWord;
    }

    public void StartGame()
    {
        if (actualText.text == "")
        {
            for (int i = 0; i < word.Length; i++)
            {
                actualText.text += "-";
            }
        }
    }
    void UpdateUi(char letter)
    {
        char[] chars = actualText.text.ToCharArray();

        for (int i = 0; i < word.Length; i++)
        {
            if (word[i] == letter && i < chars.Length)
            {
                chars[i] = letter;
            }
        }
        actualText.text = new string(chars);
    }

    bool HaveLetter(char letter)
    {
        for (int i = 0; i < word.Length; i++)
        {
            if (word[i] == letter )
            {
                return true;
            }
        }
        return false;
    }

    public void Play(char letter)
    {
        if (!HaveLetter(letter))
        {
            numberOfTurn += 1;
            hangedMan[numberOfTurn - 1].SetActive(true);
        }
        UpdateUi(letter);
        CheckWin();
        
    }

    void CheckWin()
    {
        if (numberOfTurn >= 6)
        {
            UpdatePlayerPrefs(playerOneName, playerTwoName);
            SceneManager.LoadScene(5);
        }
        else
        {
            bool j2win = true;
            for (int i = 0; i < word.Length; i++)
            {
                if (actualText.text[i] == '-')
                {
                    j2win = false;
                    break;
                }
            }
            if (j2win)
            {
                UpdatePlayerPrefs(playerTwoName, playerOneName);
                SceneManager.LoadScene(6);
            }
        }
    }

    private void UpdatePlayerPrefs(string winnerName, string looserName)
    {
        for (int i = 0; i < 2; i++)
        {
            PlayerPrefs.SetString("winner" + (i+1), PlayerPrefs.GetString("winner" + (i+2)));
            PlayerPrefs.SetString("looser"+ (i+1), PlayerPrefs.GetString("looser"+ (i+2)));
            PlayerPrefs.SetString("word"+ (i+1), PlayerPrefs.GetString("word"+ (i+2)));
        }
        
        PlayerPrefs.SetString("winner3", winnerName);
        PlayerPrefs.SetString("looser3", looserName);
        PlayerPrefs.SetString("word3", word);
        
        PlayerPrefs.Save();
    }
}
