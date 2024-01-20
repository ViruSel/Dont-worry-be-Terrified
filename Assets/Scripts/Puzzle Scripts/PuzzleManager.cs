using System.Collections.Generic;
using UnityEngine;

namespace Puzzle_Scripts
{
    // Note: This script isn't attached to any GameObject in the scene.
    // Contains the common properties of all the puzzles.
    public class PuzzleManager : MonoBehaviour
    {   
        // Constant Variables
        public const int DistanceToButton = 4;
        
        // Check if all the buttons are pressed
        public static bool CheckIfAllButtonsPressed(IEnumerable<GameObject> buttons)
        {
            foreach (var button in buttons)
            {
                if (!button.GetComponent<PuzzleButton>().isPressed) return false;
            }

            return true;
        }
        
        // Check if the buttons are pressed in correct order
        public static bool CheckPassword(IReadOnlyList<int> password, IReadOnlyList<int> passwordReceived)
        {
            for (var i = 0; i < password.Count; i++)
            {
                if (password[i] != passwordReceived[i])
                {
                    return false;
                }
            }

            return true;
        }

        // Reset button pressing order
        public static void ResetButtons(IEnumerable<GameObject> buttons)
        {
            foreach (var button in buttons)
            {
                button.GetComponent<PuzzleButton>().ResetButton();
            }
        }

        // Send password to the correct puzzle
        public static void SendPassword(int puzzleNumber, int password)
        {
            // In case of more puzzles, we can use switch case to send the password to the correct script
            switch (puzzleNumber)
            {
                case 1:
                    // Do nothing
                    break;
                case 2:
                    // Send password to puzzle 2
                    Puzzle2SolvingButton.passwordReceived.Add(password);
                    break;
                default:
                    Debug.LogWarning("Puzzle number is not valid");
                    break;
            }
        }
        
    }
}
