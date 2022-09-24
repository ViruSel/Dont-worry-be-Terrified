using System;
using UnityEngine;

namespace Trigger_Scripts
{
    /// <summary>
    /// Trigger one or multiple animations at once with specific sounds
    /// </summary>
    public class TriggerMirrorBreaking : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        private GameObject mirror;
        private GameObject mirrorUnbroken;
        
        private Animation anim;
        private AudioSource sound;
        
        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            Initialize();
        }
        
        /// <summary>
        /// initialize variables
        /// </summary>
        private void Initialize()
        {
            mirror = GameObject.FindWithTag("Mirror");
            mirrorUnbroken = mirror.transform.GetChild(0).gameObject;
            
            anim = mirror.GetComponent<Animation>();
            sound = mirror.GetComponent<AudioSource>();
        }

        /// <summary>
        /// Actions to be done while entering the trigger
        /// </summary>
        /// <param name="other"> The object that enters the trigger </param>
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            mirrorUnbroken.SetActive(false);
            
            anim.Play();
            sound.Play();

            Destroy(this);      // Destroying the collider so the animations can't be triggered again
        }
    }
}
