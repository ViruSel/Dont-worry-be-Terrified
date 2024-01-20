using System;
using UnityEngine;

namespace Mirror_Scripts
{
    // Display the room on the other side of the mirror
    public class MirrorCameraView : MonoBehaviour 
    {
        // Variables
        [SerializeField] private Transform mirror;
        [SerializeField] private Transform otherMirror;

        private Transform _playerCamera;
        
        // Called before Start function
        private void Awake()
        {
            _playerCamera = GameObject.FindWithTag("MainCamera").transform;
        }
        
        // Called after all Update functions
        private void LateUpdate()
        {
            ConnectMirrors();
        }
        
        // Connecting 2 mirror views, makes camera from the other mirror follow player movement
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
