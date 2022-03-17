using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI_Scripts
{
    /// <summary>
    /// Pause Menu Script
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        public static bool isPaused;
        
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject settingsMenu;
        
        private LevelLoader _levelLoader;

        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            isPaused = false;
            _levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && Time.timeSinceLevelLoad > 1f)
                if(isPaused)
                    Resume();
                else
                    Pause();
        }

        /// <summary>
        /// Pause Game Actions
        /// </summary>
        private void Pause()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isPaused = true;
        }

        /// <summary>
        /// Resume Game Actions
        /// </summary>
        public void Resume()
        {
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isPaused = false;
        }

        /// <summary>
        /// Open Settings Actions
        /// </summary>
        public void Settings()
        {
        
        }

        /// <summary>
        /// Exit to main menu action
        /// </summary>
        public void Exit()
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            _levelLoader.LoadScene("Menu");
            Destroy(this);
        }
    }
}
