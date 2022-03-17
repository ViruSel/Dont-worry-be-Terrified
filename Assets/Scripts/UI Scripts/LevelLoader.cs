using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI_Scripts
{
    /// <summary>
    /// Level loading Script with loading screen
    /// </summary>
    public class LevelLoader : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private Animator transition;

        private static readonly int StartTrigger = Animator.StringToHash("Start");
        private const float TransitionTime = 1f;

        /// <summary>
        /// Loading the main menu level by scene name
        /// </summary>
        /// <param name="sceneName"> Scene Name </param>
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadLevel(sceneName));
        }
    
        /// <summary>
        /// Plays animation and loads the next level by scene name
        /// </summary>
        /// <param name="sceneName"> Scene Name </param>
        /// <returns></returns>
        private IEnumerator LoadLevel(string sceneName)
        {
            transition.SetTrigger(StartTrigger);

            yield return new WaitForSeconds(TransitionTime);

            SceneManager.LoadScene(sceneName);
        }
    }
}
