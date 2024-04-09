using LitMotion;
using LitMotion.Extensions;
using LitMotionSampleProject.Scripts.Buttons;
using TMPro;
using UnityEngine;

namespace LitMotionSampleProject.Scripts.Scores
{
    public sealed class Score1 : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button1 _addButton;
        [SerializeField] private Button1 _resetButton;


        [Header("Settings")]
        [SerializeField] private int _addition;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _addDuration;
        [SerializeField] private float _resetDuration;
        

        private TextMeshProUGUI _text;

        private int _currentScore;
        
        private readonly CompositeMotionHandle _motionHandle = new(2);

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _addButton.OnClick += OnClick;
            _resetButton.OnClick += OnReset;
        }

        private void OnClick()
        {
            _motionHandle.Cancel();
            
            LMotion.Create(_currentScore, _currentScore + _addition, _addDuration)
                .WithEase(_ease)
                .BindToText(_text,  "{0:000000}")
                .AddTo(_motionHandle);

            _currentScore += _addition;
        }

        private void OnReset()
        {
            _motionHandle.Cancel();
            
            LMotion.Create(_currentScore, 0, _resetDuration)
                .WithEase(_ease)
                .WithOnComplete(() => _currentScore = 0)
                .BindToText(_text, "{0:000000}")
                .AddTo(_motionHandle);
        }

        private void OnDestroy()
        {
            _addButton.OnClick -= OnClick;
            _resetButton.OnClick -= OnReset;
        }
    }
}