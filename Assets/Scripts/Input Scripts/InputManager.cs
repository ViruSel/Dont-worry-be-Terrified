using System.Linq;
using UnityEngine;

namespace Input_Scripts
{
    /// <summary>
    /// Custom Input Manager
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        // Variables
        [SerializeField] private Keybindings keybindings;
        
        public static InputManager Instance;

        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            // Making sure this persists in the whole game
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// Get key Code for a specific action
        /// </summary>
        /// <param name="keyBindingAction"></param>
        /// <returns> Key for corresponding action </returns>
        public KeyCode GetKeyForAction(KeybindingActions keyBindingAction)
        {
            return (from keybindingCheck in keybindings.keybindingChecks where keybindingCheck.keybindingAction == keyBindingAction select keybindingCheck.keycode).FirstOrDefault();
        }

        /// <summary>
        /// Replaces function Input.GetKey
        /// </summary>
        /// <param name="keyBindingAction"></param>
        /// <returns> if Key is pressed or not </returns>
        public bool GetKey(KeybindingActions keyBindingAction)
        {
            return (from keybindingCheck in keybindings.keybindingChecks where keybindingCheck.keybindingAction == keyBindingAction select Input.GetKey(keybindingCheck.keycode)).FirstOrDefault();
        }
        
        /// <summary>
        /// Replaces function Input.GetKeyDown
        /// </summary>
        /// <param name="keyBindingAction"></param>
        /// <returns> if Key is pressed or not </returns>
        public bool GetKeyDown(KeybindingActions keyBindingAction)
        {
            return (from keybindingCheck in keybindings.keybindingChecks where keybindingCheck.keybindingAction == keyBindingAction select Input.GetKeyDown(keybindingCheck.keycode)).FirstOrDefault();
        }

        /// <summary>
        /// Replaces function Input.GetKeyUp
        /// </summary>
        /// <param name="keyBindingAction"></param>
        /// <returns> if Key is released or not </returns>
        public bool GetKeyUp(KeybindingActions keyBindingAction)
        {
            return (from keybindingCheck in keybindings.keybindingChecks where keybindingCheck.keybindingAction == keyBindingAction select Input.GetKeyUp(keybindingCheck.keycode)).FirstOrDefault();
        }

    }
}
