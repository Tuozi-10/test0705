using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //WTS = word to search
    public string WTS;
    public string J1name = "J1";
    public string J2name = "J2";
    public bool isJ1turn = true;
    
    public static GameManager Instance;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

}
