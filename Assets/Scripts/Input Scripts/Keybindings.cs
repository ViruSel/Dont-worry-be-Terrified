using UnityEngine;

namespace Input_Scripts
{
    // Creating Custom object "Keybindings"
    [CreateAssetMenu(fileName ="Keybindings", menuName ="Keybindings")]
    
    public class Keybindings : ScriptableObject
    {
        // Key actions class with their corresponding bind 
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
