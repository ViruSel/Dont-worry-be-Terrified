namespace Player_Scripts
{
    public static class PlayerProperties
    {
        //TODO: JUMP_FALL_OFF_CURVE 

        // Movement
        public const float SmoothTime = 0.125f;            // Time it takes to reach target velocity (Between 0 -  1)
        public const int DefaultSpeed = 5;                 // Default speed of the player
        public const int WalkSpeed = 3;                    // Speed of the player when walking
        public const int RunSpeed = 6;                     // Speed of the player when running
        public const int AccelerationSpeed = 5;            // Speed of the player when accelerating
        public const float CrouchSpeed = 2.5f;             // Speed of the player when crouching
        public const int JumpMultiplier = 10;              // Multiplier for the jump force
        public const float Gravity = -9.81f;               // Gravity
        public const float GroundDistance = 0.4f;          // Distance from the player to the ground
        public const int CrouchingSpeed = 5;               // Speed of the crouching animation
        public const int SlopeForce = 6;                   // Force applied to the player when on a slope
        public const int SlopeLimitInAir = 91;             // Limit of the slope the player can walk on in the air
        public const int SlopeLimitOnGround = 45;          // Limit of the slope the player can walk on on the ground
        public const float SlopeForceRayLength = 1.5f;     // Length of the raycast used to detect slopes
        
        // Camera
        public const float CameraSmoothTime = 0f;          // Time it takes to reach target camera position (Between 0 -  1)
        public const int FovChangingSpeed = 5;             // Speed of the FOV changing
        public const int FovDifference = 15;               // Difference between the FOV when crouching/running/walking
        public const float FOVCorrection = 0.05f;          // Correction of the FOV when crouching or running
        public const float CameraClampUp = 89.999f;        // Clamp of the camera up
        public const float CameraClampDown = -89.999f;     // Clamp of the camera down
        
        // Cast to enemy
        public const float CastToEnemyDistance = 5f;       // Distance of the cast to enemy
        public const int VignetteChangingSpeed = 5;        // Speed of the vignette changing
        public const float VignetteDefaultValue = 0.25f;   // Default value of the vignette
        public const float VignetteNewValue = 0.5f;        // New value of the vignette when the player is close to an enemy
        public const float VignetteCorrection = 0.0005f;   // Correction of the vignette when the player is close to an enemy
    }
}   
