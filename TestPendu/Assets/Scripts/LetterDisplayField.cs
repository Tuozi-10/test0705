using TMPro;
using UnityEngine;

public class LetterDisplayField : MonoBehaviour
{
    [SerializeField] private TMP_Text letterText;

    public void SetLetter(char letter)
    {
        letterText.text = letter.ToString();
    }
}