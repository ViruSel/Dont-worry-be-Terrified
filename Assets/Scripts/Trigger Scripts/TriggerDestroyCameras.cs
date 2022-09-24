using Scene_Scripts;
using UnityEngine;

namespace Trigger_Scripts
{
    public class TriggerDestroyCameras : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        private GameObject[] cameras;
        
        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            cameras = GameObject.FindGameObjectsWithTag("Camera");
        }
        
        /// <summary>
        /// Destroy cameras when player enters trigger
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            ObjectManager.DestroyObjects(cameras);
            Destroy(this);
        }
    }
}
