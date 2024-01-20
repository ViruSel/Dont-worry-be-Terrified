using System.Linq;
using UnityEngine;

namespace Input_Scripts
{
    // Custom Input Manager
    public class InputManager : MonoBehaviour
    {

        // Variables
        [SerializeField] private Keybindings keybindings;
        
        public static InputManager Instance;

        // Called before Start function
        private void Awake()
        {
            // Making sure this persists in the whole game
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            DontDestroyOnLoad(this);
        }

        // Get key Code for a specific action
        public KeyCode GetKeyForAction(KeybindingActions keyBindingAction)
        {
            return (from keybindingCheck in keybindings.keybindingChecks where keybindingCheck.keybindingAction == keyBindingAction select keybindingCheck.keycode).FirstOrDefault();
        }
        
        // Replaces function Input.GetKey
        public bool GetKey(KeybindingActions keyBindingAction)
        {
            return (from keybindingCheck in keybindings.keybindingChecks where keybindingCheck.keybindingAction == keyBindingAction select Input.GetKey(keybindingCheck.keycode)).FirstOrDefault();
        }

        // Replaces function Input.GetKeyDown
        public bool GetKeyDown(KeybindingActions keyBindingAction)
        {
            return (from keybindingCheck in keybindings.keybindingChecks where keybindingCheck.keybindingAction == keyBindingAction select Input.GetKeyDown(keybindingCheck.keycode)).FirstOrDefault();
        }
        
        // Replaces function Input.GetKeyUp
        public bool GetKeyUp(KeybindingActions keyBindingAction)
        {
            return (from keybindingCheck in keybindings.keybindingChecks where keybindingCheck.keybindingAction == keyBindingAction select Input.GetKeyUp(keybindingCheck.keycode)).FirstOrDefault();
        }

    }
}
