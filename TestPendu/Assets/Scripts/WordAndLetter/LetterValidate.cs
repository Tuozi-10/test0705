using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterValidate : MonoBehaviour
{
    [SerializeField]private TMP_InputField inputField;
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValidateInput += ValidateLetterOnly;
    }
    private char ValidateLetterOnly(string text, int charIndex, char addedChar)
    {
        char upper = char.ToUpper(addedChar);
        return (upper >= 'A' && upper <= 'Z') ? addedChar : '\0';
    }
}
