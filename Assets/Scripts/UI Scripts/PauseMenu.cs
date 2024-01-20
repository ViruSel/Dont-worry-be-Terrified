using System;
using Audio_Scripts;
using Scene_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI_Scripts
{
    // Pause Menu Script
    public class PauseMenu : MonoBehaviour
    {
        // Variables
        public static bool isPaused;
        
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject settingsMenu;
        [SerializeField] private GameObject crosshair;
        
        private LevelLoader _levelLoader;
        
        // Called before the first frame update
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            isPaused = false;
            _levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
        }
        
        // Update is called once per frame
        private void Update()
        {
            CheckPaused();
        }

        // Check if game is paused
        private void CheckPaused()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) || Time.timeSinceLevelLoad < 1f) return;
            
            if (isPaused)
                Resume();
            else
                Pause();
        }
        
        // Pause Game Actions
        private void Pause()
        {
            pauseMenu.SetActive(true);
            crosshair.SetActive(false);
            
            Time.timeScale = 0f;
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            isPaused = true;
        }
        
        // Resume Game Actions
        public void Resume()
        {
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(false);
            crosshair.SetActive(true);
            
            Time.timeScale = 1f;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            isPaused = false;
        }
        
        // Exit to main menu action
        public void Exit()
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            _levelLoader.LoadScene("Menu");
            Destroy(this);
        }
    }
}
