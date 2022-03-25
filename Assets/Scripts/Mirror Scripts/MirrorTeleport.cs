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

        private bool _playerIsOverlapping;
        private Transform _player;
        
        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").transform;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update ()
        {
            if (_playerIsOverlapping)
                TeleportPlayer();
        }

        /// <summary>
        /// Teleporting the Player
        /// </summary>
        private void TeleportPlayer()
        {
            // Math
            var thisTransform = transform;
            var mirrorToPlayer = _player.position - thisTransform.position;
            var dotProduct = Vector3.Dot(thisTransform.up, mirrorToPlayer);

            // Check if player has moved across the portal
            if (dotProduct < 0f)
            {
                // Math
                var rotationDiff = -Quaternion.Angle(thisTransform.rotation, otherMirror.rotation);
                rotationDiff += 180;
                
                // Rotate player
                _player.Rotate(Vector3.up, rotationDiff);

                // Teleport player
                var positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * mirrorToPlayer;
                _player.position = otherMirror.position + positionOffset;

                _playerIsOverlapping = false;
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
