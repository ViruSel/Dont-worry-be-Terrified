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

        public static bool Caught;
        
        private int _currentWaypoint;

        private LevelLoader _levelLoader;
        
        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            Caught = false;
            _levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            MoveEnemy();
        }

        /// <summary>
        /// Move Enemy
        /// </summary>
        private void MoveEnemy()
        {
            if (Caught) return;
            
            if (transform.position != waypoints[_currentWaypoint].position)
            {
                var position = transform.position;
                
                var pos = Vector3.MoveTowards(position, waypoints[_currentWaypoint].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
                
                var rotation = Quaternion.LookRotation(waypoints[_currentWaypoint].position - position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
            else
            {
                _currentWaypoint = (_currentWaypoint + 1) % waypoints.Length;
            }
        }

        /// <summary>
        /// Actions to perform after enemy collided with the player
        /// </summary>
        private void KillPlayer()
        {
            // Oped death panel
            _levelLoader.LoadScene("Scene 2");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {   
                Caught = true;
                KillPlayer();
            }
        }
    }
}
