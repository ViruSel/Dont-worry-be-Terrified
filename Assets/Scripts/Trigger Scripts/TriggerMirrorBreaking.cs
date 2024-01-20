using System;
using UnityEngine;

namespace Trigger_Scripts
{
    // Trigger one or multiple animations at once with specific sounds
    public class TriggerMirrorBreaking : MonoBehaviour
    {
        // Variables
        private GameObject mirror;
        private GameObject mirrorUnbroken;
        
        private Animation anim;
        private AudioSource sound;
        
        // Called before the first frame update
        private void Start()
        {
            Initialize();
        }
        
        // initialize variables
        private void Initialize()
        {
            mirror = GameObject.FindWithTag("Mirror");
            mirrorUnbroken = mirror.transform.GetChild(0).gameObject;
            
            anim = mirror.GetComponent<Animation>();
            sound = mirror.GetComponent<AudioSource>();
        }
        
        // Actions to be done while entering the trigger
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            mirrorUnbroken.SetActive(false);
            
            anim.Play();
            sound.Play();

            Destroy(this);  // Destroying the collider so the animations can't be triggered again
        }
    }
}
