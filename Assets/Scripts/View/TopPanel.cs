using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace TripletsAnimalMatch
{
    public class TopPanel : EnvironmentDynamic
    {
        [SerializeField] private Transform[] _fishkiPosition = new Transform[7];
        private List<Fishka> _fishkasPlace = new List<Fishka>();
        public List<Fishka> FishkasPlace { get => _fishkasPlace; }

        protected override void Init()
        {
            HidePosition = StartPosition + new Vector3(10, 0, 0);
            Transform.position = HidePosition;

            _fishkasPlace.Clear();
        }

        public override void Hide()
        {
            Tween = Transform
                .DOMoveX(HidePosition.x, AnimationTime)
                .From(StartPosition)
                .SetEase(Ease.InBounce);
        }

        public override void Show(Action callback = null)
        {
            Tween = Transform
                .DOMoveX(StartPosition.x, AnimationTime)
                .From(HidePosition)
                .SetEase(Ease.OutBounce);
        }

        public Vector3 GetNextFishkaPosition(Fishka fishka)
        {

            int number = _fishkasPlace.Count;
            _fishkasPlace.Add(fishka);
            return _fishkiPosition[number].position;
        }
    }
}
