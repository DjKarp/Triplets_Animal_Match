using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Zenject;

namespace TripletsAnimalMatch
{
    public class PlayAgainButton : MonoBehaviour
    {
        private Sequence _sequence;
        private AudioService _audioService;

        // This animation option for the button is made in the DoTween

        [Inject]
        public void Cinstruct(AudioService audioService)
        {
            _audioService = audioService;
        }

        private void OnMouseDown()
        {
            _sequence = DOTween.Sequence();

            _sequence
                .Append(transform.DOScale(0.8f, 0.3f))
                .Append(transform.DOScale(1.0f, 0.3f))
                .AppendInterval(0.2f)
                .OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));

        }

        private void OnDisable()
        {
            _sequence.Kill(true);
        }
    }
}
