using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private bool selectMenuIsDeployed = false, scoreMenuIsDeployed = false;
    private bool isMoving = false; 

    
    public GameObject scoreCanvas;
    public GameObject[] selectMenuCanvas;
    
    public GameObject setting;

    private GameObject[] save1;
    private GameObject[] save2;
    private GameObject[] save3;
    
    public float slideDistance = 500f;
    public float slideDuration = 0.5f;

    // Menu Buttons
    public void StartGame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void SelectMenu()
    {
        StartCoroutine(DeploySelectMenu());
    }

    public void ScoreMenu()
    {
        scoreMenuIsDeployed = !scoreMenuIsDeployed;
        scoreCanvas.SetActive(scoreMenuIsDeployed);
        Load();
        
    }

    public void Settings()
    {
        setting.SetActive(!setting.activeSelf);
        setting.GetComponent<TMP_Text>().text = "NO SETTINGS LMAO";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Load()
    {
        string player1Save1 = PlayerPrefs.GetString("Save1/Player1", "Player 1");
        string player2Save1 = PlayerPrefs.GetString("Save1/Player2", "Player 2");
        string wordSave1 = PlayerPrefs.GetString("Save1/Word", "No Word");

        string player1Save2 = PlayerPrefs.GetString("Save2/Player1", "Player 1");
        string player2Save2 = PlayerPrefs.GetString("Save2/Player2", "Player 2");
        string wordSave2 = PlayerPrefs.GetString("Save2/Word", "No Word");

        string player1Save3 = PlayerPrefs.GetString("Save3/Player1", "Player 1");
        string player2Save3 = PlayerPrefs.GetString("Save3/Player2", "Player 2");
        string wordSave3 = PlayerPrefs.GetString("Save3/Word", "No Word");

        UpdateSaveSlotUI(save1, player1Save1, player2Save1, wordSave1);
        UpdateSaveSlotUI(save2, player1Save2, player2Save2, wordSave2);
        UpdateSaveSlotUI(save3, player1Save3, player2Save3, wordSave3);
    }

    private void UpdateSaveSlotUI(GameObject[] saveSlot, string player1, string player2, string word)
    {
        if (saveSlot.Length > 0)
        {
            saveSlot[0].GetComponent<TMP_Text>().text = player1; 
            saveSlot[1].GetComponent<TMP_Text>().text = word; 
            saveSlot[2].GetComponent<TMP_Text>().text = player2;    
        }
    }
    
    // Select Menu Coroutine
    IEnumerator DeploySelectMenu()
    {
        if (isMoving) yield break; 
        isMoving = true;

        float elapsed = 0f;
        Vector2 direction = selectMenuIsDeployed ? Vector2.right : Vector2.left;

        while (elapsed < slideDuration)
        {
            float step = (slideDistance / slideDuration) * Time.deltaTime;

            foreach (GameObject menu in selectMenuCanvas)
            {
                if (menu != null)
                {
                    RectTransform rt = menu.GetComponent<RectTransform>();
                    rt.anchoredPosition += direction * step;
                }
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        selectMenuIsDeployed = !selectMenuIsDeployed;
        isMoving = false; 
    }


}