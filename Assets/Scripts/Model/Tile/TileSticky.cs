using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace TripletsAnimalMatch
{
    public class TileSticky : TileWhitEffect
    {
        private List<Tile> _stickTiles = new List<Tile>();
        private int _maxStickTiles = 2;

        public override void Init(TileModel tileModel, Sprite shape, Sprite animals, GameObject collider, SignalBus signalBus)
        {
            base.Init(tileModel, shape, animals, collider, signalBus);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_stickTiles.Count < _maxStickTiles)
            {
                Tile collisionTile = collision.gameObject.GetComponent<Tile>();

                if (collisionTile != null)
                    DetectCollision(collisionTile);
            }
        }

        public void DetectCollision(Tile collisionTile)
        {
            if (_stickTiles.Contains(collisionTile) == false)
            {
                _stickTiles.Add(collisionTile);
                collisionTile.AttachTile(Rigidbody2D);
            }
        }

        public override void OnTileClick()
        {
            base.OnTileClick();

            Tween = EffectSpriteRenderer
                .DOFade(0.0f, 1.0f)
                .OnComplete(() => EffectSpriteRenderer.enabled = false);

            foreach (Tile tile in _stickTiles)
                tile.DeattachTile();
        }
    }
}
