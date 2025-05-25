using UnityEngine;

public static class PlayerPrefSave
{
    // c'est un peu dommage, dans l'ensemble le code est OK, bien dispatché, mais il manque un peu de contenu pour pouvoir juger vraiment
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
