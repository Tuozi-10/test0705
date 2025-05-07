using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
            if (isParametersOpen)
            {
                panelParameters.SetActive(true);
                isParametersOpen = false;
            }
            else
            {
                panelParameters.SetActive(false);
                isParametersOpen = true;
            }
           
        }

        #endregion
        
        public void LauchGameNameSelection() => CurrentGameState = GameState.NameSelection;

    }
}
