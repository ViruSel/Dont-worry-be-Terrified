using UnityEngine;

namespace Mirror_Scripts
{
    public class SpawnEnemy : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;

        private MirrorTeleport mirrorTeleporter;
        
        // Start is called before the first frame update
        private void Start()
        {
            enemy.SetActive(false);
            mirrorTeleporter = GetComponent<MirrorTeleport>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (mirrorTeleporter.playerTeleported)
                enemy.SetActive(true);
        }
    }
}
