using Scene_Scripts;
using UnityEngine;

namespace Menu_Scripts
{
    // Setting up actions for Main menu buttons
    public class MenuButtonsAction : MonoBehaviour
    {
        // Variables
        private Camera _camera;
        private LevelLoader _levelLoader;
        
        // Called before Start function
        private void Awake()
        {
            _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            _levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
        }
        
        // Update is called once per frame
        private void Update()
        {
            CheckButtonPressed();
        }
        
        // Checking which button is pressed
        private void CheckButtonPressed()
        {
            if (!Input.GetMouseButtonUp(0)) return;

            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit)) return;
        
            if (hit.transform.Equals(null)) return;

            // Which button has which action
            switch (hit.transform.gameObject.name)
            {
                case "Start":
                    StartGame();
                    break;
                case "Settings":
                    Settings();
                    break;
                case "Exit":
                    ExitGame();
                    break;
            }
        }
        
        // Start game button action
        private void StartGame()
        {
            _levelLoader.LoadScene("Scene 1");
        }
        
        // Opening Settings menu
        private void Settings()
        {
            Debug.Log("Settings not implemented yet");
        }

        // Exiting the game
        private void ExitGame()
        {
            Application.Quit();
        }
    }
}
