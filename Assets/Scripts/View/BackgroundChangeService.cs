using System.Collections.Generic;
using UnityEngine;

namespace TripletsAnimalMatch
{
    public class BackgroundChangeService : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _backgroundSprites = new List<Sprite>();
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_backgroundSprites.Count > 0)
                _spriteRenderer.sprite = _backgroundSprites[Random.Range(0, _backgroundSprites.Count)];
        }
    }
}
