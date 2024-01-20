using UnityEngine;

namespace UI_Scripts
{
    // Enable Debug UI
    public class Debugger : MonoBehaviour
    {
        // Variables
        public static Debugger instance;
        public bool debugMode = true;       // To be global & changed from the inspector
        
        // Called before Start function
        private void Awake()
        {
            Initialize();
        }

        // Initialize variables
        private void Initialize()
        {
            if(debugMode)
            {
                if (instance == null)
                    instance = this;
                else
                    Destroy(gameObject);
            }
            else
            {
                if (instance != null)
                    Destroy(gameObject);
            }
        }
    }
}
