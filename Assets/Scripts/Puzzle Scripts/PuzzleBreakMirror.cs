using UnityEngine;

namespace Puzzle_Scripts
{
    public class PuzzleBreakMirror : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        private GameObject[] _mirrors;

        [SerializeField] private Material black;

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            if (PuzzleSolvedButton.AnimatedObjReference == "Mirror")
            {
                _mirrors = GameObject.FindGameObjectsWithTag("Mirror");

                /*
                foreach (var mirror in _mirrors)
                {
                    for(var i = 2; i < mirror.transform.childCount; i++)
                    {
                        mirror.transform.GetChild(i).GetComponent<Renderer>().material = black;
                    }
                }
                */
            }

            if (PuzzleSolvedButton.IsSolved)
            {
                foreach (var mirror in _mirrors)
                {
                    mirror.GetComponent<Animation>().Play();
                    mirror.GetComponent<AudioSource>().Play();
                    mirror.transform.GetChild(0).gameObject.SetActive(false); // Disable Teleportation collider
                    mirror.transform.GetChild(1).gameObject.SetActive(false); // Disable Non broken Mirror
                }
            }
        }
    }
}
