using System.Collections;
using Player_Scripts;
using Scene_Scripts;
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
        [SerializeField] private float rotationTime;

        private static bool _caughtPlayer;
        
        private int _currentWaypoint;

        private GameObject _player;
        private GameObject _playerCamera;
        private LevelLoader _levelLoader;
        
        private PlayerMovement _playerMovement;
        private PlayerCameraView _playerCameraView;
        private Rigidbody _enemyRigidbody;
        
        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Called once per fixed frame
        /// </summary>
        private void FixedUpdate()
        {
            if(!_caughtPlayer) MoveEnemy();
        }

        /// <summary>
        /// Initialize every variable
        /// </summary>
        private void Initialize()
        {
            _caughtPlayer = false;

            speed = EnemyProperties.Speed;
            rotationTime = EnemyProperties.RotationTime;
            
            _player = GameObject.FindWithTag("Player");
            _playerCamera = GameObject.FindWithTag("MainCamera");
            _levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
            
            _enemyRigidbody = GetComponent<Rigidbody>();
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _playerCameraView = _playerCamera.GetComponent<PlayerCameraView>();
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
                _enemyRigidbody.MovePosition(pos);
                
                // Rotate enemy to face the next waypoint
                var rotation = Quaternion.LookRotation(waypoints[_currentWaypoint].position - position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationTime);
            }
            else // Loop Waypoints
                _currentWaypoint = (_currentWaypoint + 1) % waypoints.Length;
        }

        /// <summary>
        /// Actions to perform after enemy collided with the player
        /// </summary>
        private IEnumerator KillPlayer()
        {   
            _caughtPlayer = true;
            _playerMovement.enabled = false;
            _playerCameraView.enabled = false;
            
            // Oped death panel
            _levelLoader.LoadScene("Scene 2");

            yield return new WaitForSeconds(1f);

            _playerMovement.enabled = true;
            _playerCameraView.enabled = true;
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
