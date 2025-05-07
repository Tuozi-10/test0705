using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelectManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [Header("inputField")]
    [SerializeField] private TMP_InputField enterNameInputField;
    [SerializeField] private string defaultPlaceHolderText;
    [SerializeField] private TMP_Text placeHolderText;
    [Header("Players")]
    [SerializeField] private TMP_Text currentPlayerText;
    [SerializeField] private string J1name = "J1";
    [SerializeField] private string J2name = "J2";
    private bool isJ1turn = true;

    private void Start()
    {
        placeHolderText.text = defaultPlaceHolderText;
    }

    private void FixedUpdate()
    {
        if (isJ1turn)
        {
            currentPlayerText.text = J1name;
            currentPlayerText.color = Color.red;
        }
        else
        {
            currentPlayerText.text = J2name;
            currentPlayerText.color = Color.blue;
        }
    }
    
    public void ValidateButton()
    {
        if (enterNameInputField.text.Length > 0)
        {
            if (isJ1turn)
            {
                J1name = enterNameInputField.text;
                enterNameInputField.text = null;
                placeHolderText.text = defaultPlaceHolderText;
                placeHolderText.color = Color.grey;
                isJ1turn = false;
            }
            else
            {
                J2name = enterNameInputField.text;
                enterNameInputField = null;
                placeHolderText.text = ";P";
                isJ1turn = true;
                EndSelection();
            }
        }
        else
        {
            placeHolderText.text = "You need a name !";
            placeHolderText.color = Color.red;
        }
    }

    private void EndSelection()
    {
        
    }

    private void NextScene()
    {
        //set a fade out here
        if (nextSceneName == null) SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(2).name);
        else SceneManager.LoadScene(nextSceneName);
    }
}
