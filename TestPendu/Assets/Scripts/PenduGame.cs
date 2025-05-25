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


    private string displayWord = string.Empty;
    
    private int maxFail = 6;
    private int failCount = 0;
    
    private List<char> guessedLetters = new List<char>();
    
    private const string ResultsKey = "GameResults";
    
    public string player1NameString { get; private set; } = string.Empty;

    public string player2NameString { get; private set; } = string.Empty;

    public string setterNameString { get; private set; } = string.Empty;

    public string GuesserNameString { get; private set; } = string.Empty;

    public string SecretWord { get; private set; } = string.Empty;

    void Awake()
    {
        enabled = false;
    }

    // ton enable est beaucoup trop velu, hésite pas à le couper en diverses fonctions de reset ( ResetTexts, ResetGameObjects, .... )
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
        
        player1NameString   = string.Empty;
        player2NameString   = string.Empty;
        setterNameString    = string.Empty;
        GuesserNameString   = string.Empty;
        SecretWord    = string.Empty;
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

    // ton update ne devrait pas avoir de logique, seulement appeller des fonctions 
    // Là par exemple, pour améliorer, tu pourrais avoir une fonction UpdateState, et des sous fonctions UpdateStateEnterName, etc
    void Update()
    {
        switch (state)
        {
            //on entre le nom du J1 si il est >0 char
            case GameState.EnterName1:
                if (Input.GetKeyDown(KeyCode.Return) && inputField.text.Length > 0) 
                {
                    player1NameString = inputField.text;
                    inputField.text = string.Empty;
                    
                    //meme chose pour le j2
                    promptText.text = "Entrez le nom du Joueur 2 :";
                    state = GameState.EnterName2;
                }
                break;

            case GameState.EnterName2:
                if (Input.GetKeyDown(KeyCode.Return) && inputField.text.Length > 0)
                {
                    player2NameString = inputField.text;
                    inputField.text = string.Empty;
                    promptText.text = $"Qui entre le mot ? (1 = {player1NameString}, 2 = {player2NameString})";
                    state = GameState.ChooseRole;
                }
                break;

            //on choisi le role des joueur 1 et 2
            case GameState.ChooseRole:
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    setterNameString = player1NameString;
                    GuesserNameString = player2NameString;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                {
                    setterNameString = player2NameString;
                    GuesserNameString = player1NameString;
                }
                if (!string.IsNullOrEmpty(setterNameString)) //le setter entre le mot secret
                {
                    promptText.text = $"({setterNameString}) Entrez le mot :";
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
                        SecretWord = rawInput.ToUpper(); // transformer en majuscules
                        displayWord = new string('_', SecretWord.Length); // remplacer chaque caractère par '_'
                        wordDisplayText.text = displayWord;
                        failCount = 0;
                        errorText.text = $"Erreurs : {failCount}/{maxFail}";

                        promptText.text = $"{GuesserNameString}, devinez des lettres :";
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
                            if (SecretWord.Contains(letter.ToString()))
                            {
                                // relever toute les occurrence de la lettre
                                char[] chars = displayWord.ToCharArray();
                                for (int i = 0; i < SecretWord.Length; i++)
                                {
                                    if (SecretWord[i] == letter)
                                    {
                                        chars[i] = letter;
                                    }
                                        
                                }
                                    

                                displayWord = new string(chars);
                                wordDisplayText.text = displayWord;

                                // Vérifier la victoire
                                if (!displayWord.Contains("_"))
                                {
                                    promptText.text = $"{GuesserNameString} a gagné, quel bg !";
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
                                    promptText.text = $"vous etes pas très fort. Le mot était {SecretWord}.";
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
    
    // pareil, là tu devrais avoir des sous fonctions pour sauvegarder, etc,
    // pour pouvoir potentiellement aussi l'appeller d'ailleurs si besoin et éviter du dupplicata 
    private void OnGameEnd(bool guesserWon) //on prend par défaut le guesser comme codition de victoire
    {
        if (GameManager.instance.movedToA == false)
        {
            GameManager.instance.DisplayScore();
        }
        
        string msg = guesserWon ? $"{GuesserNameString} a trouver le mot de {setterNameString} : {SecretWord}" : $"{GuesserNameString} n'a pas trouver le mot de {setterNameString} : {SecretWord}";

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



    // la fête commence ?
    public void StartParty()
    {
        enabled = true;
    }
}