using System.Collections;
using Player_Scripts;
using Scene_Scripts;
using UnityEngine;

namespace Enemy_Scripts
{
    public class Enemy : MonoBehaviour
    {
        // Variables
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
        
        // Called before the first frame update
        private void Start()
        {
            Initialize();
        }
        
        // Called once per fixed frame
        private void FixedUpdate()
        {
            if(!_caughtPlayer) MoveEnemy();
        }
        
        // Initialize every variable
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
        
        // Move Enemy towards waypoint
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
        
        // Actions to perform after enemy collided with the player
        private IEnumerator KillPlayer()
        {   
            _caughtPlayer = true;
            _playerMovement.enabled = false;
            _playerCameraView.enabled = false;
            
            //TODO: Open death panel
            _levelLoader.LoadScene("Scene 2");

            yield return new WaitForSeconds(1);

            _playerMovement.enabled = true;
            _playerCameraView.enabled = true;
        }
        
        // Things to do on collision
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                StartCoroutine(KillPlayer());
        }
    }
}
