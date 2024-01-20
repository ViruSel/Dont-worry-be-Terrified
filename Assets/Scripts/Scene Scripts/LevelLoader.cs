using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene_Scripts
{
    // Level loading Script with loading screen
    public class LevelLoader : MonoBehaviour
    {
        // Variables
        [SerializeField] private Animator transition;
        private const float TransitionTime = 1f;
        
        // Loading level by scene name
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadLevel(sceneName));
        }

        // Loading level Async and additive
        public static void LoadSceneAsyncAdditive(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        
        // Plays animation and loads the level by scene name
        private IEnumerator LoadLevel(string sceneName)
        {
            transition.Play("Crossfade_Start");

            yield return new WaitForSeconds(TransitionTime);

            SceneManager.LoadScene(sceneName);
        }
    }
}
