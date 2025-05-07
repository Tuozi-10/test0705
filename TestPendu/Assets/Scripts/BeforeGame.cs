using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeforeGame : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameManager GM;
    [SerializeField] private PenduManager PM;
    [SerializeField] private GameObject specialChar;
    private bool J1NameChoosed = false;
    private bool J2NameChoosed = false;
    void Start()
    {
        text.text = "Joueur1, entrez votre nom.";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inputField.text.Length > 1)
            {
                if (!J1NameChoosed)
                {
                    changeJ1Name();
                }
                else if (!J2NameChoosed)
                {
                    changeJ2Name();
                }
                else
                {
                    chooseWord();
                }
            }

            inputField.text = null;
        }
    }

    void changeJ1Name()
    {
        GM.J1Name = inputField.text;
        J1NameChoosed = true;
        text.text = "Joueur2, entrez votre nom.";
    }
    void changeJ2Name()
    {
        GM.J2Name = inputField.text;
        J2NameChoosed = true;
        text.text = GM.J1Name + ", choisissez le mot à faire deviner.";
    }
    void chooseWord()
    {
        for (int i = 0; i < inputField.text.Length; i++)
        {
            if (!Char.IsLetter(inputField.text[i]))
            {
                specialChar.SetActive(true);
                return;
            }
        }
        specialChar.SetActive(false);
        GM.WordToGuess = inputField.text;
        J1NameChoosed = false;
        J2NameChoosed = false;
        text.text = "Joueur1, entrez votre nom.";
        GM.BeforeGameObj.SetActive(false);
        GM.PenduGameObj.SetActive(true);
        PM.StartGame();
    }
}
