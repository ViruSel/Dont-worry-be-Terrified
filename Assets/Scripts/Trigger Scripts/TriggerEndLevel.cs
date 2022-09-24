using Scene_Scripts;
using UnityEngine;

namespace Trigger_Scripts
{
    public class TriggerEndLevel: MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private string levelName;
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
            if (!other.CompareTag("Player")) return;
            
            _levelLoader.LoadScene(levelName);
            Destroy(this);
        }
    }
}
