using System.Collections;
using UnityEngine;

namespace Trigger_Scripts
{
    // Change Lighting Color for a more scary atmosphere
    public class TriggerChangeLight : MonoBehaviour
    {
        // Variables
        [SerializeField] private float delay;
        [SerializeField] private Light[] lights;
        [SerializeField] private Color redColor;
        
        // Changes every dynamic lighting color with delay
        private IEnumerator ChangeLightToRed()
        {
            yield return new WaitForSeconds(delay);

            foreach (var lightSource in lights)
            {
                lightSource.color = redColor;
            }
            
            Destroy(this); // Destroy the script so the light doesn't change again
        }
        
        // Actions to be done while in the trigger
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                StartCoroutine(ChangeLightToRed());
        }
    }
}
