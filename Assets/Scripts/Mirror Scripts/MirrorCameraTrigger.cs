using UnityEngine;

namespace Mirror_Scripts
{
    //TODO: Fix breaking image while being close to the mirrors
    
    public class MirrorCameraTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject[] camerasToEnable;
        [SerializeField] private GameObject[] camerasToDisable;

        private void OnTriggerStay(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                foreach (var cam in camerasToEnable)
                {
                    cam.SetActive(true);
                }
                foreach (var cam in camerasToDisable)
                {
                    cam.SetActive(false);
                }
            }
        }
    }
}
