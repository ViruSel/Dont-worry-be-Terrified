using System.Collections;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerRespawn : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private Animator crossFade;
        
        private Vector3 _respawnPoint;
        private Vector3 _respawnRotation;
        
        private const float TransitionTime = 1f;
        
        private Transform _player;

        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            _player = GameObject.Find("Player").transform;
            
            _respawnPoint = _player.position;
            _respawnRotation = _player.rotation.eulerAngles;
        }

        /// <summary>
        /// Teleport player back to the level with a transition animation
        /// </summary>
        /// <returns></returns>
        private IEnumerator RespawnPlayer()
        {
            crossFade.Play("Crossfade_Start");
            
            yield return new WaitForSeconds(TransitionTime);
            
            crossFade.Play("Crossfade_End");
            
            _player.localPosition = _respawnPoint;
            _player.localRotation = Quaternion.Euler(_respawnRotation);
        }
        
        /// <summary>
        /// Teleport player back to the level
        /// </summary>
        /// <param name="other"> The object that enters the trigger </param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                StartCoroutine(RespawnPlayer());
        }
    }
}
