using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Manager
{
    public class PlayerManager : MonoBehaviour
    {
        #region Variable

        public Players players = new Players();

        public static PlayerManager instance;

        [SerializeField] private TMP_Text textPlayerTurn;
        [SerializeField] private TMP_Text textPlayerTurnInGame;

        [SerializeField] private TMP_InputField nameHolder;
        
        [SerializeField] private TMP_InputField worldHolder;
        
        [SerializeField] private TMP_InputField wordGuesseur;

        [SerializeField] private GameObject prefabsLetter;
        [SerializeField] private GameObject prefabsLetterParents;

        [SerializeField] private List<GameObject> listOfBodiesPart;

        public string wordToGuess;

        public List<TMP_Text> listOfLetterRef;

        public PlayerTurn currentPlayerTurn;
        
        public class Players
        {
            public string nameFirstPlayer;
            public string nameSecondPlayer;
        }

        public enum PlayerTurn
        {
            PlayerOne,
            PlayerSecond,
        }

        [SerializeField] private GameObject wordHolderGO;
        [SerializeField] private GameObject wordGuesseurGO;

        [SerializeField] private TMP_Text textEndWhoWin;

        private int countBeforeDeath;

        public string winner;
        #endregion

        #region Initialisation

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
            DisplayText(textPlayerTurn,"PLAYER TURN : FIRST, select a name");
            wordGuesseurGO.SetActive(false);
        }

        #endregion

        #region ChoosingNameScene

        public void ConfirmName()
        {
            if (nameHolder.text.Length < 1) return;
            
            switch (currentPlayerTurn)
            {
                case PlayerTurn.PlayerOne:
                    players.nameFirstPlayer = nameHolder.text;
                    DisplayText(textPlayerTurn,"PLAYER TURN : SECOND, select a name");
                    break;
                case PlayerTurn.PlayerSecond:
                    players.nameSecondPlayer = nameHolder.text;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            nameHolder.text = "";

            EndActualTurn();
            
            if (players is not { nameFirstPlayer: not null, nameSecondPlayer: not null }) return;
            var manager = GameManager.instance;
            if (manager != null) manager.CurrentGameState = GameManager.GameState.Game;
            wordHolderGO.SetActive(true);
            DisplayText(textPlayerTurnInGame,$"PLAYER TURN : {players.nameFirstPlayer}, Choose a word");
                
        }

        #endregion
        
        private void DisplayText(TMP_Text textTarget,string textInTarget)
        {
            textTarget.text = textInTarget;
        }
        
        private void EndActualTurn()
        {
            switch (currentPlayerTurn)
            {
                case PlayerTurn.PlayerOne:
                    currentPlayerTurn = PlayerTurn.PlayerSecond;
                    break;
                case PlayerTurn.PlayerSecond:
                    currentPlayerTurn = PlayerTurn.PlayerOne;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        #region ChoosingTheWord

        public void ChooseAWord()
        {
            if (worldHolder.text.Length < 1) return;
            wordToGuess = worldHolder.text;
            
            if (!wordToGuess.All(char.IsLetter))
            {
                return;
            }

            wordToGuess = wordToGuess.ToLower();

            wordHolderGO.SetActive(false);
            currentPlayerTurn = PlayerTurn.PlayerSecond;
            DisplayText(textPlayerTurnInGame,$"PLAYER TURN : {players.nameSecondPlayer}");
            wordGuesseurGO.SetActive(true);
            SetupWord();
        }

        #endregion
        
        #region GamePlay

        private void SetupWord()
        {
            foreach (char letter in wordToGuess)
            {
                TMP_Text letterToAdd = Instantiate(prefabsLetter, prefabsLetterParents.transform).GetComponent<TMP_Text>();
                letterToAdd.alpha = 0;
                letterToAdd.text = (letter).ToString();
                listOfLetterRef.Add(letterToAdd);
            }

            foreach (var bodiesPart in listOfBodiesPart)
            {
                bodiesPart.SetActive(false);
            }
        }

        private void BadGuess()
        {
            countBeforeDeath++;
            listOfBodiesPart[countBeforeDeath - 1].SetActive(true);
            if (countBeforeDeath >= 6)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            EndGame(false);
        }
        
        private bool DisplayWord(char targetLetter)
        {
            bool isLetterGood = false;
            foreach (var letter in listOfLetterRef)
            {
                if (letter.text[0] == targetLetter)
                {
                    letter.alpha = 1;
                    isLetterGood = true;
                }
            }

            return isLetterGood;
        }

        private void EndGame(bool guesseurWin)
        {
            textEndWhoWin.text = guesseurWin ? $"{players.nameSecondPlayer} WON" : $"{players.nameFirstPlayer} WON";
            
            SaveManager.instance.Save();

            GameManager.instance.CurrentGameState = GameManager.GameState.End;
            
            winner = guesseurWin ? players.nameSecondPlayer : players.nameFirstPlayer;
    }
        
        private void DisplayWorldWin()
        {
            foreach (var letter in listOfLetterRef)
            {
                letter.alpha = 1;
            }
            EndGame(true);
        }

        private bool CheckForWin()
        {
            bool didWin = true;
            foreach (var letter in listOfLetterRef)
            {
                if (letter.alpha == 0)
                {
                    didWin = false;
                }
            }

            return didWin;
        }
        
        public void GuessWord()
        {
            switch (wordGuesseur.text.Length)
            {
                case 1:
                    bool goodGuess = DisplayWord(wordGuesseur.text[0]);
                    if (CheckForWin())
                    {
                        DisplayWorldWin();
                    }

                    if (!goodGuess)
                    {
                        BadGuess();
                    }
                    
                    break;
                case > 1:
                    if (wordGuesseur.text == wordToGuess)
                    {
                        DisplayWorldWin();
                    }
                    else
                    {
                        BadGuess();   
                    }
                    break;
                default:
                    return;
            }

            wordGuesseur.text = "";
        }

        #endregion
        
        
    }
}