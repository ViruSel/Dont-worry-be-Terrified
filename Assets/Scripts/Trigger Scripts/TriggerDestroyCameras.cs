using Scene_Scripts;
using UnityEngine;

namespace Trigger_Scripts
{
    public class TriggerDestroyCameras : MonoBehaviour
    {
        // Variables
        private GameObject[] cameras;
        
        // Called before the first frame update
        private void Start()
        {
            cameras = GameObject.FindGameObjectsWithTag("Camera");
        }
        
        // Destroy objects when player enters trigger
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            ObjectManager.DestroyObjects(cameras);
            Destroy(this);
        }
    }
}
