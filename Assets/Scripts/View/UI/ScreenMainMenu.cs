using System;
using UnityEngine;
using DG.Tweening;

namespace TripletsAnimalMatch
{
    public class ScreenMainMenu : EnvironmentDynamic
    {
        protected override void Init()
        {
            Show();
            HidePosition = StartPosition + new Vector3(-10.0f, 0.0f, 0.0f);
        }

        public override void Hide()
        {
            Tween = Transform
                .DOMoveX(HidePosition.x, AnimationTime * 2.0f)
                .From(StartPosition)
                .SetEase(Ease.OutBounce);
        }

        public override void Show(Action callback = null)
        {
            gameObject.SetActive(true);
        }
    }
}
