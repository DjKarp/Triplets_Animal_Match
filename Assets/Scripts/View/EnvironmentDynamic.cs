using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace TripletsAnimalMatch
{
    public abstract class EnvironmentDynamic : MonoBehaviour
    {
        protected Transform Transform;
        protected float AnimationTime = 0.5f;
        protected Tween Tween;
        protected Vector3 StartPosition;
        protected Vector3 HidePosition;

        private void Awake()
        {
            Transform = gameObject.transform;
            StartPosition = Transform.position;

            Init();
        }

        protected abstract void Init();

        public abstract void Show(Action callback = null);

        public abstract void Hide();

        private void OnDisable()
        {
            Tween.Kill(true);
        }
    }
}
