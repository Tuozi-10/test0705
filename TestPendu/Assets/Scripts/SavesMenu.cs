using System;
using DG.Tweening;
using UnityEngine;

public class SavesMenu : MonoBehaviour
{
    [SerializeField] private SaveDisplay savePrefab;
    [SerializeField] private Transform saveContainer;

    private void OnEnable()
    {
        LoadSaves();
    }

    private void LoadSaves()
    {
        for (int i = 1; i <= 3; i++)
        {
            if (!PlayerPrefsTools.HasSave(i)) return;
            
            SaveDisplay saveDisplay = Instantiate(savePrefab, saveContainer);
            SaveData saveData = PlayerPrefsTools.LoadSave(i);
            saveDisplay.Init(saveData);
        }
    }
    
    public void Open()
    {
        transform.DOScale(Vector3.one, 1f);
    }
    
    public void Close()
    {
        transform.DOScale(Vector3.zero, 1f);
    }
}
