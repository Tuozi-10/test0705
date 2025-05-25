using TMPro;
using UnityEngine;

// j'avoue être un peu perdu entre les 4 classes de Playerxxxx que tu as, le playerManager qui gère des inputs,
// le player controller qui n'est qu'un stockage, celui là qui au final n'a pas vraiment de raison d'être un monobehaviour
// c'est un peu confus, j'ai l'impression que tu as été à court de temps, et que tu as galopé pour faire rentrer le plus possible pour le rendu :/

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