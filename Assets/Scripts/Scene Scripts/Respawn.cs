using System.Collections;
using UnityEngine;

namespace Scene_Scripts
{
    public class Respawn : MonoBehaviour
    {
        [SerializeField] private Animator respawnAnim;
        
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
            respawnAnim.Play("Crossfade_Start");
            
            yield return new WaitForSeconds(TransitionTime);
            
            respawnAnim.Play("Crossfade_End");
            
            _player.localPosition = _respawnPoint;
            _player.localRotation = Quaternion.Euler(_respawnRotation);
        }
        
        /// <summary>
        /// Actions to be done while entering the trigger
        /// </summary>
        /// <param name="other"> The object that enters the trigger </param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                StartCoroutine(RespawnPlayer());
        }
    }
}
