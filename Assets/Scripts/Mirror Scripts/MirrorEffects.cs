using System;
using UnityEngine;

namespace Mirror_Scripts
{
    /// <summary>
    /// Apply player's camera effects on mirrors
    /// </summary>
    public class MirrorEffects : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        private Camera _playerCamera;
        private Camera _mirrorCamera;

        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            _mirrorCamera = GetComponent<Camera>();
            _playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        /// <summary>
        /// Called after all Update functions
        /// </summary>
        private void LateUpdate()
        {
            ChangeFOV();
        }
    
        /// <summary>
        /// Change Mirror's FOV with Player's FOV
        /// </summary>
        private void ChangeFOV()
        {
            _mirrorCamera.fieldOfView = _playerCamera.fieldOfView;
        }
    }
}
