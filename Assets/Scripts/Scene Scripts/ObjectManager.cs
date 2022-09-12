using UnityEngine;

namespace Scene_Scripts
{
    //TODO: Interface

    public class ObjectManager : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        private static GameObject[] _triggers;
        private static GameObject[] _tunnelObjects;
        private static GameObject[] _puzzleObjects;

        private void Start()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _triggers = GameObject.FindGameObjectsWithTag("Trigger");
            _puzzleObjects = GameObject.FindGameObjectsWithTag("Puzzle");
            _tunnelObjects = GameObject.FindGameObjectsWithTag("Tunnel");

            foreach (var tunnelObject in _tunnelObjects)
                tunnelObject.SetActive(false);
        }
        
        public static void DestroyPuzzleObjects()
        {
            foreach (var puzzleObject in _puzzleObjects)
                Destroy(puzzleObject);
        }
        
        public static void DestroyTriggers()
        {
            foreach (var trigger in _triggers)
                Destroy(trigger);
        }
        
        public static void ActivateTunnelObjects()
        {
            foreach (var tunnelObject in _tunnelObjects)
                tunnelObject.SetActive(true);
        }
    }
}
