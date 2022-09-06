using UnityEngine;

namespace Player_Scripts
{
    public static class PlayerProperties
    {
        //TODO: JUMP_FALL_OFF_CURVE 

        // Movement
        public const float SMOOTH_TIME = 0.125f;            // Time it takes to reach target velocity (Between 0 -  1)
        public const int DEFAULT_SPEED = 5;                 // Default speed of the player
        public const int WALK_SPEED = 3;                    // Speed of the player when walking
        public const int RUN_SPEED = 6;                     // Speed of the player when running
        public const int ACCELERATION_SPEED = 5;            // Speed of the player when accelerating
        public const float CROUCH_SPEED = 2.5f;             // Speed of the player when crouching
        public const int JUMP_MULTIPLIER = 10;              // Multiplier for the jump force
        public const float GRAVITY = -9.81f;                // Gravity
        public const float GROUND_DISTANCE = 0.4f;          // Distance from the player to the ground
        public const int CROUCHING_SPEED = 5;               // Speed of the crouching animation
        public const int SLOPE_FORCE = 6;                   // Force applied to the player when on a slope
        public const int SLOPE_LIMIT_IN_AIR = 91;           // Limit of the slope the player can walk on in the air
        public const int SLOPE_LIMIT_ON_GROUND = 45;        // Limit of the slope the player can walk on on the ground
        public const float SLOPE_FORCE_RAY_LENGTH = 1.5f;   // Length of the raycast used to detect slopes
        
        // Camera
        public const float CAMERA_SMOOTH_TIME = 0f;         // Time it takes to reach target camera position (Between 0 -  1)
        public const int FOV_CHANGING_SPEED = 5;            // Speed of the FOV changing
        public const int FOV_DIFFERENCE = 15;               // Difference between the FOV when crouching or running
        public const float FOV_CORRECTION = 0.05f;          // Correction of the FOV when crouching or running
        public const float CAMERA_CLAMP_UP = 89.999f;       // Clamp of the camera up
        public const float CAMERA_CLAMP_DOWN = -89.999f;    // Clamp of the camera down
        
        // Cast to enemy
        public const float CAST_TO_ENEMY_DISTANCE = 5f;     // Distance of the cast to enemy
        public const int VIGNETTE_CHANGING_SPEED = 5;       // Speed of the vignette changing
        public const float VIGNETTE_DEFAULT_VALUE = 0.25f;  // Default value of the vignette
        public const float VIGNETTE_NEW_VALUE = 0.5f;       // New value of the vignette when the player is close to an enemy
        public const float VIGNETTE_CORRECTION = 0.0005f;   // Correction of the vignette when the player is close to an enemy
    }
}   
