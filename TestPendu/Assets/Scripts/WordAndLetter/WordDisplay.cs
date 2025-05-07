using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordDisplay : MonoBehaviour
{
    [SerializeField] private LetterSlot letterSlotPrefab;
    [SerializeField] private TMP_Text wordDisplayText;
    private void Start()
    {
        WordDisplayInit(GameManager.Instance.Word);
    }

    private void WordDisplayInit(string word)
    {
        wordDisplayText.text = word;
        wordDisplayText.alpha = 0f;
        foreach (char character in word)
        {
            char characterMaj = char.ToUpper(character);
            LetterSlot letterSlot = Instantiate(letterSlotPrefab, transform);
            letterSlot.SetLetter(characterMaj);
            GameManager.Instance.LetterSlots.Add(letterSlot);
        }
    }
}
