using System;
using UnityEngine;

namespace Scene_Scripts
{
    /// <summary>
    /// Trigger one or multiple animations at once with specific sounds
    /// </summary>
    public class TriggerAnimations : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private Animation[] animations;
        [SerializeField] private AudioSource[] sounds;

        /// <summary>
        /// Triggers animations with specific sounds
        /// </summary>
        private void TriggerAnims()
        {
            for (var i = 0; i < animations.Length; i++)
            {
                // Check Mirror object to fix the animation
                if (animations[i].gameObject.name == "Mirror")
                {
                    animations[i].transform.GetChild(0).gameObject.SetActive(false); // Disable Non broken Mirror
                    animations[i].transform.GetChild(1).gameObject.SetActive(false); // Disable Teleportation collider
                }
                
                sounds[i].Play();
                animations[i].Play();
            }
        }
        
        /// <summary>
        /// Actions to be done while entering the trigger
        /// </summary>
        /// <param name="other"> The object that enters the trigger </param>
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
                TriggerAnims();
            
            Destroy(this);  // Destroying the collider so the animations can't be triggered again
        }
    }
}
