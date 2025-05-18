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
        private Animator _animator;
        private PolygonCollider2D _collider;

        [Inject]
        public void Construct(GameView gameView)
        {
            _gameView = gameView;
        }

        private void Awake()
        {
            _spriteRenderers = new List<SpriteRenderer>();
            _spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
            _animator = GetComponentInChildren<Animator>();
            _collider = GetComponent<PolygonCollider2D>();
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

        private void OnMouseDown()
        {
            _animator.SetTrigger("isClick");            
        }

        // Call from Animations, for Example
        public void ButtonClick()
        {
            _gameView.ReloadTiles();
        }
    }
}
