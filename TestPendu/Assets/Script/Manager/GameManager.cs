using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

namespace Script.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Variables

        public static GameManager instance;

        public bool isParametersOpen;

        private GameState currentGameState;

        [SerializeField] private List<GameObject> listOfEveryCanva;

        [SerializeField] private GameObject panelParameters;
        private RectTransform panelParametersRectTransform;

        [SerializeField] private TMP_Text score1;
        [SerializeField] private TMP_Text score2;
        [SerializeField] private TMP_Text score3;
        
        #endregion
        
        public enum GameState
        {
            Menu,
            NameSelection,
            Game,
            End,
        }
        

        #region INITIALISATION

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            InitGameManager();
        }

        private void InitGameManager()
        {
            CurrentGameState = GameState.Menu;
            panelParameters.SetActive(false);
            panelParametersRectTransform = panelParameters.GetComponent<RectTransform>();
        }

        #endregion

        public GameState CurrentGameState
        {
            get => currentGameState;

            set
            {
                currentGameState = value;
                OpenCanvaTarget(listOfEveryCanva[(int)CurrentGameState]);

                if (currentGameState == GameState.NameSelection || currentGameState == GameState.Game)
                {
                    var manager = PlayerManager.instance;
                    if (manager != null) manager.currentPlayerTurn = PlayerManager.PlayerTurn.PlayerOne;
                }
            }
        }

        #region UI

        public void UpdateEntireUiScore()
        {
            if (PlayerPrefs.HasKey("word_1"))
            {
                UpdateUiScore(score1,"word_1","namePlayer1_1","namePlayer2_1","win_1");
            }
            if (PlayerPrefs.HasKey("word_2"))
            {
                UpdateUiScore(score2,"word_2","namePlayer1_2","namePlayer2_2","win_2");
            }
            if (PlayerPrefs.HasKey("word_3"))
            {
                UpdateUiScore(score3,"word_3","namePlayer1_3","namePlayer2_3","win_3");
            }
            
            
            
        }
        
        private void UpdateUiScore(TMP_Text targetText, string word,string player1, string player2, string winner)
        {
            targetText.text = $"Le mot choisi est " +
                              $"{PlayerPrefs.GetString(word)}, les deux players sont " +
                              $"{PlayerPrefs.GetString(player1)} " +
                              $"{PlayerPrefs.GetString(player2)} le winner est " + winner;
        }
        
        private void OpenCanvaTarget(GameObject canvaTarget)
        {
            foreach (var canva in listOfEveryCanva)
            {
                canva.SetActive(false);
            }
            canvaTarget.SetActive(true);
        }

        public void ToggleSwitchParameters()
        {
            float slideDuration = 0.5f;
            Vector2 onScreenPosition = Vector2.zero;             
            Vector2 offScreenPosition = new Vector2(2000, 0);    

            if (isParametersOpen)
            {
               
                panelParametersRectTransform.DOAnchorPos(offScreenPosition, slideDuration).SetEase(Ease.InBack)
                    .OnComplete(() => panelParameters.SetActive(false));
                isParametersOpen = false;
            }
            else
            {
                panelParameters.SetActive(true);
                panelParametersRectTransform.anchoredPosition = offScreenPosition;
                panelParametersRectTransform.DOAnchorPos(onScreenPosition, slideDuration).SetEase(Ease.OutBack);
                isParametersOpen = true;
            }
        }

        #endregion
        
        public void LauchGameNameSelection() => CurrentGameState = GameState.NameSelection;

        public void ReloadActualScene()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }
    }
}
