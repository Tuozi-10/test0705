using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiveGameManagerReferences : MonoBehaviour
{
    [SerializeField] private TMP_Text actualText;
    [SerializeField] private GameObject[] hangedMan;
 
    private void Awake()
    {
        GameManager.INSTANCE.actualText = actualText;
        GameManager.INSTANCE.hangedMan = hangedMan;
        GameManager.INSTANCE.StartGame();
    }
}
