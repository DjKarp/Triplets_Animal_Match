using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace TripletsAnimalMatch
{
    public class PlayAgainButton : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(transform.DOScale(0.8f, 0.3f))
                .Append(transform.DOScale(1.0f, 0.3f))
                .AppendInterval(0.2f)
                .OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));

        }
    }
}
