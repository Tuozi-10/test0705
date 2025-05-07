using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void ChangeScene(int index)
    {
        StartCoroutine(WaitForAnim(index));
    }

    IEnumerator WaitForAnim(int index)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(index);
    }
}
