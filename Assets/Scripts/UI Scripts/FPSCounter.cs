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
        private const float FPSMeasurePeriod = 0.5f;
        private const string Display = "{0} FPS";
        
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
            _fpsNextPeriod = Time.realtimeSinceStartup + FPSMeasurePeriod;
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
            
            if (!(Time.realtimeSinceStartup > _fpsNextPeriod)) return;
            
            // Math
            _currentFps = (int) (_fpsAccumulator/FPSMeasurePeriod);
            _fpsAccumulator = 0;
            _fpsNextPeriod += FPSMeasurePeriod;
            
            // Display FPS
            _text.text = string.Format(Display, _currentFps);
        }
    }
}
