using UI_Scripts;
using UnityEngine;

namespace Scene_Scripts
{
    public class TriggerEndLevel: MonoBehaviour
    {
        private LevelLoader _levelLoader;

        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            _levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
        }

        /// <summary>
        /// Action to be done while entering the trigger
        /// </summary>
        /// <param name="other"> The object that enters the trigger </param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _levelLoader.LoadScene("Scene 2");
            }
        }
    }
}
