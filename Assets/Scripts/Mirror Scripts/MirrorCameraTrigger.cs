using Scene_Scripts;
using UnityEngine;

namespace Mirror_Scripts
{
    //TODO: Fix breaking image while running close to the mirrors
    
    public class MirrorCameraTrigger : MonoBehaviour
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
