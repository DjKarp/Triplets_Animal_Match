using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using System;

namespace TripletsAnimalMatch
{
    public class Fishka : MonoBehaviour
    {
        private FishkaModel _fishkaModel;

        public FishkaModel FishkaModel { get => _fishkaModel; }

        private Transform _transform;
        public Transform Transform { get => _transform; }

        private int _numberPositionOnTopPanel;
        public int NumberPositionOnTopPanel { set => _numberPositionOnTopPanel = value; }

        [SerializeField] private SpriteRenderer _shapeSprite;
        [SerializeField] private SpriteRenderer _animalsSprite;
        [SerializeField] private Transform _colliderPosition;
        private GameObject _colliderGO;
        private Rigidbody2D _rigidbody2D;

        private Tween _tween;
        private Sequence _tweenSequence;
        private SignalBus _signalBus;

        private bool _isGameplayOn = false;

        private Vector3 _scaleOnTopPanel = new Vector3(0.9f, 0.9f, 0.9f);

        public void Init(FishkaModel fishkaModel, Sprite shape, Sprite animals, GameObject collider, SignalBus signalBus)
        {
            _fishkaModel = fishkaModel;
            _shapeSprite.sprite = shape;
            _animalsSprite.sprite = animals;
            _colliderGO = Instantiate(collider, _colliderPosition);
            _signalBus = signalBus;

            _transform = gameObject.transform;
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

            _signalBus.Subscribe<StartStopGameplaySignal>(SwitchBoolIsGameplay);
        }

        public void MoveToTopPanel(Vector3 endPosition, float duration)
        {
            _tweenSequence = DOTween.Sequence();

            _tweenSequence
                .Append(_transform.DOMove(endPosition, duration).SetEase(Ease.OutSine))
                .Insert(0, _transform.DOScale(_scaleOnTopPanel, duration / 5))
                .Insert(1, _transform.DORotate(Vector3.zero, duration / 5))
                .OnComplete(() => _signalBus.Fire(new FishkaOnTopPanelSignal(this, _numberPositionOnTopPanel)));
        }

        public void MoveToFinishPlace(Vector3 finishPosition, float duration)
        {
            _tweenSequence = DOTween.Sequence();

            _tweenSequence
                .Append(_transform.DOMove(finishPosition, duration).SetEase(Ease.InOutElastic))
                .Append(_transform.DOShakeScale(0.2f))
                .Append(_transform.DOScale(Vector3.zero, duration / 5))
                .OnComplete(() => gameObject.SetActive(false));
        }

        public void DestroyFromGamefield()
        {
            _tweenSequence = DOTween.Sequence();

            _tweenSequence
                .Append(_transform.DOShakeScale(UnityEngine.Random.Range(0.2f, 1.0f), strength: 0.5f))
                .Append(_transform.DOScale(Vector3.zero, UnityEngine.Random.Range(0.2f, 1.0f)))
                .OnComplete(() => Destroy(this));
        }

        private void OnMouseDown()
        {
            if (_isGameplayOn)
            {
                _signalBus.Fire(new ClickOnFishkaSignal(this));
                SwitchOffRigidbodyAndCollider();
            }
        }

        private void SwitchOffRigidbodyAndCollider()
        {
            _rigidbody2D.simulated = false;
            _colliderGO.gameObject.SetActive(false);
            _shapeSprite.sortingOrder++;
            _animalsSprite.sortingOrder++;
        }

        private void SwitchBoolIsGameplay(StartStopGameplaySignal startStopGameplay)
        {
            _isGameplayOn = startStopGameplay.IsStart;
        }

        private void OnDisable()
        {
            _tween.Kill(true);
            _tweenSequence.Kill(true);
        }
    }
}
