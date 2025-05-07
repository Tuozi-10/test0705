using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Phase1UI : MonoBehaviour
{
    public RectTransform helpMenuRect;
    private bool _helpMenu = false;
    
    public GameObject scoreMenu;
    
    [Header("Canvas links")]
    public GameObject phase1Canva;
    public GameObject phase2Canva;
    
    
    
    
    public void OpenCloseHelpMenu()
    {
        if (_helpMenu == false)
        {
            _helpMenu = true;

            helpMenuRect.DOAnchorPosX(-460, 1, false).SetEase(Ease.OutBounce);
        }
        else if (_helpMenu == true)
        {

            _helpMenu = false;

            helpMenuRect.DOAnchorPosX(0, 1, false).SetEase(Ease.OutBounce);
            
        }
    }
    
    public void OpenCloseScoreMenu()
    {
        if (scoreMenu.activeSelf == false)
        {
            scoreMenu.SetActive(true);
        }
        else
        {
            scoreMenu.SetActive(false);
        }

    }
    
    public void GoToPhase2()
    {
        phase2Canva.SetActive(true);
        phase1Canva.SetActive(false);
    }
    
}
