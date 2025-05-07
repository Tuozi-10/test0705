using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    
    #region Serialized Fields

    [Header("References")]
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject startupMenu;
    [SerializeField] private Hangman game;

    [SerializeField] private TMP_InputField startupInputField;
    
    #endregion
    
    #region State Machine
    
    public enum MenuState
    {
        Start,
        Startup,
        Game
    }
    
    private MenuState state;

    public MenuState State
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
        Instance = this;
    }

    private void Start()
    {
        State = MenuState.Start;
    }

    public void TriggerMenu(int menuStateIndex)
    {
        State = (MenuState)menuStateIndex;
    }

    private void ChangeState()
    {
        startMenu.SetActive(state is MenuState.Start);
        startupMenu.SetActive(state is MenuState.Startup);
        game.gameObject.SetActive(state is MenuState.Game);

        if (state == MenuState.Game)
        {
            game.Init();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}