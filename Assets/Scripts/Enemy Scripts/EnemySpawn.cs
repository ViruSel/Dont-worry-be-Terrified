using UnityEngine;

namespace Enemy_Scripts
{
    public class EnemySpawn : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        private GameObject enemy;

        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            enemy = GameObject.FindWithTag("Enemy");
            enemy.SetActive(false);
        }
        
        /// <summary>
        /// Spawns the enemy when the player enters the trigger
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
                enemy.SetActive(true);
        }
    }
}
