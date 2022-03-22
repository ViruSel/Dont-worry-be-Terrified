using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI_Scripts
{
    /// <summary>
    /// Settings Script
    /// </summary>
    public class Settings : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [Header("Audio")] 
        public AudioMixer audioMixer;

        [Header("Graphics")]
        private bool _isVsync;
        private bool _isFullScreen;
        
        private Resolution[] _resolutions;
        public Dropdown resolutionDropdown;

        /// <summary>
        /// Called before Update function
        /// </summary>
        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Initialize settings
        /// </summary>
        private void Initialize()
        {
            // Graphics
            _isFullScreen = true;
            _isVsync = true;
            InitializeResolution();
            ChangeVsync();
            SetQuality(6);
        } 
        
        // ========== Audio Settings ==========
        /// <summary>
        /// Set volume on slider
        /// </summary>
        /// <param name="volume"></param>
        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
        }
        
        // ========== Graphics Settings ==========
        /// <summary>
        /// Initialize resolutions list
        /// </summary>
        private void InitializeResolution()
        {
            _resolutions = Screen.resolutions;

            var options = new List<string>();
            var currentResolutionIndex = 0;
            
            for (var i = 0; i < _resolutions.Length; i++)
            {
                var option = _resolutions[i].width + " x " + _resolutions[i].height +" @ "+ _resolutions[i].refreshRate + "hz";
                options.Add(option);

                if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                    currentResolutionIndex = i;
            }
            
            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        public void SetResolution(int resolutionIndex)
        {
            var resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, _isFullScreen);
        }
        
        /// <summary>
        /// Change game graphics quality
        /// </summary>
        /// <param name="qualityIndex"> Quality profile </param>
        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        /// <summary>
        /// Change game to fullscreen or windowed
        /// </summary>
        /// <param name="isFullscreen"></param>
        public void SetFullScreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
        
        /// <summary>
        /// Changing Vsync ON/OFF
        /// </summary>
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
