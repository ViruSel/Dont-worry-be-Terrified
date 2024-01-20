using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI_Scripts
{
    // Settings Script
    public class Settings : MonoBehaviour
    {
        // Variables
        [Header("Audio")] 
        public AudioMixer audioMixer;

        [Header("Graphics")]
        private bool _isVsync;
        private bool _isFullScreen;
        
        private Resolution[] _resolutions;
        public Dropdown resolutionDropdown;
        
        // Called before Update function
        private void Start()
        {
            Initialize();
        }
        
        // Initialize settings
        private void Initialize()
        {
            // Graphics
            _isFullScreen = true;
            _isVsync = true;
            QualitySettings.vSyncCount = 1;
            InitializeResolution();
            ChangeVsync();
            SetQuality(6);
        } 
        
        // ========== Audio Settings ==========
        /// Set volume on slider
        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
        }
        
        // ========== Graphics Settings ==========
        // Initialize resolutions list
        private void InitializeResolution()
        {
            _resolutions = Screen.resolutions;

            var options = new List<string>();
            var currentResolutionIndex = 0;
            
            for (var i = 0; i < _resolutions.Length; i++)
            {
                var option = _resolutions[i].width + " x " + _resolutions[i].height +" @ "+ _resolutions[i].refreshRateRatio + "hz";
                options.Add(option);

                if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
                    currentResolutionIndex = i;
            }
            
            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        // Set desired resolution
        public void SetResolution(int resolutionIndex)
        {
            var resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, _isFullScreen);
        }
        
        // Change game graphics quality
        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }
        
        // Change game to fullscreen or windowed
        public void SetFullScreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
        
        // Changing Vsync ON/OFF
        public void ChangeVsync()
        {
            if (_isVsync)
            {
                _isVsync = false;
                QualitySettings.vSyncCount = 0;
            }
            else
            {
                _isVsync = true;
                QualitySettings.vSyncCount = 1;
            }
        }
    }
}
