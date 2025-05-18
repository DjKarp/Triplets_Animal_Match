using UnityEngine;

namespace TripletsAnimalMatch
{
    public class TileWhitEffect : Tile
    {
        [SerializeField] protected SpriteRenderer EffectSpriteRenderer;

        public override void SwitchOffRigidbodyAndCollider()
        {
            base.SwitchOffRigidbodyAndCollider();

            EffectSpriteRenderer.sortingOrder++;
        }
    }
}
