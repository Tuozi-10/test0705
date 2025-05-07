using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoTweenButtons : MonoBehaviour
{
    public void ClickAnimation()
    {
        transform.DOScale(new Vector3(0.8f,0.8f,0.8f), 0.1f);
        transform.DOScale(new Vector3(1f,1f,1f), 0.1f).SetDelay(0.1f);
    }
}
