using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    [SerializeField] private RectTransform menuBar;
    [SerializeField] private RectTransform menuButton;
    [SerializeField] private RectTransform credits;
    [SerializeField] private float menuBarNewPos = 157;
    [SerializeField] private float menuBarBasePos = 0;
    [SerializeField] private float menuButtonNewPos = -260;
    [SerializeField] private float menuButtonBasePos = 427.5f;
    [SerializeField] private float tweenDuration = 0.5f;
    [SerializeField] private TMP_Text[] textScore;
    [SerializeField] private GameObject ScoreDisplayObj;
    private bool menuOut = false;
    private bool ScoreUpdated = false;
    public void PlayGame()
    {
        GM.BeforeGameObj.SetActive(true);
        ScoreUpdated = false;
        GM.MenuObj.SetActive(false);
    }

    public void MenuButton()
    {
        if (!menuOut)
        {
            menuOut = true;
            menuBar.DOAnchorPosX(menuBarNewPos, tweenDuration).SetEase(Ease.OutBack);
            menuButton.DOAnchorPosX(menuButtonNewPos, tweenDuration).SetEase(Ease.OutBack);
        }
        else
        {
            menuOut = false;
            menuBar.DOAnchorPosX(menuBarBasePos, tweenDuration).SetEase(Ease.OutBack);
            menuButton.DOAnchorPosX(menuButtonBasePos, tweenDuration).SetEase(Ease.OutBack);
        }
    }

    public void Scores()
    {
        if (!ScoreUpdated)
        {
            ScoresDisplay();
        }
        ScoreDisplayObj.SetActive(true);
    }

    public void DisableScore()
    {
        ScoreDisplayObj.SetActive(false);
    }

    public void Credits()
    {
        if (credits.position.y < -1000)
        {
            credits.DOLocalRotate(new Vector3(0, 0, -36000),
                3f,RotateMode.FastBeyond360).SetEase(Ease.OutExpo);
            credits.DOAnchorPosY(0, 3f).SetEase(Ease.OutBounce);
        }
        else
        {
            credits.DOLocalRotate(new Vector3(0, 0, -3600),
                3f,RotateMode.FastBeyond360).SetEase(Ease.OutExpo);
            credits.DOAnchorPosY(-1562, 1).SetEase(Ease.OutBounce);
        }
    }
    
    void ScoresDisplay()
    {
        for (int i = 1; i < 4; i++)
        {
            string num = i.ToString();
            if (PlayerPrefs.HasKey("J1Name" + num))
            {
                textScore[i-1].text = "Player 1 : " + PlayerPrefs.GetString("J1Name" + num)+ " chose the word : " + 
                                      PlayerPrefs.GetString("Word" + num)+ " Player 2 : " + 
                                      PlayerPrefs.GetString("J2Name" + num) + " has " + 
                                      PlayerPrefs.GetString("Win" + num) + " with " + 
                                      PlayerPrefs.GetInt("Score" + num) + " errors";
            }
        }
        
        ScoreUpdated = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
