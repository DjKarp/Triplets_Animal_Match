using UnityEngine;
using DG.Tweening;
using Zenject;
using System;

namespace TripletsAnimalMatch
{
    public class Tile : MonoBehaviour
    {
        private TileModel _tileModel;

        public TileModel TileModel { get => _tileModel; set => _tileModel = value; }

        private Transform _transform;
        public Transform Transform { get => _transform; }

        private int _topPanelSlotIndex;
        public int TopPanelSlotIndex { set => _topPanelSlotIndex = value; }

        [SerializeField] private SpriteRenderer _shapeSprite;
        [SerializeField] private SpriteRenderer _animalsSprite;
        [SerializeField] private Transform _colliderPosition;
        protected GameObject ColliderGO;
        protected Rigidbody2D Rigidbody2D;        
        private FixedJoint2D _fixedJoint;

        private float _startRigidbodyGravityScale;
        private int _startSortingOrderShapeSprite;
        private int _startSortingOrderAnimalsSprite;

        protected Tween Tween;
        private Sequence _tweenSequence;
        private SignalBus _signalBus;

        private bool _isGameplayActive = false;

        private Vector3 _scaleOnTopPanel = new Vector3(0.9f, 0.9f, 0.9f);

        public virtual void Init(TileModel tileModel, Sprite shape, Sprite animals, GameObject collider, SignalBus signalBus = null)
        {
            _tileModel = tileModel;
            _shapeSprite.sprite = shape;
            _animalsSprite.sprite = animals;
            ColliderGO = Instantiate(collider, _colliderPosition);

            _transform = gameObject.transform;
            Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

            _startRigidbodyGravityScale = Rigidbody2D.gravityScale;
            _startSortingOrderShapeSprite = _shapeSprite.sortingOrder;
            _startSortingOrderAnimalsSprite = _animalsSprite.sortingOrder;

            if (signalBus != null)
            {
                _signalBus = signalBus;
                _signalBus.Subscribe<IsGameplayActiveSignal>(SwitchBoolIsGameplay);
            }
        }

        public void MoveToTopPanel(Vector3 endPosition, float duration)
        {
            _tweenSequence = DOTween.Sequence();

            _tweenSequence
                .Append(_transform.DOMove(endPosition, duration).SetEase(Ease.OutSine))
                .Insert(0, _transform.DOScale(_scaleOnTopPanel, duration / 5))
                .Insert(1, _transform.DORotate(Vector3.zero, duration / 5))
                .OnComplete(() => _signalBus.Fire(new TileOnTopPanelSignal(this, _topPanelSlotIndex)));
        }

        public virtual void MoveToFinish(Vector3 finishPosition, float duration)
        {
            _tweenSequence = DOTween.Sequence();

            _tweenSequence
                .Append(_transform.DOMove(finishPosition, duration).SetEase(Ease.InOutElastic))
                .Append(_transform.DOShakeScale(0.2f))
                .Append(_transform.DOScale(Vector3.zero, duration / 5))
                .OnComplete(() =>
                {
                    SetDefaultState();
                });
        }

        public void DestroyFromGamefield()
        {
            _tweenSequence = DOTween.Sequence();

            _tweenSequence
                .Append(_transform.DOShakeScale(UnityEngine.Random.Range(0.2f, 0.5f), strength: 0.2f))
                .Append(_transform.DOScale(Vector3.zero, UnityEngine.Random.Range(0.2f, 0.5f)))
                .OnComplete(() =>
                {
                    SetDefaultState();
                });
        }

        private void OnMouseDown()
        {
            OnTileClick();
        }

        public virtual void OnTileClick()
        {
            if (_isGameplayActive)
                _signalBus.Fire(new ClickOnTileSignal(this));
        }

        public virtual void SwitchOffRigidbodyAndCollider()
        {
            Rigidbody2D.simulated = false;
            Destroy(ColliderGO);
            _shapeSprite.sortingOrder++;
            _animalsSprite.sortingOrder++;
        }

        protected virtual void SetDefaultState()
        {
            _transform.localScale = Vector3.one;
            Rigidbody2D.gravityScale = _startRigidbodyGravityScale;
            _shapeSprite.sortingOrder = _startSortingOrderShapeSprite;
            _animalsSprite.sortingOrder = _startSortingOrderAnimalsSprite;
            DeattachTile();
            Destroy(ColliderGO);

            _signalBus.Fire(new TileOnFinishSignal(this));
        }

        private void SwitchBoolIsGameplay(IsGameplayActiveSignal startStopGameplay)
        {
            _isGameplayActive = startStopGameplay.IsActive;
        }

        public void AttachTile(Rigidbody2D rigidbody2D)
        {
            _fixedJoint = gameObject.AddComponent<FixedJoint2D>();
            _fixedJoint.connectedBody = rigidbody2D;
            _fixedJoint.autoConfigureConnectedAnchor = true;
            _fixedJoint.enableCollision = false;
        }

        public void DeattachTile()
        {
            if (_fixedJoint != null)
                Destroy(_fixedJoint);
        }

        private void OnDisable()
        {
            Tween.Kill(true);
            _tweenSequence.Kill(true);
        }
    }
}
