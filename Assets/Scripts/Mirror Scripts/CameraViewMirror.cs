using System;
using UnityEngine;

namespace Mirror_Scripts
{
    /// <summary>
    /// Display the room on the other side of the mirror
    /// </summary>
    public class CameraViewMirror : MonoBehaviour 
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private Transform mirror;
        [SerializeField] private Transform otherMirror;

        private Transform _playerCamera;
        
        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            _playerCamera = GameObject.FindWithTag("MainCamera").transform;
        }

        /// <summary>
        /// Called after all Update functions
        /// </summary>
        private void LateUpdate()
        {
            ConnectMirrors();
        }

        /// <summary>
        /// Connecting 2 mirror views
        /// Makes camera from the other mirror follow player movement
        /// </summary>
        private void ConnectMirrors()
        {
            // Math
            var playerOffset = _playerCamera.position - otherMirror.position;
            transform.position = mirror.position + playerOffset;

            var angularDiffBetweenMirrorRotations = Quaternion.Angle(mirror.rotation, otherMirror.rotation);

            var mirrorRotationalDifference = Quaternion.AngleAxis(angularDiffBetweenMirrorRotations, Vector3.up);
            var newCameraDirection = mirrorRotationalDifference * _playerCamera.forward;
            transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        }
        
    }
}
