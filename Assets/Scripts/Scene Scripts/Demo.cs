using Player_Scripts;
using UnityEngine;

namespace Scene_Scripts
{
    // End of the demo
    public class Demo : MonoBehaviour
    {
        // Variables
        [SerializeField] private string levelName;
        [SerializeField] private LevelLoader levelLoader;
        [SerializeField] private GameObject player;

        // Start is called before the first frame update
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();

            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponentInChildren<PlayerCameraView>().enabled = false;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // Go back to the menu on exit
        public void Exit()
        {
            levelLoader.LoadScene(levelName);
            Destroy(this);
        }
    }
}
