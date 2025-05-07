using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    
    public static PlayerManager SINGLETON { get; private set; }
    
    [SerializeField]private TMP_InputField player1Input;
    [SerializeField]private TMP_InputField player2Input;
    public string name1;
    public string name2;

    public List<string> playerNames = new List<string>();
    public Dictionary<string, List<string>> wordsFound = new();
    public Dictionary<string, List<string>> lettersFounds = new ();
    public string Score;
    
    void Awake()
    {
        if (SINGLETON != null)
        {
            Debug.LogError("Multiple Ui_Updater instances");
            Destroy(gameObject);
            return;
        }
        SINGLETON = this;
    }

    
    public void ValidateNames()
    {
        name1 = player1Input.text; 
        name2 = player2Input.text;

        if (!string.IsNullOrWhiteSpace(name1)) playerNames.Add(name1);
        if (!string.IsNullOrWhiteSpace(name2)) playerNames.Add(name2);

        Debug.Log("Players: " + string.Join(", ", playerNames));
    }
}