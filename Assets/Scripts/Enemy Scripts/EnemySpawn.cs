using UnityEngine;

namespace Enemy_Scripts
{
    public class EnemySpawn : MonoBehaviour
    {
        private GameObject enemy;

        private void Start()
        {
            enemy = GameObject.FindWithTag("Enemy");
            enemy.SetActive(false);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
                enemy.SetActive(true);
        }
    }
}
