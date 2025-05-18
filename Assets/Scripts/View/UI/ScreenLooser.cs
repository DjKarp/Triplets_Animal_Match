using System;
using UnityEngine;
using DG.Tweening;

namespace TripletsAnimalMatch
{
    public class ScreenLooser : EnvironmentDynamic
    {
        protected override void Init()
        {
            HidePosition = StartPosition + new Vector3(0.0f, -20.0f, 0.0f);

            Hide();
        }

        public override void Hide()
        {
            Transform.position = HidePosition;
        }

        public override void Show(Action callback = null)
        {
            Tween = Transform
                .DOMoveY(StartPosition.y, AnimationTime * 5.0f)
                .From(HidePosition)
                .SetEase(Ease.OutBounce);
        }
    }
}
