using System.Linq;
using TMPro;
using UnityEngine;

public class StartupMenu : MonoBehaviour
{
    #region Serialized Fields

    [Header("References")]
    [SerializeField] private TMP_Text chooseNameTextField;
    [SerializeField] private TMP_Text warningTextField;
    [SerializeField] private TMP_InputField inputField;

    [Header("Parameters")]
    [SerializeField] private string player1Text = "<color=\"red\">Player 1</color>";
    [SerializeField] private string player2Text = "<color=\"blue\">Player 2</color>";
    [SerializeField] private string chooseNameText = ", choose name:";
    [SerializeField] private string chooseWordText = ", choose word:";
    [SerializeField] private string emptyInputWarning = "Invalid input. Must be at least one character!";
    [SerializeField] private string invalidLettersWarning = "Invalid input. Must contains only letters!";
    [SerializeField] private string namePlaceholder = "Player name...";
    [SerializeField] private string wordPlaceholder = "Word...";

    #endregion

    #region State Machine
    
    public enum StartupMenuState
    {
        Player1Name,
        Player2Name,
        Word
    }

    private StartupMenuState state;

    public StartupMenuState State
    {
        get => state;
        set
        {
            state = value;
            ChangeState();
        }
    }
    
    #endregion

    private void Awake()
    {
        inputField.onSubmit.AddListener(OnFieldInputSubmit);
    }

    private void ChangeState()
    {
        string text = state switch
        {
            StartupMenuState.Player1Name => player1Text + chooseNameText,
            StartupMenuState.Player2Name => player2Text + chooseNameText,
            StartupMenuState.Word => player1Text + chooseWordText,
            _ => ""
        };

        UpdateTexts(text, state == StartupMenuState.Word);
    }

    private void UpdateTexts(string text, bool word)
    {
        inputField.text = "";
        ((TMP_Text)inputField.placeholder).text = word ? wordPlaceholder : namePlaceholder;
        chooseNameTextField.text = text;
    }

    #region Input Field

    private void OnFieldInputSubmit(string input)
    {
        if (IsInputValid(input))
        {
            ValidateInput(input);
        }
        
        inputField.Select();
        inputField.ActivateInputField();
    }

    private bool IsInputValid(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            warningTextField.text = emptyInputWarning;
            return false;
        }

        if (!input.All(char.IsLetter))
        {
            warningTextField.text = invalidLettersWarning;
            return false;
        }

        return true;
    }
    
    private void ValidateInput(string input)
    {
        switch (state)
        {
            case StartupMenuState.Player1Name:
                Hangman.Instance.Player1Name = $"<color=\"red\">{input}</color>";
                State = StartupMenuState.Player2Name;
                break;
            case StartupMenuState.Player2Name:
                Hangman.Instance.Player2Name = $"<color=\"blue\">{input}</color>";
                State = StartupMenuState.Word;
                break;
            case StartupMenuState.Word:
                Hangman.Instance.Word = TextTools.RemoveAccents(input.ToUpper());
                MenuManager.Instance.State = MenuManager.MenuState.Game;
                break;
        }
    }

    #endregion
}