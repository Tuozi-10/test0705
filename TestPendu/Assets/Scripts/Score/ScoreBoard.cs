using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private ScoreDisplay scoreDisplayPrefab;
    [SerializeField] private int maxScoreDisplay = 3;

    public void initScore(int numberScoreDisplay)
    {
        foreach (Transform children in container)
        {
            Destroy(children.gameObject);
        }

        for (int i = 0; i < numberScoreDisplay; i++)
        {
            ScoreDisplay scoreDisplay = Instantiate(scoreDisplayPrefab, container);
            
            scoreDisplay.init(i);
        }
    }
    
    public void ShowScoreDisplay()
    {
        transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutBack);
        initScore(maxScoreDisplay);
        
    }

    public void HideScoreDisplay()
    {
        transform.DOScale(Vector3.zero, 0.15f).SetEase(Ease.InBack);
    }
}
