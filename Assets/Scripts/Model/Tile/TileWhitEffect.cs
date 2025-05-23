using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class TileWhitEffect : Tile
    {
        [SerializeField] protected SpriteRenderer EffectSpriteRenderer;

        private int _startSortingOrderEffectSprite;

        public override void Init(TileModel tileModel, Sprite shape, Sprite animals, GameObject collider, SignalBus signalBus = null)
        {
            base.Init(tileModel, shape, animals, collider, signalBus);

            _startSortingOrderEffectSprite = EffectSpriteRenderer.sortingOrder;
        }

        protected override void SetDefaultState()
        {
            base.SetDefaultState();

            EffectSpriteRenderer.sortingOrder = _startSortingOrderEffectSprite;
        }

        public override void SwitchOffRigidbodyAndCollider()
        {
            base.SwitchOffRigidbodyAndCollider();

            EffectSpriteRenderer.sortingOrder++;
        }
    }
}
