using UnityEngine;

public static class PlayerPrefSave
{
    public static void SaveResult(string wordToFind, string winnerName, string looserName)
    {
        PlayerPrefs.SetString("match1_wordToFind" , wordToFind);
        PlayerPrefs.SetString("match1_winnerName" , winnerName);
        PlayerPrefs.SetString("match1_looserName" , looserName);
    }
    
    public static string LoadLastMatchInformations()
    {
        string word   = PlayerPrefs.GetString("match1_wordToFind", "");
        string winner = PlayerPrefs.GetString("match1_winnerName", "");
        string looser = PlayerPrefs.GetString("match1_looserName", "");
        return $"Le mot à trouver : {word}\nGagnant : {winner}\nPerdant : {looser}";
    }

}
