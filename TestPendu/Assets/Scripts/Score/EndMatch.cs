using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndMatch : MonoBehaviour
{
    // attention à garder la meme nomenclature dans le projet
    // Là tu as des private avec majuscule au début, d'autres sans, c'est important de garder la même logique partout
    // Je te conseille par exemple,
    // les private tu commences par m_ ou _,
    // les publics la première lettre en minuscule,
    // et les consts premiere lettre en majuscule
    [SerializeField] private TMP_Text WordTxt;
    [SerializeField] private TMP_Text WhoIsWinnerTxt;
    [SerializeField] private Player player;
    [SerializeField] private List<Sprite> headSprites;
    [SerializeField] private Image headSprite;
    [SerializeField] private WinManager winManager;

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (player.asLose || player.asWin)
        {
            SetWin();
            ShowEndPanel();
            winManager.SaveMatchResult(); 
        }
    }

    private void SetWin()
    {
        WordTxt.text = $"Le mot était : {GameManager.Instance.Word} " ;
        if (player.asWin)
        {
            headSprite.sprite = headSprites[0];
            WhoIsWinnerTxt.text = $"{GameManager.Instance.PlayerTwo} à trouver le mot.";
        }
        else if (player.asLose)
        {
            headSprite.sprite = headSprites[1];
            WhoIsWinnerTxt.text = $"{GameManager.Instance.PlayerTwo} est mort pendu.";
        }
    }

    private void ShowEndPanel()
    {
        transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutBack);
    }
}
