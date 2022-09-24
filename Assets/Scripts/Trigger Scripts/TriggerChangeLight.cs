using System.Collections;
using UnityEngine;

namespace Trigger_Scripts
{
    /// <summary>
    /// Change Lighting Color for a more scary atmosphere
    /// </summary>
    public class TriggerChangeLight : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private float delay;
        
        [SerializeField] private Light[] lights;
        [SerializeField] private Color redColor;

        /// <summary>
        /// Changes every dynamic lighting color with delay
        /// </summary>
        /// <returns></returns>
        private IEnumerator ChangeLightToRed()
        {
            yield return new WaitForSeconds(delay);

            foreach (var lightSource in lights)
            {
                lightSource.color = redColor;
            }
            
            Destroy(this); // Destroy the script so the light doesn't change again
        }
        
        /// <summary>
        /// Actions to be done while entering the trigger
        /// </summary>
        /// <param name="other"> The object that enters the trigger </param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                StartCoroutine(ChangeLightToRed());
        }
    }
}
