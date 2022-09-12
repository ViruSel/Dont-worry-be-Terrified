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
                foreach (var cam in cratesToDisable)
                {
                    cam.SetActive(false);
                }
            }
        }
    
        private void OnTriggerExit(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                foreach (var cam in cratesToDisable)
                {
                    cam.SetActive(true);
                }
            }
        }
    }
}
