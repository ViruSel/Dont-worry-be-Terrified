using System.Collections;
using Mirror_Scripts;
using Player_Scripts;
using Scene_Scripts;
using UI_Scripts;
using UnityEngine;

namespace Enemy_Scripts
{
    public class Enemy : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private Transform[] waypoints;

        [SerializeField] private float speed;
        [SerializeField] private float damping;

        public static bool CaughtPlayer;
        
        private int _currentWaypoint;

        private GameObject _player;
        private GameObject _playerCamera;
        private LevelLoader _levelLoader;
        
        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            CaughtPlayer = false;

            _player = GameObject.FindWithTag("Player");
            _playerCamera = GameObject.FindWithTag("MainCamera");
            _levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            if(!CaughtPlayer) MoveEnemy();
        }

        /// <summary>
        /// Move Enemy towards waypoint
        /// </summary>
        private void MoveEnemy()
        {
            if (transform.position != waypoints[_currentWaypoint].position)
            {
                var position = transform.position;
                
                // Move Enemy to the next waypoint
                var pos = Vector3.MoveTowards(position, waypoints[_currentWaypoint].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
                
                // Rotate enemy to face the next waypoint
                var rotation = Quaternion.LookRotation(waypoints[_currentWaypoint].position - position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
            else // Loop Waypoints
                _currentWaypoint = (_currentWaypoint + 1) % waypoints.Length;
        }

        /// <summary>
        /// Actions to perform after enemy collided with the player
        /// </summary>
        private IEnumerator KillPlayer()
        {   
            CaughtPlayer = true;
            _player.GetComponent<Movement>().enabled = false;
            _playerCamera.GetComponent<CameraView>().enabled = false;
            
            // Oped death panel
            _levelLoader.LoadScene("Scene 2");

            yield return new WaitForSeconds(1f);

            _player.GetComponent<Movement>().enabled = true;
            _playerCamera.GetComponent<CameraView>().enabled = true;
        }

        /// <summary>
        /// Things to do on collision
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                StartCoroutine(KillPlayer());
        }
    }
}
