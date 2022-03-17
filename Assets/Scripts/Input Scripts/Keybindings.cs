using UnityEngine;

namespace Input_Scripts
{
    /// <summary>
    /// Creating Custom object "Keybindings"
    /// </summary>
    [CreateAssetMenu(fileName ="Keybindings", menuName ="Keybindings")]
    
    public class Keybindings : ScriptableObject
    {
        /// <summary>
        /// Key actions class with their corresponding bind 
        /// </summary>
        [System.Serializable]
        public class KeybindingCheck
        {
            public KeybindingActions keybindingAction;
            public KeyCode keycode;
        }

        // Keybindings Array
        public KeybindingCheck[] keybindingChecks;
    }
}
