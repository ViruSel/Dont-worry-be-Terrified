using UnityEngine;

namespace Player_Scripts
{
    public class CastToObject : MonoBehaviour
    {
        public static float Distance;

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            CheckDistance();
        }

        /// <summary>
        /// Cast a ray to find the distance between player and objects
        /// </summary>
        private void CheckDistance()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit))
                Distance = hit.distance;
        }
    }
}
