using Scene_Scripts;
using UnityEngine;

namespace Trigger_Scripts
{
    public class TriggerActivateObjects : MonoBehaviour
    {
        // Variables
        [SerializeField] private GameObject[] gameObjects;
        
        // Start is called before the first frame update
        private void Start()
        {
            ObjectManager.DisableObjects(gameObjects);
        }
        
        // Activate gameObjects by trigger
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ObjectManager.ActivateObjects(gameObjects);
            }
        }
    }
}
