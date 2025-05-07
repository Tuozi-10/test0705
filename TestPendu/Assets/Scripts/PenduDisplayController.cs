using UnityEngine;

public class PenduDisplayController : MonoBehaviour
{
    [SerializeField] private GameObject[] penduElements;
    private int currentIndex;

    private void Start()
    {
        ResetPendu();
    }

    public void ResetPendu()
    {
        foreach (GameObject element in penduElements) element.SetActive(false);
        currentIndex = 0;
    }

    public void ShowElement()
    {
        if (currentIndex < penduElements.Length)
            penduElements[currentIndex++].SetActive(true);
    }
}