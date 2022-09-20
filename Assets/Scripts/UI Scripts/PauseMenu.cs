using System;
using Audio_Scripts;
using Scene_Scripts;
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
        public static bool IsPaused;
        
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject settingsMenu;
        [SerializeField] private GameObject crosshair;
        
        private LevelLoader _levelLoader;

        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            IsPaused = false;
            _levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            CheckPaused();
        }

        private void CheckPaused()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) || !(Time.timeSinceLevelLoad > 1f)) return;
            
            if (IsPaused)
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
            crosshair.SetActive(false);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            IsPaused = true;
        }

        /// <summary>
        /// Resume Game Actions
        /// </summary>
        public void Resume()
        {
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(false);
            crosshair.SetActive(true);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            IsPaused = false;
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
