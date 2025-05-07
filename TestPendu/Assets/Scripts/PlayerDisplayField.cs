using TMPro;
using UnityEngine;

public class PlayerDisplayField : MonoBehaviour
{
    [SerializeField] private TMP_Text playerId;
    [SerializeField] private TMP_Text playerName;

    public void SetInformation(PlayerController.Players id, string nameToDisplay)
    {
        playerId.text = id.ToString();
        playerName.text = nameToDisplay;
    }
}