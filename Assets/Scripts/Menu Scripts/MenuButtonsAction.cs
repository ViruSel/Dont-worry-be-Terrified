using Scene_Scripts;
using UnityEngine;

namespace Menu_Scripts
{
    /// <summary>
    /// Setting up actions for Main menu buttons
    /// </summary>
    public class MenuButtonsAction : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        private Camera _camera;
        private LevelLoader _levelLoader;

        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            _levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            CheckButtonPressed();
        }

        /// <summary>
        /// Checking which button is pressed
        /// </summary>
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

        /// <summary>
        /// Start game button action
        /// </summary>
        private void StartGame()
        {
            _levelLoader.LoadScene("Scene 1");
        }

        /// <summary>
        /// Opening Settings menu
        /// </summary>
        private void Settings()
        {
            print("Settings not implemented yet");
        }

        /// <summary>
        /// Exiting the game
        /// </summary>
        private void ExitGame()
        {
            Application.Quit();
        }
    }
}
