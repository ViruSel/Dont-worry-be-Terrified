using System;
using UnityEngine;

namespace Scene_Scripts
{
    /// <summary>
    /// Trigger one or multiple animations at once
    /// </summary>
    public class TriggerAnimations : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private Animator[] animators;
        [SerializeField] private string[] actions;

        /// <summary>
        /// Actions to be done while entering the trigger
        /// </summary>
        /// <param name="other"> The object that enters the trigger </param>
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            for (var i = 0; i < animators.Length; i++)
            {
                animators[i].Play(actions[i],0,0f);
            }
            
            Destroy(this);  // Destroying the collider so the animations can't be triggered again
        }
    }
}
