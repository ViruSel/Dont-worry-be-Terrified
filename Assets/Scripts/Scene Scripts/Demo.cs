using Player_Scripts;
using UnityEngine;

namespace Scene_Scripts
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] private string levelName;
        [SerializeField] private LevelLoader levelLoader;
        [SerializeField] private GameObject player;

        // Start is called before the first frame update
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();

            player.GetComponent<Movement>().enabled = false;
            player.GetComponentInChildren<CameraView>().enabled = false;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void Exit()
        {
            levelLoader.LoadScene("Menu");
            Destroy(this);
        }
    }
}
