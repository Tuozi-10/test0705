using UnityEngine;

public struct SaveData
{
    public string Player1Name;
    public string Player2Name;
    public string Word;
    public int Winner;
}

public static class PlayerPrefsTools
{
    public static void SaveGame(string player1Name, string player2Name, string word, int winner)
    {
        PlayerPrefs.SetString("player1Name1", player1Name);
        PlayerPrefs.SetString("player2Name1", player2Name);
        PlayerPrefs.SetString("word1", word);
        PlayerPrefs.SetInt("winner1", winner);
    }

    public static void CycleSaves()
    {
        for (int i = Common.SaveAmount; i > 1; i--)
        {
            if (!HasSave(i - 1)) continue;
            
            string player1Name = PlayerPrefs.GetString("player1Name" + (i - 1));
            string player2Name = PlayerPrefs.GetString("player2Name" + (i - 1));
            string word = PlayerPrefs.GetString("word" + (i - 1));
            int winner = PlayerPrefs.GetInt("winner" + (i - 1));
            
            PlayerPrefs.SetString("player1Name" + i, player1Name);
            PlayerPrefs.SetString("player2Name" + i, player2Name);
            PlayerPrefs.SetString("word" + i, word);
            PlayerPrefs.SetInt("winner" + i, winner);
        }
    }

    public static SaveData LoadSave(int i)
    {
        return new SaveData
        {
            Player1Name = PlayerPrefs.GetString("player1Name" + i),
            Player2Name = PlayerPrefs.GetString("player2Name" + i),
            Word = PlayerPrefs.GetString("word" + i),
            Winner = PlayerPrefs.GetInt("winner" + i)
        };
    }

    public static bool HasSave(int i)
    {
        return PlayerPrefs.HasKey("player1Name" + i);
    }
}