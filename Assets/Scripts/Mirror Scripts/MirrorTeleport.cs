using System;
using UnityEngine;

namespace Mirror_Scripts
{
    /// <summary>
    /// Teleport player to the room inside the mirror
    /// </summary>
    public class MirrorTeleport : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private Transform otherMirror;

        public bool playerTeleported;
        private bool _playerIsOverlapping;

        private Transform _player;

        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            playerTeleported = false;
            _player = GameObject.FindWithTag("Player").transform;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update ()
        {
            if (_playerIsOverlapping)
                Teleport(_player);
        }

        /// <summary>
        /// Teleporting the Player
        /// </summary>
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
        
        /// <summary>
        /// Check which object to be teleported
        /// </summary>
        /// <param name="objectToCheck"></param>
        private void CheckTeleportingObject(Component objectToCheck)
        {
            if (objectToCheck.CompareTag("Player"))
            {
                _playerIsOverlapping = false;
                playerTeleported = true;
            }
        }

        /// <summary>
        /// Actions when colliding with the trigger
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter (Collider other)
        {
            if (other.CompareTag("Player"))
                _playerIsOverlapping = true;
        }

        /// <summary>
        /// Actions when exiting the trigger
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit (Collider other)
        {
            if (other.CompareTag("Player"))
                _playerIsOverlapping = false;
        }
    }
}
