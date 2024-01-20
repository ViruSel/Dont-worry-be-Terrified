using Scene_Scripts;
using UnityEngine;

namespace Trigger_Scripts
{
    // Activate cameras while inside trigger
    public class TriggerMirrorCamera : MonoBehaviour
    {
        // Variables
        [SerializeField] private GameObject[] camerasToEnable;
        [SerializeField] private GameObject[] camerasToDisable;

        // Actions to be done while in the trigger
        private void OnTriggerStay(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                ObjectManager.ActivateObjects(camerasToEnable);
                ObjectManager.DisableObjects(camerasToDisable);
            }
        }
    }
}
