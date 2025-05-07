using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DotweenStart : MonoBehaviour
{
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float delay;
    private void Awake()
    {
        transform.DOMove(endPosition, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);
    }
}
