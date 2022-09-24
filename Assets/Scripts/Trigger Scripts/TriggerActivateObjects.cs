using Scene_Scripts;
using UnityEngine;

namespace Trigger_Scripts
{
    public class TriggerActivateObjects : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private GameObject[] gameObjects;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            ObjectManager.DisableObjects(gameObjects);
        }

        /// <summary>
        /// Activate gameObjects when player enters trigger
        /// </summary>
        /// <param name="other"> The object that enters the trigger </param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ObjectManager.ActivateObjects(gameObjects);
            }
        }
    }
}
