using LitMotion;
using LitMotion.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LitMotionSampleProject.Scripts.Buttons
{
    public class Button4 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _labelText;

        [Header("Settings")]
        [SerializeField] private Ease _clickEase;
        [SerializeField] private float _hoverDuration;
        [SerializeField] private float _clickDuration;

        private string _defaultText;
        private float _imageStartPosY;
        private float _labelStartPosY;

        private readonly CompositeMotionHandle _motionHandles = new(2);

        private void Start()
        {
            _defaultText = _labelText.text;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _motionHandles.Cancel();

            LMotion.String.Create512Bytes("", _defaultText, _hoverDuration)
                .WithScrambleChars(ScrambleMode.All)
                .BindToText(_labelText)
                .AddTo(_motionHandles);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _motionHandles.Cancel();

            _labelText.text = _defaultText;
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