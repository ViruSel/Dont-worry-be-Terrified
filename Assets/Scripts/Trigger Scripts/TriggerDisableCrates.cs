using Scene_Scripts;
using UnityEngine;

namespace Trigger_Scripts
{
    public class TriggerDisableCrates : MonoBehaviour
    {
        // Variables
        [SerializeField] private GameObject[] cratesToDisable;

        // Actions to be done while in the trigger
        private void OnTriggerStay(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                ObjectManager.DisableObjects(cratesToDisable);
            }
        }
    
        // Actions to be done while exiting the trigger
        private void OnTriggerExit(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                ObjectManager.ActivateObjects(cratesToDisable);
            }
        }
    }
}
