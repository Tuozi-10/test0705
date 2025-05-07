using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.AnimationD
{
    public class ButtonAnimation : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] private ButtonType _buttonType;
        [SerializeField] private float scale;
        private Vector3 scaleOriginal;
        
        enum ButtonType
        {
            play,
            parameters
        }

        private void Start()
        {
            scaleOriginal = this.transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            switch (_buttonType)
            {
                case ButtonType.play:
                    transform.DOKill();
                    transform.DOScale(scale, 1f).SetEase(Ease.InBounce);
                    
                    break;
                case ButtonType.parameters:
                    transform.DOKill();
                    transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
                        .SetEase(Ease.OutQuad);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            switch (_buttonType)
            {
                case ButtonType.play:
                    transform.DOKill();
                    transform.localScale = scaleOriginal;
                    break;
                case ButtonType.parameters:
                    transform.DOKill();
                    transform.rotation = Quaternion.identity;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}