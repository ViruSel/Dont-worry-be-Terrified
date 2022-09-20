using System.Collections.Generic;
using UnityEngine;

namespace Scene_Scripts
{
    /// <summary>
    /// Note: This script isn't attached to any GameObject in the scene.
    /// Contains methods for manipulating the existence of GameObjects in the scene.
    /// </summary>
    public class ObjectManager : MonoBehaviour
    {
        public static void DestroyObjects(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                Destroy(gameObject);
            }
        }

        public static void DisableObjects(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetActive(false);
            }
        }
        
        public static void ActivateObjects(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
