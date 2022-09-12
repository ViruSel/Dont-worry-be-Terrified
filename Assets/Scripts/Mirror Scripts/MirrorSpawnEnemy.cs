using UnityEngine;

namespace Mirror_Scripts
{
    public class MirrorSpawnEnemy : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;

        private MirrorTeleport mirrorTp;
        
        // Start is called before the first frame update
        private void Start()
        {
            enemy.SetActive(false);
            mirrorTp = GetComponent<MirrorTeleport>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (mirrorTp.playerTeleported)
                enemy.SetActive(true);
        }
    }
}
