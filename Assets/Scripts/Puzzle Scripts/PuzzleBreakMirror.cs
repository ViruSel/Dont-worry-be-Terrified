using System;
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

        private void Awake()
        {
            _mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
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
