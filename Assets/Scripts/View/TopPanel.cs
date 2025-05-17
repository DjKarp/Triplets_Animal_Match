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
        private Fishka[] _fishkasPlace;
        public Fishka[] FishkasPlace { get => _fishkasPlace; }
        private bool[] _placeUse;

        protected override void Init()
        {
            _fishkasPlace = new Fishka[7];
            _placeUse = new bool[7];

            HidePosition = StartPosition + new Vector3(10, 0, 0);
            Transform.position = HidePosition;
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
            for (int i = 0; i < _fishkasPlace.Length; i++)
            {
                if (!_placeUse[i])
                {
                    fishka.NumberPositionOnTopPanel = i;
                    _placeUse[i] = true;
                    return _fishkiPosition[i].position;
                }
            }

            return Vector3.zero;
        }

        public void RemoveFishkaFromTopPanel(Fishka fishka)
        {
            for (int i = 0; i < _fishkasPlace.Length; i++)
                if (_fishkasPlace[i] == fishka)
                {
                    _fishkasPlace[i] = null;
                    _placeUse[i] = false;
                }
        }

        public void AddedFishkuOnTopPanel(Fishka fishka, int number)
        {
            _fishkasPlace[number] = fishka;
        }
    }
}
