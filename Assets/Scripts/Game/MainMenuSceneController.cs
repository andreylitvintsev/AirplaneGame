using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MainMenuSceneController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                Quit();
            }
            else if (Input.anyKey)
            {
                SceneManager.LoadScene("GameScene");
            }
        }

        private void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            
        }
    }
}