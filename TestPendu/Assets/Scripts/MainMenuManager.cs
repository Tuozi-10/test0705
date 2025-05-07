using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayButton(string sceneName)
    {
        if (sceneName == null) SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(1).name);
        else SceneManager.LoadScene(sceneName);
    }
}
