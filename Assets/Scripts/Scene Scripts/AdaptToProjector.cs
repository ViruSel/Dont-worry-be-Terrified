using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Scene_Scripts
{
    // Disable certain Post Processing Effects
    public class AdaptToProjector : MonoBehaviour
    {
        // Variables
        [SerializeField] private VolumeProfile volume;
        [SerializeField] private bool isProjecting;
        
        private Tonemapping _tonemapping;
        
        // Called once per frame
        private void Update()
        {
            CheckProjector();
        }
        
        // Change the Tone mapping settings based on preference
        private void CheckProjector()
        {
            if (volume.TryGet(out _tonemapping)) 
                _tonemapping.active = !isProjecting;
        }
    }
}
