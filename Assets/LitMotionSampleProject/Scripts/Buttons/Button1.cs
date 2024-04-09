using System;
using LitMotion;
using LitMotion.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LitMotionSampleProject.Scripts.Buttons
{
    public class Button1 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Header("Components")]
        [SerializeField] private Image _fillImage;
        [SerializeField] private TextMeshProUGUI _labelText;

        [Header("Settings")]
        [SerializeField] private Color _hoverLabelColor;
        [SerializeField] private Ease _hoverEase;
        [SerializeField] private Ease _clickEase;
        [SerializeField] private float _hoverDuration;
        [SerializeField] private float _clickDuration;
        
        /// <summary>
        /// クリック時のイベント
        /// </summary>
        public event Action OnClick;
        
        private readonly CompositeMotionHandle _motionHandles = new(2);


        public void OnPointerEnter(PointerEventData eventData)
        {
            _motionHandles.Cancel();

            LMotion.Create(Vector3.zero, Vector3.one, _hoverDuration)
                .WithEase(_hoverEase)
                .BindToLocalScale(_fillImage.transform)
                .AddTo(_motionHandles);

            LMotion.Create(_labelText.color, _hoverLabelColor, _hoverDuration)
                .WithEase(_hoverEase)
                .BindToColor(_labelText)
                .AddTo(_motionHandles);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _motionHandles.Cancel();

            LMotion.Create(Vector3.one, Vector3.zero, _hoverDuration)
                .WithEase(_hoverEase)
                .BindToLocalScale(_fillImage.transform)
                .AddTo(_motionHandles);

            LMotion.Create(_labelText.color, Color.white, _hoverDuration)
                .WithEase(_hoverEase)
                .BindToColor(_labelText)
                .AddTo(_motionHandles);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            LMotion.Create(Vector3.one, Vector3.one * 0.9f, _clickDuration)
                .WithLoops(2, LoopType.Yoyo)
                .WithEase(_clickEase)
                .BindToLocalScale(transform)
                .AddTo(gameObject);
            
            OnClick?.Invoke();
        }

        private void OnDestroy()
        {
            _motionHandles.Cancel();
        }
    }
}