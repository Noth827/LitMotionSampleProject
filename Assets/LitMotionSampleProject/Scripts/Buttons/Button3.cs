using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LitMotionSampleProject.Scripts.Buttons
{
    public class Button3 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Header("Components")]
        [SerializeField] private RectTransform _fillImageParent;
        [SerializeField] private RectTransform _labelParent;

        [Header("Settings")]
        [SerializeField] private Ease _hoverEase;
        [SerializeField] private Ease _clickEase;

        /// <summary>
        /// ホバー時の背景画像のY座標
        /// </summary>
        [SerializeField] private float _imageHoverPosY;

        /// <summary>
        /// ホバー時のラベルのY座標
        /// </summary>
        [SerializeField] private float _labelHoverPosY;
        [SerializeField] private float _hoverDuration;
        [SerializeField] private float _clickDuration;


        private float _imageStartPosY;
        private float _labelStartPosY;

        private readonly CompositeMotionHandle _motionHandles = new(2);

        private void Start()
        {
            _imageStartPosY = _fillImageParent.anchoredPosition.y;
            _labelStartPosY = _labelParent.anchoredPosition.y;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _motionHandles.Cancel();

            LMotion.Create(_fillImageParent.anchoredPosition.y, _imageHoverPosY, _hoverDuration)
                .WithEase(_hoverEase)
                .BindToAnchoredPositionY(_fillImageParent)
                .AddTo(_motionHandles);

            LMotion.Create(_labelParent.anchoredPosition.y, _labelHoverPosY, _hoverDuration)
                .WithEase(_hoverEase)
                .BindToAnchoredPositionY(_labelParent)
                .AddTo(_motionHandles);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _motionHandles.Cancel();

            LMotion.Create(_fillImageParent.anchoredPosition.y, _imageStartPosY, _hoverDuration)
                .WithEase(_hoverEase)
                .BindToAnchoredPositionY(_fillImageParent)
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