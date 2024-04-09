using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LitMotionSampleProject.Scripts.Buttons
{
    public class Button2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Header("Components")]
        [SerializeField] private Image _fillImage;
        [SerializeField] private RectTransform _labelParent;

        [Header("Settings")]
        [SerializeField] private Ease _hoverEase;
        [SerializeField] private Ease _clickEase;

        /// <summary>
        /// ホバー時の背景画像のX座標
        /// </summary>
        [SerializeField] private float _imageHoverPosX;

        /// <summary>
        /// ホバー時のラベルのY座標
        /// </summary>
        [SerializeField] private float _labelHoverPosY;
        [SerializeField] private float _hoverDuration;
        [SerializeField] private float _clickDuration;


        private float _imageStartPosX;
        private float _labelStartPosY;

        private readonly CompositeMotionHandle _motionHandles = new(2);

        private void Start()
        {
            _imageStartPosX = _fillImage.rectTransform.anchoredPosition.x;
            _labelStartPosY = _labelParent.anchoredPosition.y;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _motionHandles.Cancel();

            LMotion.Create(_fillImage.rectTransform.anchoredPosition.x, _imageHoverPosX, _hoverDuration)
                .WithEase(_hoverEase)
                .BindToAnchoredPositionX(_fillImage.rectTransform)
                .AddTo(_motionHandles);

            LMotion.Create(_labelParent.anchoredPosition.y, _labelHoverPosY, _hoverDuration)
                .WithEase(_hoverEase)
                .BindToAnchoredPositionY(_labelParent)
                .AddTo(_motionHandles);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _motionHandles.Cancel();

            LMotion.Create(_fillImage.rectTransform.anchoredPosition.x, _imageStartPosX, _hoverDuration)
                .WithEase(_hoverEase)
                .BindToAnchoredPositionX(_fillImage.rectTransform)
                .AddTo(_motionHandles);

            LMotion.Create(_labelParent.anchoredPosition.y, _labelStartPosY, _hoverDuration)
                .WithEase(_hoverEase)
                .BindToAnchoredPositionY(_labelParent)
                .AddTo(_motionHandles);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            LMotion.Create(Vector3.one, Vector3.one * 0.9f, _clickDuration)
                .WithLoops(2, LoopType.Yoyo)
                .WithEase(_clickEase)
                .BindToLocalScale(transform)
                .AddTo(gameObject);
        }

        private void OnDestroy()
        {
            _motionHandles.Cancel();
        }
    }
}