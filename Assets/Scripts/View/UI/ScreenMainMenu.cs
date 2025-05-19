using System;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Zenject;

namespace TripletsAnimalMatch
{
    public class ScreenMainMenu : EnvironmentDynamic
    {
        // ƒл€ вступлени€ не стал с зависимост€ми сильно работать. —делал быстро просто, так как уже 3 дн€ прошло. 

        [SerializeField] private Transform _menuPictures;
        [SerializeField] private SpriteRenderer _textBackground;
        [SerializeField] private SpriteRenderer _textBackground2;
        [SerializeField] private TextMeshPro _textLor;
        [SerializeField] private Transform _planeTransform;
        [SerializeField] private Cloud _cloud;
        [SerializeField] private GameObject _stakan;

        private SignalBus _signalBus;
        private Sequence _sequence;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        protected override void Init()
        {
            Show();
            HidePosition = StartPosition + new Vector3(-10.0f, 0.0f, 0.0f);

            _textBackground.DOFade(0.0f, 0.0f);
            _textLor.DOFade(0.0f, 0.0f);
            _stakan.SetActive(false);
        }

        public override void Hide()
        {
            _cloud.SimplyShow();

            _sequence = DOTween.Sequence();

            _sequence
                .Append(_menuPictures
                    .DOMoveX(HidePosition.x, AnimationTime * 2.0f)
                    .From(StartPosition)
                    .SetEase(Ease.OutBounce))
                .Append(_textBackground.DOFade(0.7f, AnimationTime))
                .Append(_textLor.DOFade(1.0f, AnimationTime))
                .AppendInterval(10.0f)
                .Append(_textBackground2.DOFade(0.0f, 0.1f))
                .Append(_textLor.DOFade(0.0f, AnimationTime))
                .Append(_textBackground.DOFade(0.0f, AnimationTime))
                .Append(_planeTransform
                    .DOMoveX(_planeTransform.position.x - 10.0f, AnimationTime * 5.0f)
                    .SetEase(Ease.InOutExpo))
                .OnComplete(() => HideLogo());
        }

        private void Update()
        {
            if (_sequence != null && _sequence.active && Input.anyKey)
            {
                _sequence.Kill();
                HideLogo();
            }
        }

        private void HideLogo()
        {
            _cloud.Hide();
            _stakan.SetActive(true);
            _textBackground.DOFade(0.0f, 0.1f);
            _textBackground2.DOFade(0.0f, 0.1f);
            _textLor.DOFade(0.0f, 0.0f);

            _signalBus.Fire(new FinishShowLogoSignal());
        }

        public override void Show(Action callback = null)
        {
            gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            _sequence.Kill();
        }
    }
}
