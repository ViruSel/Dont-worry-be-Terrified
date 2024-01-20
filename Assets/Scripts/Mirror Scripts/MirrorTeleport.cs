using System;
using UnityEngine;

namespace Mirror_Scripts
{
    // Teleport player to the room inside the mirror
    public class MirrorTeleport : MonoBehaviour
    {
        // Variables
        [SerializeField] private Transform otherMirror;

        public bool playerTeleported;
        private bool _playerIsOverlapping;
        private bool _crateIsOverlapping;

        private Transform _player;
        
        // Called before Start function
        private void Awake()
        {
            playerTeleported = false;
            _player = GameObject.FindWithTag("Player").transform;
        }
        
        // Update is called once per frame
        private void Update ()
        {
            if (_playerIsOverlapping)
                Teleport(_player);

            if (_crateIsOverlapping)
            {
                //TODO: Teleport crate
            }
        }
        
        // Teleporting the Player
        private void Teleport(Transform objectToTeleport)
        {
            // Math
            var thisTransform = transform;
            var mirrorToObject = objectToTeleport.position - thisTransform.position;
            var dotProduct = Vector3.Dot(thisTransform.up, mirrorToObject);

            // Check if object has moved across the portal
            if (dotProduct < 0f)
            {
                // Math
                var rotationDiff = -Quaternion.Angle(thisTransform.rotation, otherMirror.rotation);
                rotationDiff += 180;
                
                // Rotate object
                objectToTeleport.Rotate(Vector3.up, rotationDiff);

                // Teleport object
                var positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * mirrorToObject;
                objectToTeleport.position = otherMirror.position + positionOffset;
                
                CheckTeleportingObject(objectToTeleport);
            }
        }
        
        // Check which object to be teleported
        private void CheckTeleportingObject(Component objectToCheck)
        {
            if (objectToCheck.CompareTag("Player"))
            {
                _playerIsOverlapping = false;
                playerTeleported = true;
            }
        }
        
        // Actions when colliding with the trigger
        private void OnTriggerEnter (Collider other)
        {
            if (other.CompareTag("Player"))
                _playerIsOverlapping = true;
        }
        
        /// Actions when exiting the trigger
        private void OnTriggerExit (Collider other)
        {
            if (other.CompareTag("Player"))
                _playerIsOverlapping = false;
        }
    }
}
