using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    [Header("Canvas links")]
    public GameObject phase1Canva;
    public GameObject phase2Canva;
    public GameObject gamePhaseCanva;


    private void Start()
    {
        phase1Canva.SetActive(true);
        phase2Canva.SetActive(false);
        gamePhaseCanva.SetActive(false);
    }
}
