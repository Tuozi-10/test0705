using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    public static DisplayManager Instance;
    [SerializeField] private GameObject registerScreen;
    [SerializeField] private GameObject createWordScreen;
    [SerializeField] private GameObject findWordScreen;
    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private TMP_Text creatorText;
    [SerializeField] private TMP_Text endGameText;
    [SerializeField] private LetterDisplayField letterPrefab;
    [SerializeField] private GameObject wordContainer;
    [SerializeField] private PenduDisplayController pendu;
    [SerializeField] private TMP_Text lastMatchText;
    private List<LetterDisplayField> letters = new List<LetterDisplayField>();
    
    public enum ScreenState { RegisterPlayer, CreateWord, FindWord, EndGame }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetState(ScreenState state)
    {
        registerScreen.SetActive(false);
        createWordScreen.SetActive(false);
        findWordScreen.SetActive(false);
        endGameScreen.SetActive(false);
        switch (state)
        {
            case ScreenState.RegisterPlayer: registerScreen.SetActive(true); break;
            case ScreenState.CreateWord: createWordScreen.SetActive(true); break;
            case ScreenState.FindWord: findWordScreen.SetActive(true); break;
            case ScreenState.EndGame: endGameScreen.SetActive(true); break;
        }
    }

    public void DisplayWordCreatorScreen(string text)
    {
        creatorText.text = text;
        SetState(ScreenState.CreateWord);
    }

    public void InitWordDisplayer(string word)
    {
        foreach (LetterDisplayField letter in letters) Destroy(letter.gameObject);
        letters.Clear();
        foreach (char letter in word)
            letters.Add(Instantiate(letterPrefab, wordContainer.transform));
        SetState(ScreenState.FindWord);
    }

    public void DisplayLetterAtPosition(char letter, int position)
    {
        letters[position].SetLetter(letter);
    }

    public void UpdatePendu()
    {
        pendu.ShowElement();
    }

    public void SetEndGameText(string text)
    {
        endGameText.text = text;
        SetState(ScreenState.EndGame);
    }

    public void DisplayLastMatch()
    {
        lastMatchText.text = PlayerPrefSave.LoadLastMatchInformations();
    }
}
