using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    /// Display FPS Script
    [RequireComponent(typeof (Text))]
    public class FPSCounter : MonoBehaviour
    {
        // Variables
        private float _fpsNextPeriod;
        private int _fpsAccumulator;
        private int _currentFps;
        
        private Text _text;
        
        // Called before Start function
        private void Awake()
        {
            _text = GetComponent<Text>();
            _fpsNextPeriod = Time.realtimeSinceStartup + UIProperties.FpsMeasurePeriod;
        }
        
        // Update is called once per frame
        private void Update()
        {
            MeasureFPS();
        }
        
        // Measure FPS
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
