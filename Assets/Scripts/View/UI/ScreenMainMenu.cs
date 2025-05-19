using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

namespace TripletsAnimalMatch
{
    public class ScreenMainMenu : EnvironmentDynamic
    {
        // ƒл€ вступлени€ не стал с зависимост€ми сильно работать. —делал быстро просто, так как уже 3 дн€ прошло. 

        [SerializeField] private Transform _menuPictures;
        [SerializeField] private List<SpriteRenderer> _textBackgroundList;
        [SerializeField] private List<Transform> _textLorList;
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
            HideTextAndBackground();
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
                .Append(_textBackgroundList[0].DOFade(0.7f, AnimationTime))
                .Insert(AnimationTime * 3.0f, _textBackgroundList[1].DOFade(0.7f, AnimationTime))
                .Append(_textLorList[0].DOMoveX(0.0f, AnimationTime))
                .AppendInterval(2.0f)
                .Append(_textLorList[0].DOMoveX(-10.0f, AnimationTime))
                .Append(_textLorList[1].DOMoveX(0.0f, AnimationTime))
                .AppendInterval(2.0f)
                .Append(_textLorList[1].DOMoveX(-10.0f, AnimationTime))
                .Append(_textLorList[2].DOMoveX(0.0f, AnimationTime))
                .AppendInterval(2.0f)
                .Append(_textLorList[2].DOMoveX(-10.0f, AnimationTime))
                .Append(_textLorList[3].DOMoveX(0.0f, AnimationTime))
                .AppendInterval(2.0f)
                .Append(_textLorList[3].DOMoveX(-10.0f, AnimationTime))
                .Append(_textBackgroundList[0].DOFade(0.0f, AnimationTime))
                .Append(_textBackgroundList[1].DOFade(0.0f, AnimationTime))
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
            HideTextAndBackground();
            foreach (Transform transform in _textLorList)
                transform.gameObject.SetActive(false);
            _signalBus.Fire(new FinishShowLogoSignal());
        }

        private void HideTextAndBackground()
        {
            foreach (SpriteRenderer spriteRenderer in _textBackgroundList)
                spriteRenderer.DOFade(0.0f, 0.0f);

            foreach (Transform transform in _textLorList)
                transform.position = transform.position + new Vector3(10.0f, 0.0f, 0.0f);
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
