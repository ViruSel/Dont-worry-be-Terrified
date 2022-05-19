using UnityEngine;

namespace Mirror_Scripts
{
    public class SpawnEnemy : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        
        // Start is called before the first frame update
        private void Start()
        {
            enemy.SetActive(false);
        }

        // Update is called once per frame
        private void Update()
        {
            if (GetComponent<MirrorTeleport>().playerTeleported)
                enemy.SetActive(true);
        }
    }
}
