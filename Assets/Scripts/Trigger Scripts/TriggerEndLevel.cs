using Scene_Scripts;
using UnityEngine;

namespace Trigger_Scripts
{
    public class TriggerEndLevel: MonoBehaviour
    {
        // Variables
        [SerializeField] private string levelName;
        private LevelLoader _levelLoader;
        
        // Called before Start function
        private void Awake()
        {
            _levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
        }
        
        // Action to be done while entering the trigger
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            _levelLoader.LoadScene(levelName);
            Destroy(this);
        }
    }
}
