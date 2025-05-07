using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI; 
using TMPro;

public class PenduGame : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text promptText;
    [SerializeField] private TMP_Text wordDisplayText;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text resultText;
    
    [SerializeField] private Image penduDisplay;       // L’Image UI à mettre à jour
    [SerializeField] private List<Sprite> penduSprites;
    
    private enum GameState 
    { 
        EnterName1,
        EnterName2, 
        ChooseRole, 
        EnterWord, 
        Guessing, 
        Ended
        
    }
    private GameState state;

 
    private string player1Name = string.Empty;
    private string player2Name = string.Empty;
    private string setterName = string.Empty;
    private string guesserName = string.Empty;
    private string secretWord = string.Empty;
    private string displayWord = string.Empty;
    
    private int maxFail = 6;
    private int failCount = 0;
    
    private List<char> guessedLetters = new List<char>();
    
    private const string ResultsKey = "GameResults";
    
    public string player1NameString
    {
        get { return player1Name; }
    }
    
    public string player2NameString
    {
        get { return player2Name; }
    }
    
    public string setterNameString
    {
        get { return setterName; }
    }
    
    public string GuesserNameString
    {
        get { return guesserName; }
    }

    public string SecretWord  => secretWord;
    void Awake()
    {
        enabled = false;
    }

    void OnEnable()
    {
        inputField.gameObject.SetActive(true);
        promptText.gameObject.SetActive(true);
        wordDisplayText.gameObject.SetActive(true);
        errorText.gameObject.SetActive(true);
        penduDisplay.gameObject.SetActive(true);
        
        UpdateHangmanVisual(); 
        
       //on set ou reset tout les text vide
        state = GameState.EnterName1;
        
        player1Name   = string.Empty;
        player2Name   = string.Empty;
        setterName    = string.Empty;
        guesserName   = string.Empty;
        secretWord    = string.Empty;
        displayWord   = string.Empty;
        
        failCount = 0;
        guessedLetters.Clear();

        promptText.text = "Entrer le nom du joueur 1 :";
        inputField.contentType = TMP_InputField.ContentType.Standard; //passe l'input field en standar dans le cas oui il est rester en mode password
        inputField.text = string.Empty;
        inputField.gameObject.SetActive(true);
        inputField.ActivateInputField();

        wordDisplayText.text = string.Empty;
        errorText.text = string.Empty;

        // on cherche les donné de gameresult et on met a jour son texte
        string raw = PlayerPrefs.GetString(ResultsKey, "");
        if (!string.IsNullOrEmpty(raw))//si il est vide
        {
            var entries = raw.Split(new[] { "|" }, System.StringSplitOptions.RemoveEmptyEntries);
            var ligne = new System.Text.StringBuilder();
            ligne.AppendLine("Historique des parties :");
            foreach (var line in entries)
                ligne.AppendLine(line);
            resultText.text = ligne.ToString();
        }
        else
        {
            resultText.text = "";
        }

        
        
    }

    void Update()
    {
        switch (state)
        {
            //on entre le nom du J1 si il est >0 char
            case GameState.EnterName1:
                if (Input.GetKeyDown(KeyCode.Return) && inputField.text.Length > 0) 
                {
                    player1Name = inputField.text;
                    inputField.text = string.Empty;
                    
                    //meme chose pour le j2
                    promptText.text = "Entrez le nom du Joueur 2 :";
                    state = GameState.EnterName2;
                }
                break;

            case GameState.EnterName2:
                if (Input.GetKeyDown(KeyCode.Return) && inputField.text.Length > 0)
                {
                    player2Name = inputField.text;
                    inputField.text = string.Empty;
                    promptText.text = $"Qui entre le mot ? (1 = {player1Name}, 2 = {player2Name})";
                    state = GameState.ChooseRole;
                }
                break;

            //on choisi le role des joueur 1 et 2
            case GameState.ChooseRole:
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    setterName = player1Name;
                    guesserName = player2Name;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                {
                    setterName = player2Name;
                    guesserName = player1Name;
                }
                if (!string.IsNullOrEmpty(setterName)) //le setter entre le mot secret
                {
                    promptText.text = $"({setterName}) Entrez le mot :";
                    inputField.contentType = TMP_InputField.ContentType.Password;//on le cache grace a password
                    inputField.text = string.Empty;
                    inputField.ActivateInputField();
                    state = GameState.EnterWord;
                }
                break;

            case GameState.EnterWord:
                if (Input.GetKeyDown(KeyCode.Return) && inputField.text.Length > 0)
                {
                    // valider que le mot ne contient que des lettres
                    string rawInput = inputField.text;
                    if (System.Text.RegularExpressions.Regex.IsMatch(rawInput, "^[A-Z]+$"))
                    {
                        secretWord = rawInput.ToUpper(); // transformer en majuscules
                        displayWord = new string('_', secretWord.Length); // remplacer chaque caractère par '_'
                        wordDisplayText.text = displayWord;
                        failCount = 0;
                        errorText.text = $"Erreurs : {failCount}/{maxFail}";

                        promptText.text = $"{guesserName}, devinez des lettres :";
                        inputField.gameObject.SetActive(false);
                        state = GameState.Guessing;
                    }
                    else
                    {
                        // message d'erreur si caractères non alphabétiques détectés
                        promptText.text = "Entrée seulement des lettres majuscules.";
                        inputField.text = string.Empty;
                        inputField.ActivateInputField();
                    }
                }
                break;

            case GameState.Guessing:
                if (Input.anyKeyDown)
                {
                    // Utiliser Input.inputString pour le claiver azerty
                    string inputChars = Input.inputString;
                    foreach (char rawChar in inputChars)
                    {
                        if (char.IsLetter(rawChar))
                        {
                            char letter = char.ToUpper(rawChar);
                            if (guessedLetters.Contains(letter))
                                continue;

                            guessedLetters.Add(letter); // enregistrer la tentative
                            if (secretWord.Contains(letter.ToString()))
                            {
                                // relever toute les occurrence de la lettre
                                char[] chars = displayWord.ToCharArray();
                                for (int i = 0; i < secretWord.Length; i++)
                                {
                                    if (secretWord[i] == letter)
                                    {
                                        chars[i] = letter;
                                    }
                                        
                                }
                                    

                                displayWord = new string(chars);
                                wordDisplayText.text = displayWord;

                                // Vérifier la victoire
                                if (!displayWord.Contains("_"))
                                {
                                    promptText.text = $"{guesserName} a gagné, quel bg !";
                                    state = GameState.Ended;
                                    if (GameManager.instance.movedToA == false)
                                    {
                                        GameManager.instance.DisplayScore();
                                    }
                                    OnGameEnd(true);
                                }
                            }
                            else
                            {
                                // erreur
                                failCount++;
                                UpdateHangmanVisual(); 
                                errorText.text = $"erreurs : {failCount}/{maxFail}";
                                
                                // verifier la défaite
                                if (failCount >= maxFail)
                                {
                                    promptText.text = $"vous etes pas très fort. Le mot était {secretWord}.";
                                    state = GameState.Ended;
                                    OnGameEnd(false);
                                }
                                
                                errorText.rectTransform.DOShakeAnchorPos(0.5f, new Vector2(10f, 10f), 20,90f);
                            }
                        }
                    }
                }
                break;
               

            case GameState.Ended:
                
                //on ne fais rien en attente d'un choisi du joueur dans le gamemanager
                break;
        }
        
        
    }

    private void UpdateHangmanVisual()
    {
        if (penduSprites != null && penduDisplay != null && failCount < penduSprites.Count)
        {
            penduDisplay.sprite = penduSprites[failCount];
        }
    }
    
    private void OnGameEnd(bool guesserWon) //on prend par défaut le guesser comme codition de victoire
    {
        if (GameManager.instance.movedToA == false)
        {
            GameManager.instance.DisplayScore();
        }
        
        string msg = guesserWon ? $"{guesserName} a trouver le mot de {setterName} : {secretWord}" : $"{guesserName} n'a pas trouver le mot de {setterName} : {secretWord}";

        // on recupper gameresult
        string raw = PlayerPrefs.GetString(ResultsKey, "");
        var list = new List<string>();
        if (!string.IsNullOrEmpty(raw))
        {
            list.AddRange(raw.Split(new[] { "|" }, System.StringSplitOptions.None));
        }
            

        //saugearde
        list.Add(msg);
        PlayerPrefs.SetString(ResultsKey, string.Join("|", list));
        PlayerPrefs.Save();

        // mise a jour du texte
        var ligne = new System.Text.StringBuilder();
        ligne.AppendLine("Historique des parties :");
        foreach (var line in list)
        {
            ligne.AppendLine(line);
        }
            
        resultText.text = ligne.ToString();

        // Notifier le GameManager pour le score
        if (GameManager.instance != null)
        {
            GameManager.instance.EndGame(guesserWon);
        }
            

        enabled = false;
    
    }



    public void StartParty()
    {
        enabled = true;
    }
}