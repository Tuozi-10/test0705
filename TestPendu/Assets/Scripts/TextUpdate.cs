using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string textBeforePlayerName = "";
    [SerializeField] private string textAfterPlayerName = "";
    [SerializeField] private int numberPlayer;

    private void Awake()
    {
        string name;
        if (numberPlayer == 1)
        {
            name = GameManager.INSTANCE.playerOneName;
        }
        else
        {
            name = GameManager.INSTANCE.playerTwoName;
        }

        text.text = textBeforePlayerName + " " + name + " " + textAfterPlayerName;
    }
}
