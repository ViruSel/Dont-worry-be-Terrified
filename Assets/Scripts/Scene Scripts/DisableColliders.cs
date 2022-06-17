using UnityEngine;

namespace Scene_Scripts
{
    public class DisableColliders : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private GameObject[] objectsToDisable;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

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
