using Scene_Scripts;
using UnityEngine;

namespace Trigger_Scripts
{
    public class TriggerDisableCrates : MonoBehaviour
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
