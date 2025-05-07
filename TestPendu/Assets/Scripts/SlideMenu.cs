using DG.Tweening;
using UnityEngine;

public class SlideMenu : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float moveTime;

    private bool hasSlided;

    public void Slide()
    {
        transform.DOLocalMoveX(transform.localPosition.x + (hasSlided ? 1 : -1) * rectTransform.rect.width, moveTime);
        hasSlided = !hasSlided;
    }
}