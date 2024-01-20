using UnityEngine;

namespace Player_Scripts
{
    public class PlayerCastToObject : MonoBehaviour
    {
        public static float Distance;
        
        // Called once per frame
        private void Update()
        {
            CheckDistance();
        }
        
        // Cast a ray to find the distance between player and objects
        private void CheckDistance()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit))
                Distance = hit.distance;
        }
    }
}
