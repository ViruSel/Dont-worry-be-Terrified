using UnityEngine;

namespace Puzzle_Scripts
{
    public class PuzzleDisablesObjects : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private GameObject[] objectsToDisable;

        // Update is called once per frame
        void Update()
        {
            if (PuzzleSolvedButton.IsSolved)
            {
                foreach (var obj in objectsToDisable)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
