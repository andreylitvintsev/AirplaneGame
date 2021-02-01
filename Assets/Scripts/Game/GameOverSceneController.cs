using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameOverSceneController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("MainMenuScene");
            }
        }
    }
}