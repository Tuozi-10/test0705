
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SelectScreenManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    private string currentSelectPhase = "nameSelect";
    
    [Header("inputField")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private string nameSelectPlaceHolderStr;
    [SerializeField] private string wordToSearchPlaceHolderStr;
    [SerializeField] private TMP_Text placeHolderText;
    
    [Header("Players")]
    [SerializeField] private TMP_Text currentPlayerText;

    private void Start()
    {
        SetScene(currentSelectPhase);
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.isJ1turn)
        {
            currentPlayerText.text = GameManager.Instance.J1name;
            currentPlayerText.color = Color.red;
        }
        else
        {
            currentPlayerText.text = GameManager.Instance.J2name;
            currentPlayerText.color = Color.blue;
        }
    }
    
    private void NextScene()
    {
        if (nextSceneName == null) SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(2).name);
        else SceneManager.LoadScene(nextSceneName);
    }
    
    private void SetScene(string phase)
    {
        if (phase == "nameSelect") placeHolderText.text = nameSelectPlaceHolderStr;
        if (phase == "wordSelect") placeHolderText.text = wordToSearchPlaceHolderStr;
        
        inputField.text = "";
        placeHolderText.color = Color.grey;
    }
    
    public void OnValidate()
    {
        if (currentSelectPhase == "nameSelect")
        {
            if (inputField.text.Length > 0)
            {
                if (GameManager.Instance.isJ1turn)
                {
                    GameManager.Instance.J1name = inputField.text;
                    SetScene(currentSelectPhase);
                    GameManager.Instance.isJ1turn = false;
                }
                else
                {
                    GameManager.Instance.J2name = inputField.text;
                    currentSelectPhase = "wordSelect";
                    SetScene(currentSelectPhase);
                    GameManager.Instance.isJ1turn = true;
                }
            }
            else
            {
                placeHolderText.text = "You need a name !";
                placeHolderText.color = Color.red;
            }
        }
        else if (currentSelectPhase == "wordSelect")
        {
            if (inputField.text.Length > 4 && inputField.text.Length < 12)
            {
                GameManager.Instance.wordToSearch = inputField.text;
                NextScene();
            }
            else
            {
                placeHolderText.text = "4min, 12max characters";
                placeHolderText.color = Color.red;
            }
        }
    }
}
