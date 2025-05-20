using UnityEngine;
using UnityEngine.SceneManagement;

namespace TripletsAnimalMatch
{
    public class Bootstrap : MonoBehaviour
    {
        private const string GameplaySceneName = "Gameplay";

        private void Start()
        {
            SceneManager.LoadScene(GameplaySceneName);
        }
    }
}
