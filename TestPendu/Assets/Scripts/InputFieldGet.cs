using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class InputFieldGet : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    
    public void ChangeNames(int number)
    {
        GameManager.INSTANCE.SetPlayerName(number, inputField.text);
    }

    public void ChangeWord()
    {
        GameManager.INSTANCE.SetWord(Format(inputField.text));
    }

    public void Play()
    {
        if (inputField.text != "")
        {
            GameManager.INSTANCE.Play(Convert.ToChar(Format(inputField.text)));
        }
    }
    
    private string Format(string input)
    {
        string lower = input.ToLower();
        string normalized = lower.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();
        foreach (char c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }
        string withoutAccents = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        string onlyLetters = Regex.Replace(withoutAccents, "[^a-z]", "");
        return onlyLetters;
    }
}

