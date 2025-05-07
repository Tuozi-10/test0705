using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string wordToFind;
    private HashSet<int> foundPositions = new HashSet<int>();
    private HashSet<char> foundLetters = new HashSet<char>();
    private PlayerController.Players currentPlayer;
    private const int maxErrors = 6;
    private int currentPlayerIndex;
    public int currentErrors = maxErrors;
    public static GameManager Instance;

    private List<PlayerController> players => PlayerManager.Instance.players;
    
    public enum EndGameState { Win, Lose }
    public enum Roles { Creator, Finder }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Init2PlayersGame()
    {
        if (players.Count < 2) return;
        while (players.Count > 2)
            players.RemoveAt(players.Count - 1);
        ShowWordCreatorScreen();
        DisplayManager.Instance.SetState(DisplayManager.ScreenState.CreateWord);
    }

    public void CreateWordToFind(string submittedWord)
    {
        submittedWord = submittedWord.ToUpper();
        if (!IsWordValid(submittedWord)) return;
        wordToFind = submittedWord;
        SwitchTurn();
        DisplayManager.Instance.InitWordDisplayer(wordToFind);
    }
    
    private void ShowWordCreatorScreen()
    {
        PlayerController creator = players[currentPlayerIndex];
        DisplayManager.Instance.DisplayWordCreatorScreen($"{creator.playerName} doit choisir un mot :");
    }

    public void SubmitLetter(char letter)
    {
        if (!IsLetterValid(letter)) return;
        if (foundLetters.Contains(letter)) return;
        if (IsLetterInWord(letter, wordToFind, out List<int> positions))
        {
            AddFoundLetter(letter, positions);
            ShowFoundLetter(letter, positions);
        }
        else WrongLetterSubmitted();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SwitchTurn()
    {
        currentPlayer = players[(int)currentPlayer].player;
        int next = ((int)currentPlayer + 1) % players.Count;
        currentPlayer = (PlayerController.Players)next;
    }

    private bool IsWordValid(string word)
    {
        if (string.IsNullOrEmpty(word)) return false;
        foreach (char letter in word)
            if (!IsLetterValid(letter)) return false;
        return true;
    }

    private bool IsLetterValid(char c) => c >= 'A' && c <= 'Z';

    private bool IsLetterInWord(char letter, string word, out List<int> positions)
    {
        positions = new List<int>();
        for (int i = 0; i < word.Length; i++)
            if (word[i] == letter) positions.Add(i);
        return positions.Count > 0;
    }

    private void AddFoundLetter(char letter, List<int> positions)
    {
        foundLetters.Add(letter);
        foreach (int position in positions) foundPositions.Add(position);
        if (foundPositions.Count == wordToFind.Length)
            EndGame(EndGameState.Win);
    }

    private void ShowFoundLetter(char letter, List<int> positions)
    {
        foreach (int position in positions)
            DisplayManager.Instance.DisplayLetterAtPosition(letter, position);
    }

    private void WrongLetterSubmitted()
    {
        currentErrors--;
        DisplayManager.Instance.UpdatePendu();
        if (currentErrors <= 0) EndGame(EndGameState.Lose);
    }

    private void EndGame(EndGameState state)
    {
        string winnerName = state == EndGameState.Win ? players[currentPlayerIndex].playerName : players[1 - currentPlayerIndex].playerName;
        string loserName  = state == EndGameState.Win ? players[1 - currentPlayerIndex].playerName : players[currentPlayerIndex].playerName;
        PlayerPrefSave.SaveResult(wordToFind, winnerName, loserName);

        DisplayManager.Instance.SetState(DisplayManager.ScreenState.EndGame);
        SetEndGameScreen(state);
    }

    private void SetEndGameScreen(EndGameState state)
    {
        string result = state == EndGameState.Win ? "a gagné" : "a perdu";
        string playerName = state == EndGameState.Win ? players[currentPlayerIndex].playerName : players[1 - currentPlayerIndex].playerName;
        DisplayManager.Instance.SetEndGameText($"{playerName} {result}");
    }

}
