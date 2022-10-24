using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    /// <summary>
    /// Display FPS Script
    /// </summary>
    [RequireComponent(typeof (Text))]
    public class FPSCounter : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>

        private float _fpsNextPeriod;
        private int _fpsAccumulator;
        private int _currentFps;
        
        private Text _text;
        
        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            _text = GetComponent<Text>();
            _fpsNextPeriod = Time.realtimeSinceStartup + UIProperties.FpsMeasurePeriod;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            MeasureFPS();
        }

        /// <summary>
        /// Measure FPS
        /// </summary>
        private void MeasureFPS()
        {
            // Measure average frames per second
            _fpsAccumulator++;
            
            if (Time.realtimeSinceStartup <= _fpsNextPeriod) return;
            
            // Math
            _currentFps = (int) (_fpsAccumulator/UIProperties.FpsMeasurePeriod);
            _fpsAccumulator = 0;
            _fpsNextPeriod += UIProperties.FpsMeasurePeriod;
            
            // Display FPS
            _text.text = string.Format(UIProperties.FPSString, _currentFps);
        }
    }
}
