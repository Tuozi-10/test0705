using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerSelectManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    [Header("PlayerNameSelect------------------------------------------")] 
    //PNS = playerNameSelect
    [Header("inputField")] 
    [SerializeField] private GameObject PNS_panel; 
    [SerializeField] private TMP_InputField PNS_InputField;
    [SerializeField] private string PNS_defaultPlaceHolderText;
    [SerializeField] private TMP_Text PNS_placeHolderText;
    [Header("Players")]
    [SerializeField] private TMP_Text currentPlayerText;

    [Header("WordToSearchSelect----------------------------------------")]
    //WTSS = word to search
    [SerializeField] GameObject WTSS_panel;
    [SerializeField] TMP_InputField WTSS_inputField;
    [SerializeField] private TMP_Text WTSS_placeHolderText;

    private void Start()
    {
        PNS_placeHolderText.text = PNS_defaultPlaceHolderText;
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
        //set a fade out here
        if (nextSceneName == null) SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(2).name);
        else SceneManager.LoadScene(nextSceneName);
    }
    
    private void EndPlayerSelect()
    {
        PNS_panel.SetActive(false);
        WTSS_panel.SetActive(true);
    }

    
    //PNS section ------------------------------------------
    
    public void PNS_ValidateButton()
    {
        if (PNS_InputField.text.Length > 0)
        {
            if (GameManager.Instance.isJ1turn)
            {
                GameManager.Instance.J1name = PNS_InputField.text;
                PNS_InputField.text = null;
                PNS_placeHolderText.text = PNS_defaultPlaceHolderText;
                PNS_placeHolderText.color = Color.grey;
                GameManager.Instance.isJ1turn = false;
            }
            else
            {
                GameManager.Instance.J2name = PNS_InputField.text;
                PNS_InputField = null;
                PNS_placeHolderText.text = ";P";
                GameManager.Instance.isJ1turn = true;
                EndPlayerSelect();
            }
        }
        else
        {
            PNS_placeHolderText.text = "You need a name !";
            PNS_placeHolderText.color = Color.red;
        }
        Debug.Log("button fucking pressed");
    }
    
    //WTSS section ------------------------------------------

    public void WTSS_ValidateButton()
    {
        if (WTSS_inputField.text.Length > 4 && WTSS_inputField.text.Length < 12)
        {
            GameManager.Instance.WTS = WTSS_inputField.text;
            NextScene();
        }
        else
        {
            WTSS_placeHolderText.text = "only between 4 and 12 characters !";
            WTSS_placeHolderText.color = Color.red;
        }
    }
}
