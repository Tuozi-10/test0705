using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // attention au nom en dur, j'ai du modifier moi meme pour que la scene se lance :(
    public void PlayButton(string sceneName)
    {
        if (sceneName == null) SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(1).name);
        else SceneManager.LoadScene(sceneName);
    }
}
