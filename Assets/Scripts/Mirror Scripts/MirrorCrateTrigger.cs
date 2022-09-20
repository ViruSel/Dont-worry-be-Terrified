using Scene_Scripts;
using UnityEngine;

namespace Mirror_Scripts
{
    public class MirrorCrateTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject[] cratesToDisable;

        private void OnTriggerStay(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                ObjectManager.DisableObjects(cratesToDisable);
            }
        }
    
        private void OnTriggerExit(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                ObjectManager.ActivateObjects(cratesToDisable);
            }
        }
    }
}
