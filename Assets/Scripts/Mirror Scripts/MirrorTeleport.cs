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
        [SerializeField] private Transform otherDoor;

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
            var portalToPlayer = _player.position - transform.position;
            var dotProduct = Vector3.Dot(transform.forward, portalToPlayer);
        
            // If this is true: The player has moved across the portal
            if (!(dotProduct < 0f)) return;
        
            // Teleport player
            var rotationDiff = -Quaternion.Angle(transform.rotation, otherDoor.rotation);
            rotationDiff += 180; // was 180
            _player.Rotate(Vector3.up, rotationDiff);
            
            var positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
            _player.position = otherDoor.position + positionOffset;

            // Player is leaving the portal
            _playerIsOverlapping = false;
        }
    
        /// <summary>
        /// Actions when colliding with the trigger
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter (Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerIsOverlapping = true;
                //Debug.Log("Collision made");
            }
        }

        /// <summary>
        /// Actions when exiting the trigger
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit (Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerIsOverlapping = false;
                //Debug.Log("Collision exit");
            }
        }
    }
}
