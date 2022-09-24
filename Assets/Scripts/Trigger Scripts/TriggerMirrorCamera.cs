using Scene_Scripts;
using UnityEngine;

namespace Trigger_Scripts
{
    //TODO: Fix breaking image while running close to the mirrors
    
    public class TriggerMirrorCamera : MonoBehaviour
    {
        [SerializeField] private GameObject[] camerasToEnable;
        [SerializeField] private GameObject[] camerasToDisable;

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
