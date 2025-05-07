using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerController> players { get; private set; } = new List<PlayerController>();
    public static PlayerManager Instance;
    [SerializeField] private TMP_InputField registerInput;
    [SerializeField] private PlayerDisplayField playerPrefab;
    [SerializeField] private GameObject playerListContainer;
    [SerializeField] private TMP_InputField createWordInput;
    [SerializeField] private TMP_InputField findWordInput;
    private List<PlayerDisplayField> displayedFields = new List<PlayerDisplayField>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        DisplayManager.Instance.SetState(DisplayManager.ScreenState.RegisterPlayer);
        DisplayManager.Instance.DisplayLastMatch();
        registerInput.onSubmit.AddListener(NewPlayerJoin);
        createWordInput.onSubmit.AddListener(GameManager.Instance.CreateWordToFind);
        findWordInput.onValueChanged.AddListener(OnLetterTyped);
    }

    private void NewPlayerJoin(string playerName)
    {
        foreach (PlayerController.Players player in Enum.GetValues(typeof(PlayerController.Players)))
        {
            if (!PlayerController.activePlayers.Contains(player))
            {
                PlayerController newPlayer = new PlayerController(player, playerName);
                players.Add(newPlayer);
                PlayerDisplayField field = Instantiate(playerPrefab, playerListContainer.transform);
                field.SetInformation(player, playerName);
                displayedFields.Add(field);
                break;
            }
        }
    }

    private void OnLetterTyped(string text)
    {
        if (string.IsNullOrEmpty(text)) return;
        char letter = char.ToUpper(text[^1]);
        GameManager.Instance.SubmitLetter(letter);
    }
}