using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace TripletsAnimalMatch
{
    public class ReloadTilesButton : MonoBehaviour
    {
        private GameView _gameView;
        private List<SpriteRenderer> _spriteRenderers;        
        private PolygonCollider2D _collider;

        // This animation option for the button is made in the animator, for example
        private Animator _animator;

        private AudioService _audioService;

        [Inject]
        public void Construct(GameView gameView, AudioService audioService)
        {
            _gameView = gameView;
            _audioService = audioService;
        }

        private void Awake()
        {
            _spriteRenderers = new List<SpriteRenderer>();
            _spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
            _animator = GetComponentInChildren<Animator>();
            _collider = GetComponent<PolygonCollider2D>();
        }
        private void OnMouseDown()
        {
            _animator.SetTrigger("isClick");
            _audioService.PlayUIAudio(AudioService.AudioUI.ClickOnButton);
        }

        public void Hide()
        {
            _collider.enabled = false;
            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
                spriteRenderer.DOFade(0.0f, 0.5f);
        }

        public void Show()
        {
            _collider.enabled = true;
            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
                spriteRenderer.DOFade(1.0f, 1.0f);
        }        

        // Call from Animations, for Example
        public void ButtonClick()
        {
            _gameView.ReloadTiles();
        }
    }
}
