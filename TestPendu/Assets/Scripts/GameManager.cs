using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private PenduGame penduGame;
    [SerializeField] private TMP_Text scoreboardText;
    [SerializeField] private TMP_Text resultText;
    
    [SerializeField] private GameObject playBouton;
    [SerializeField] private GameObject resetBouton;
    [SerializeField] private GameObject endBouton;
    
    
    [SerializeField] private RectTransform movingTextTransform;
    [SerializeField] private RectTransform pointA;
    [SerializeField] private RectTransform pointB;
    [SerializeField] private float tweenDuration = 0.5f;

    [HideInInspector]
    public bool movedToA = false;
    
    private bool isInGame = false;

    private const string PlayerListKey = "PlayerList";
    private List<string> playerList = new List<string>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playBouton.gameObject.SetActive(true);
        resetBouton.gameObject.SetActive(false);
        isInGame = false;
        movedToA = false;
        movingTextTransform.gameObject.SetActive(false);
        LoadPlayerList();
        UpdateScoreboardUI();
    }
    
    public void StartGame()
    {
        isInGame = true;
        playBouton.gameObject.SetActive(false);
        resetBouton.gameObject.SetActive(true);
        penduGame.StartParty();
    }


    public void EndGame(bool guesserWon)
    {
        isInGame = false;
        string winner = guesserWon
            ? penduGame.GuesserNameString
            : penduGame.setterNameString;

        AddPlayerIfNew(winner);


        string scoreKey = "Score_" + winner;
        int newScore = PlayerPrefs.GetInt(scoreKey, 0) + 1;
        PlayerPrefs.SetInt(scoreKey, newScore);


        
        string msg = guesserWon
            ? $"Le joueur {penduGame.GuesserNameString} a trouvé le mot de {penduGame.setterNameString} : {penduGame.SecretWord}"
            : $"Le joueur {penduGame.GuesserNameString} n'a pas trouvé le mot de {penduGame.setterNameString} : {penduGame.SecretWord}";
        PlayerPrefs.SetString("LastGameResult", msg);
        PlayerPrefs.Save();
        
        SavePlayerList();
        UpdateScoreboardUI();

    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadPlayerList()
    {
        string data = PlayerPrefs.GetString(PlayerListKey, "");
        playerList = !string.IsNullOrEmpty(data)
            ? data.Split(',').ToList()
            : new List<string>();
        
        string raw = PlayerPrefs.GetString("GameResults", "");
        resultText.text = raw;


    }

    private void SavePlayerList()
    {
        PlayerPrefs.SetString(PlayerListKey, string.Join(",", playerList));
    }

    private void AddPlayerIfNew(string pseudo)
    {
        if (!playerList.Contains(pseudo))
            playerList.Add(pseudo);
    }

    private void UpdateScoreboardUI()
    {
        //en fonction du score le plus au haut
        var sorted = playerList
            .Select(p => new { Name = p, Score = PlayerPrefs.GetInt("Score_" + p, 0) })
            .OrderByDescending(x => x.Score)
            .ToList();

        var ligne = new System.Text.StringBuilder();
        ligne.AppendLine("Classement général :");
        foreach (var entry in sorted)
            ligne.AppendLine($"{entry.Name} : {entry.Score}");

        scoreboardText.text = ligne.ToString();
        

    }
    

    public void DisplayScore()
    {
        if (!movedToA)
        {
            movingTextTransform.gameObject.SetActive(true);
            movingTextTransform.DOAnchorPos(pointA.anchoredPosition, tweenDuration)
                .OnComplete(() => movedToA = true);
        }
        else
        {
            movingTextTransform.DOAnchorPos(pointB.anchoredPosition, tweenDuration)
                .OnComplete(() =>
                {
                    movedToA = false;
                    movingTextTransform.gameObject.SetActive(false);
                });
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
