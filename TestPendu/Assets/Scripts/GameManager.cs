using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MenuObj;
    public GameObject BeforeGameObj;
    public GameObject PenduGameObj;
    public string J1Name;
    public string J2Name;
    public string WordToGuess;
    public string WinOrLoss;
    public int numberOfMissedTrys;

    public void SaveGame()
    {
        if (!PlayerPrefs.HasKey("J1Name1"))
        {
            Save(1);
        }
        else if (!PlayerPrefs.HasKey("J2Name2"))
        {
            Save(2);
        }
        else
        {
            Save(3);
        }
        PlayerPrefs.Save();
    }

    void Save(int n)
    {
        for (int i = n; i > 1; i--)
        {
            PlayerPrefs.SetString($"J1Name{i}",PlayerPrefs.GetString($"J1Name{i-1}"));
            PlayerPrefs.SetString($"Win{i}",PlayerPrefs.GetString($"Win{i-1}"));
            PlayerPrefs.SetString($"J2Name{i}",PlayerPrefs.GetString($"J2Name{i-1}"));
            PlayerPrefs.SetString($"Word{i}",PlayerPrefs.GetString($"Word{i-1}"));
            PlayerPrefs.SetInt($"Score{i}",PlayerPrefs.GetInt($"Score{i-1}"));
        }
        PlayerPrefs.SetString("J1Name1", J1Name);
        PlayerPrefs.SetString("Win1", WinOrLoss);
        PlayerPrefs.SetString("J2Name1", J2Name);
        PlayerPrefs.SetString("Word1", WordToGuess);
        PlayerPrefs.SetInt("Score1", numberOfMissedTrys);
    }
    
    /*void SaveTo1()
    {
        PlayerPrefs.SetString("J1Name1", J1Name);
        PlayerPrefs.SetString("Win1", WinOrLoss);
        PlayerPrefs.SetString("J2Name1", J2Name);
        PlayerPrefs.SetString("Word1", WordToGuess);
        PlayerPrefs.SetInt("Score1", numberOfMissedTrys);
    }
    void SaveTo2()
    {
        PlayerPrefs.SetString("J1Name2", PlayerPrefs.GetString("J1Name1"));
        PlayerPrefs.SetString("Win2", PlayerPrefs.GetString("Win1"));
        PlayerPrefs.SetString("J2Name2", PlayerPrefs.GetString("J2Name1"));
        PlayerPrefs.SetString("Word2", PlayerPrefs.GetString("Word1"));
        PlayerPrefs.SetInt("Score2", PlayerPrefs.GetInt("Score1"));
        SaveTo1();
    }
    void SaveTo3()
    {
        PlayerPrefs.SetString("J1Name3", PlayerPrefs.GetString("J1Name2"));
        PlayerPrefs.SetString("Win3", PlayerPrefs.GetString("Win2"));
        PlayerPrefs.SetString("J2Name3", PlayerPrefs.GetString("J2Name2"));
        PlayerPrefs.SetString("Word3", PlayerPrefs.GetString("Word2"));
        PlayerPrefs.SetInt("Score3", PlayerPrefs.GetInt("Score2"));
        SaveTo2();
    }*/
}
