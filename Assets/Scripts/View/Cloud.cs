using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Zenject;

namespace TripletsAnimalMatch
{
    public class Cloud : EnvironmentDynamic
    {
        private GameplayData _gameplayData;

        [Inject]
        public void Construct(GameplayData gameplayData)
        {
            _gameplayData = gameplayData;
        }

        protected override void Init()
        {
            HidePosition = StartPosition + new Vector3(0, 10, 0);
            Transform.position = HidePosition;
        }

        public override void Hide()
        {
            Tween = Transform
                .DOLocalMoveY(HidePosition.y, AnimationTime)
                .From(StartPosition.y)
                .SetEase(Ease.InElastic);
        }

        public override void Show(Action callback)
        {
            Tween = Transform
                .DOLocalMoveY(StartPosition.y, AnimationTime)
                .From(HidePosition.y)
                .SetEase(Ease.InBounce)
                .OnComplete(() =>
                {
                    callback?.Invoke();
                    StartShake();                    
                });
        }

        public void StartShake()
        {
            Tween = Transform
                .DOShakePosition(_gameplayData.FishkiMaxCountOnScene * _gameplayData.TimeSpawn, strength: 0.3f, vibrato: 2);
        }

        public void StopShake()
        {
            Tween.Kill(true);
        }
    }
}
