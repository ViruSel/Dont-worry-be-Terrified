using UnityEngine;

namespace Main_Menu_Scripts
{
    public static class MainMenuProperties
    {
        // Camera Properties
        public const float MouseSensitivity = 1.3f;         // Mouse sensitivity
        public const int MouseSmoothTime = 1;               // Mouse smoothing time (Between 0 and 1)
        public const float FieldOfView = 70f;               // The field of view of the camera           
        public const int ClampAngleUp = 25;                 // The maximum angle the camera can look up            
        public const int ClampAngleDown = -10;              // The maximum angle the camera can look down
        public const int DefaultClampAngleLeft = -15;       // The default angle the camera can look left
        public const int DefaultClampAngleRight = 15;       // The default angle the camera can look right
    }
}
