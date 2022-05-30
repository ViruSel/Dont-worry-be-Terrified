using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Scene_Scripts
{
    public class AdaptToProjector : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private VolumeProfile volume;
        [SerializeField] private bool isProjecting;
        
        private Tonemapping _tonemapping;
        
        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            isProjecting = false;
        }
        
        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            CheckProjector();
        }

        /// <summary>
        /// Change the tonemapping settings based on preference
        /// </summary>
        private void CheckProjector()
        {
            if (volume.TryGet(out _tonemapping)) 
                _tonemapping.active = !isProjecting;
        }
    }
}
