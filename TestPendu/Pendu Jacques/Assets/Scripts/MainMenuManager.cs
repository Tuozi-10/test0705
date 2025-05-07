using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void GotoPlayerSelect()
    {
        SceneManager.LoadScene(1);
    }
}
