using UnityEngine;

namespace Scene_Scripts
{
    public class DestroyObjects : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        private static GameObject[] _puzzleObjectsToDestroy;

        private void Start()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _puzzleObjectsToDestroy = GameObject.FindGameObjectsWithTag("Puzzle");
        }
        
        public static void DestroyPuzzleObjects()
        {
            foreach (var obj in _puzzleObjectsToDestroy)
            {
                Destroy(obj);
            }
        }
    }
}
