using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuWinnerBoard : MonoBehaviour
{
    [SerializeField] private TMP_Text[] tabText;
    private void Awake()
    {
        for (int i = 0; i < tabText.Length; i++)
        {
            string key1 = "winner" + (i + 1); 
            string key2 = "looser" + (i + 1); 
            string key3 = "word" + (i + 1); 
            tabText[i].text = PlayerPrefs.GetString(key1) + " win against " + PlayerPrefs.GetString(key2) +
                              " with the word " + PlayerPrefs.GetString(key3);
        }
    }
}
