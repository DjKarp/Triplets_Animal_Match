using UnityEngine;
using UnityEngine.SceneManagement;

namespace TripletsAnimalMatch
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
