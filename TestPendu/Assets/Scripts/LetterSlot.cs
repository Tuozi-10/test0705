using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterSlot : MonoBehaviour
{
    public TMP_Text letterText;
    public Image coverImage;

    public char Letter { 
        get; 
        private set;
        
    }

    public void SetLetter(char c)
    {
        Letter = char.ToUpper(c);
        letterText.text = Letter.ToString();
        coverImage.enabled = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void Reveal()
    {
        coverImage.enabled = false;
    }
}
