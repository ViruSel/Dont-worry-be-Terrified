using System.Collections.Generic;
using UnityEngine;

namespace Scene_Scripts
{
    // Note: This script isn't attached to any GameObject in the scene.
    // Contains methods for manipulating the existence of GameObjects in the scene.
    public class ObjectManager : MonoBehaviour
    {
        // Destroy list objects
        public static void DestroyObjects(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                Destroy(gameObject);
            }
        }

        // Disable list of objects
        public static void DisableObjects(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetActive(false);
            }
        }
        
        // Activate list of objects
        public static void ActivateObjects(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
