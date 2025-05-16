using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TripletsAnimalMatch
{
    public class Fishka : MonoBehaviour
    {
        private FishkaModel _fishkaModel;

        public FishkaModel FishkaModel { get => _fishkaModel; }

        private Transform _transform;
        public Transform Transform { get => _transform; }

        [SerializeField] private SpriteRenderer _shapeSprite;
        [SerializeField] private SpriteRenderer _animalsSprite;
        [SerializeField] private Transform _colliderPosition;
        private GameObject _colliderGO;
        private Rigidbody2D _rigidbody2D;        

        public void Init(FishkaModel fishkaModel, Sprite shape, Sprite animals, GameObject collider)
        {
            _fishkaModel = fishkaModel;
            _shapeSprite.sprite = shape;
            _animalsSprite.sprite = animals;
            _colliderGO = Instantiate(collider, _colliderPosition);

            _transform = gameObject.transform;
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        }
    }
}
