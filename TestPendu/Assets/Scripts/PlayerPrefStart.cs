using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefStart : MonoBehaviour
{
    
    

    private void Start()
    {
        
        //-------------- Saved Names 
        
        if (!PlayerPrefs.HasKey("PlayerName1-1"))
        {
            PlayerPrefs.SetString("PlayerName1-1",null);
        }
        
        if (!PlayerPrefs.HasKey("PlayerName1-2"))
        {
            PlayerPrefs.SetString("PlayerName1-2",null);
        }
        
        if (!PlayerPrefs.HasKey("PlayerName1-3"))
        {
            PlayerPrefs.SetString("PlayerName1-3",null);
        }
        
        //----
        
        if (!PlayerPrefs.HasKey("PlayerName2-1"))
        {
            PlayerPrefs.SetString("PlayerName2-1",null);
        }
        
        if (!PlayerPrefs.HasKey("PlayerName2-2"))
        {
            PlayerPrefs.SetString("PlayerName2-2",null);
        }
        
        if (!PlayerPrefs.HasKey("PlayerName2-3"))
        {
            PlayerPrefs.SetString("PlayerName2-3",null);
        }
        
        //-------------- Saved Scores
        
        if (!PlayerPrefs.HasKey("Score1"))
        {
            PlayerPrefs.SetString("Score1",null);
        }
        
        if (!PlayerPrefs.HasKey("Score2"))
        {
            PlayerPrefs.SetString("Score2",null);
        }
        
        if (!PlayerPrefs.HasKey("Score3"))
        {
            PlayerPrefs.SetString("Score3",null);
        }
        
        //----------- Save all
        
        PlayerPrefs.Save();
        Debug.Log("PlayerPref has been saved");
        
        
    }
}
