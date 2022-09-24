using System.Collections.Generic;
using UnityEngine;

namespace Puzzle_Scripts
{
    /// <summary>
    /// Note: This script isn't attached to any GameObject in the scene.
    /// Contains the common properties of all the puzzles.
    /// </summary>
    public class PuzzleManager : MonoBehaviour
    {   
        /// <summary>
        /// Constant Variables
        /// </summary>
        public const int DistanceToButton = 4;
        
        /// <summary>
        /// Check if all the buttons are pressed
        /// </summary>
        /// <returns></returns>
        public static bool CheckIfAllButtonsPressed(IEnumerable<GameObject> buttons)
        {
            foreach (var button in buttons)
            {
                if (!button.GetComponent<PuzzleButton>().isPressed) return false;
            }

            return true;
        }
        
        /// <summary>
        /// Check if the buttons are pressed in correct order
        /// </summary>
        /// <returns></returns>
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

        public static void ResetButtons(IEnumerable<GameObject> buttons)
        {
            foreach (var button in buttons)
            {
                button.GetComponent<PuzzleButton>().ResetButton();
            }
        }

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
