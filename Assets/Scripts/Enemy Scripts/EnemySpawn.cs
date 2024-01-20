using UnityEngine;

namespace Enemy_Scripts
{
    public class EnemySpawn : MonoBehaviour
    {
        // Variables
        private GameObject enemy;
        private Enemy _enemyScript;
        
        // Called before the first frame update
        private void Start()
        {
            enemy = GameObject.FindWithTag("Enemy");
            _enemyScript = enemy.GetComponent<Enemy>();
            _enemyScript.enabled = false;
        }
        
        // Spawns the enemy when the player enters the trigger 
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
                _enemyScript.enabled = true;
        }
    }
}
