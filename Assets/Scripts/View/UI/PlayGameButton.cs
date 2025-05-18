using DG.Tweening;
using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class PlayGameButton : MonoBehaviour
    {
        [SerializeField] private Color _pressButtonColor;
        private SpriteRenderer _spriteRenderer;
        private SignalBus _signalbus;
        private Sequence _sequence;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalbus = signalBus;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnMouseDown()
        {
            _sequence = DOTween.Sequence();
            _sequence
                .Append(_spriteRenderer.DOColor(_pressButtonColor, 0.5f))
                .AppendInterval(0.2f)
                .OnComplete(() => _signalbus.Fire(new PlayGameSignals()));

        }

        private void OnDisable()
        {
            _sequence.Kill(true);
        }
    }
}
