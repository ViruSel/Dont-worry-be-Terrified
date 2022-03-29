using UnityEngine;

namespace Scene_Scripts
{
    public class PuzzleSync : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        private GameObject[] _objectsToSync;
        private GameObject[] _illusionObjectsToSync;

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            if (PuzzleSolvedButton.AnimatedObjReference == "Mirror")
            {
                _objectsToSync = GameObject.FindGameObjectsWithTag("Mirror");
                _illusionObjectsToSync = GameObject.FindGameObjectsWithTag("Illusion");
            }

            if (PuzzleSolvedButton.IsSolved)
            {
                foreach (var obj in _objectsToSync)
                {
                    obj.GetComponent<Animation>().Play();
                    obj.GetComponent<AudioSource>().Play();
                    obj.transform.GetChild(0).gameObject.SetActive(false); // Disable Non broken Mirror
                    obj.transform.GetChild(1).gameObject.SetActive(false); // Disable Teleportation collider
                }

                foreach (var illusion in _illusionObjectsToSync)
                {
                    illusion.transform.GetChild(0).gameObject.SetActive(false); // Disable Teleportation collider
                    illusion.transform.GetChild(1).gameObject.SetActive(false); // Disable Door Colliders
                }
            }
        }
    }
}
