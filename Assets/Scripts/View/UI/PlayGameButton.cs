using DG.Tweening;
using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class PlayGameButton : MonoBehaviour
    {
        [SerializeField] private Color _pressButtonColor;
        private Color _startColor;
        private SpriteRenderer _spriteRenderer;
        private SignalBus _signalbus;
        private AudioService _audioService;
        private Sequence _sequence;

        [Inject]
        public void Construct(SignalBus signalBus, AudioService audioService)
        {
            _signalbus = signalBus;
            _audioService = audioService;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _startColor = _spriteRenderer.color;
        }

        private void OnMouseDown()
        {
            _audioService.PlayUIAudio(AudioService.AudioUI.ClickOnButton);

            _sequence = DOTween.Sequence();
            _sequence
                .Append(_spriteRenderer.DOColor(_pressButtonColor, 0.2f))
                .Append(_spriteRenderer.DOColor(_startColor, 0.2f))
                .AppendInterval(0.2f)
                .OnComplete(() => _signalbus.Fire(new PlayGameSignals()));

        }

        private void OnDisable()
        {
            _sequence.Kill(true);
        }
    }
}
