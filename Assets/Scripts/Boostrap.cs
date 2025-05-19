using UnityEngine;
using UnityEngine.SceneManagement;

namespace TripletsAnimalMatch
{
    public class Boostrap : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
