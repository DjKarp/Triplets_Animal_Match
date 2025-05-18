using UnityEngine;
using Zenject;
using DG.Tweening;

namespace TripletsAnimalMatch
{
    public class TileFrozen : TileWhitEffect
    {
        private bool _isFreezed = true;
        public bool IsFreezed { get => _isFreezed; }

        public override void Init(TileModel tileModel, Sprite shape, Sprite animals, GameObject collider, SignalBus signalBus)
        {
            base.Init(tileModel, shape, animals, collider, signalBus);
        }

        public override void OnTileClick()
        {
            if (_isFreezed == false)
                base.OnTileClick();
        }

        public void Unfreeze()
        {
            _isFreezed = false;

            Tween = EffectSpriteRenderer
                .DOFade(0.0f, 1.0f)
                .OnComplete(() => EffectSpriteRenderer.enabled = false);
        }
    }
}
