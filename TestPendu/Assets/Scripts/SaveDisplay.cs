using TMPro;
using UnityEngine;

public class SaveDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text player1NameField;
    [SerializeField] private TMP_Text player2NameField;
    [SerializeField] private TMP_Text wordField;
    [SerializeField] private GameObject player1Crown;
    [SerializeField] private GameObject player2Crown;

    public void Init(SaveData data)
    {
        player1NameField.text = data.Player1Name;
        player2NameField.text = data.Player2Name;
        wordField.text = data.Word;
        (data.Winner == 1 ? player1Crown : player2Crown).SetActive(true);
    }
}
