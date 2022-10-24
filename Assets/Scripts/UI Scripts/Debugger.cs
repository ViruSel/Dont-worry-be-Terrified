using UnityEngine;

namespace UI_Scripts
{
    public class Debugger : MonoBehaviour
    {
        public static Debugger instance;
        public bool debugMode = true;       // To be global & changed from the inspector
        
        private void Awake()
        {
            Initialize();
        }

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
