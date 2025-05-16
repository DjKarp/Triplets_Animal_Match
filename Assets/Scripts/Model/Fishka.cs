using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TripletsAnimalMatch
{
    public class Fishka : MonoBehaviour
    {
        private FishkaModel _fishkaModel;

        public FishkaModel FishkaModel { get => _fishkaModel; set => _fishkaModel = value; }

        [SerializeField] private SpriteRenderer _shapeSprite;
        [SerializeField] private SpriteRenderer _animalsSprite;
        [SerializeField] private Transform _colliderPosition;

        private Rigidbody2D _rigidbody2D;

        public void Init()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        }
    }
}
