using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class UpdateCarreer : MonoBehaviour
{
    public RectTransform m_rect;
    public TextMeshProUGUI[] text;
    public bool isExit = false;


    private void Start()
    {
       
    }

    private void Update()
    {
        UpdateCareer();
    }
    

    public void UpdateCareer()
    {
        for (int i = 0; i < text.Length; i++)
        {
            string winner = PlayerPrefs.GetString($"winner_partie{i + 1}");
            string looser = PlayerPrefs.GetString($"looser_partie{i + 1}");
            string mot = PlayerPrefs.GetString($"mot_partie{i + 1}");
            
            text[i].text = $"{winner} a gagné contre {looser} sur le mot {mot}";
        }
    }
}
