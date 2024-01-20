using System.Collections;
using Input_Scripts;
using UI_Scripts;
using UnityEngine;

namespace Player_Scripts
{
    // Movement script
    public class PlayerMovement : MonoBehaviour
    {
        // Variables
        [Header("Movement Settings")]
        [SerializeField] private float defaultSpeed;
        [SerializeField] [Range(0f, 1f)] private float moveSmoothTime;

        [Header("Walking Settings")]
        [SerializeField] private float walkSpeed;

        [Header("Running Settings")]
        [SerializeField] private float runSpeed;
        [SerializeField] private float acceleration;

        [Header("Jumping Settings")]
        [SerializeField] private float jumpMultiplier;
        [SerializeField] private AnimationCurve jumpFallOff;

        [Header("Crouching Settings")]
        [SerializeField] private float crouchSpeed;

        [Header("Ground Check")]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private bool isGrounded;

        private float _movementSpeed;
        private float _velocityY;
        private bool _isJumping;
        private bool _wishJump;
        private bool _isWalking;
        private bool _isRunning;
        private bool _isCrouching;

        private Vector3 _velocity;
        private Vector2 _inputDir;
        private Vector2 _currentDir = Vector2.zero;
        private Vector2 _currentDirVelocity = Vector2.zero;

        private Camera _camera;
        private CharacterController _characterController;
        private Transform _groundCheck;
        private InputManager _inputManager;

        public PlayerStates playerState;
        
        // Called before Start function
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            _groundCheck = transform.Find("GroundCheck");
        }
        
        // Called before the first frame update
        private void Start()
        {
            Initialize();
        }
        
        // Called once per frame
        private void Update()
        {
            CheckPauseMenu();
            MovePlayer();
            PlayerMovementSettings();
        }
        
        // Called every fixed frame
        private void FixedUpdate()
        {
            InitializeBindings();
        }
        
        // Initialize variables
        private void Initialize()
        {
            _inputManager = InputManager.Instance;
            
            moveSmoothTime = PlayerProperties.SmoothTime;
            defaultSpeed = PlayerProperties.DefaultSpeed;
            walkSpeed = PlayerProperties.WalkSpeed;
            runSpeed = PlayerProperties.RunSpeed;
            acceleration = PlayerProperties.AccelerationSpeed;
            jumpMultiplier = PlayerProperties.JumpMultiplier;
            crouchSpeed = PlayerProperties.CrouchSpeed;
        }
        
        // Initialize Bindings
        private void InitializeBindings()
        {
            _wishJump = _inputManager.GetKey(KeybindingActions.Jump);
            _isWalking = _inputManager.GetKey(KeybindingActions.Walk);
            _isRunning = _inputManager.GetKey(KeybindingActions.Run);
            _isCrouching = _inputManager.GetKey(KeybindingActions.Crouch);
        }
        
        // Moving the Player
        private void MovePlayer()
        {
            // Movement input
            _inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _inputDir.Normalize();

            // Check if player is moving
            if (_inputDir.x == 0 && _inputDir.y == 0)
                playerState = PlayerStates.Standing;
            else
                playerState = PlayerStates.Moving;
            
            // TODO - Block air movement
            // Player movement smoothing
            _currentDir = Vector2.SmoothDamp(_currentDir, _inputDir, ref _currentDirVelocity, moveSmoothTime);
            
            // Resetting velocity if player was grounded
            if (_characterController.isGrounded) _velocityY = 0f;

            // Applying gravity
            _velocityY += (PlayerProperties.Gravity-2) * Time.deltaTime;
            
            // Movement math
            var transformNew = transform;
            _velocity = (transformNew.forward * _currentDir.y + transformNew.right * _currentDir.x) * _movementSpeed + Vector3.up * _velocityY;
            
            // Move player
            _characterController.Move(_velocity * Time.deltaTime);
        }
        
        // Player Movement Settings
        private void PlayerMovementSettings()
        {
            // Ground check
            isGrounded = Physics.CheckSphere(_groundCheck.position, PlayerProperties.GroundDistance, groundMask);

            if (!isGrounded) playerState = PlayerStates.InAir;

            // Update movement speed & state based on action
            if (_isCrouching)
            {
                UpdateMovementSpeed(crouchSpeed);
                playerState = PlayerStates.Crouching;
                isGrounded = true; // Quick fix
            }
            else if (_isRunning)
            {
                if(_inputDir.x != 0 || _inputDir.y != 0)        // Don't change player state if player is not moving
                    if (_inputDir.y > 0f)                       // Check if player is moving forward
                        playerState = PlayerStates.Running;

                UpdateMovementSpeed(playerState == PlayerStates.Running ? runSpeed : defaultSpeed);
            }
            else if (_isWalking)
            {
                if(_inputDir.x != 0 || _inputDir.y != 0)        // Don't change player state if player is not moving
                    playerState = PlayerStates.Walking;
                
                UpdateMovementSpeed(walkSpeed);
            }
            else
                UpdateMovementSpeed(defaultSpeed);
            
            // Crouch Action
            if (_isCrouching)
                CrouchEvent();
            else
                StandUpEvent();
            
            // Jump Action
            if (_wishJump && !_isJumping)
            {
                _isJumping = true; 
                playerState = PlayerStates.InAir;
                StartCoroutine(JumpEvent());
            }

            // Slope Action
            if (OnSlope()) SlopeEvent();
        }
        
        // Update movement speed
        private void UpdateMovementSpeed(float speed)
        {
            _movementSpeed = Mathf.Lerp(_movementSpeed, speed, Time.deltaTime * acceleration);
            
            if (_movementSpeed - 0.025f <= speed) _movementSpeed = speed;
        }
        
        // Jump Event
        private IEnumerator JumpEvent()
        {
            _characterController.slopeLimit = PlayerProperties.SlopeLimitInAir;

            float timeInAir = 0;

            do
            {
                var jumpForce = jumpFallOff.Evaluate(timeInAir);
                _characterController.Move(Vector3.up * (jumpForce * jumpMultiplier * Time.deltaTime));
                timeInAir += Time.deltaTime;
                yield return null;

            } while (!_characterController.isGrounded && _characterController.collisionFlags != CollisionFlags.Above);

            _characterController.slopeLimit = PlayerProperties.SlopeLimitOnGround;

            _isJumping = false;
        }
        
        // Crouch Event
        private void CrouchEvent()
        {
            _characterController.height = 1;
            _characterController.center = new Vector3(0, -0.5f, 0);
            _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, Vector3.up * 0.10f, PlayerProperties.CrouchSpeed * Time.deltaTime);
            //transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f,1f/2,1f), CrouchingSpeed * Time.deltaTime);
        }
        
        // TODO ceiling detection
        /// <summary>
        /// Stand Up Event
        /// </summary>
        private void StandUpEvent()
        {
            if (!Physics.Raycast(_camera.transform.position + Vector3.up, Vector3.up, out _, .5f))
            {
                _characterController.height = 2;
                _characterController.center = new Vector3(0, 0f, 0);
                _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, Vector3.up * 0.66f, PlayerProperties.CrouchingSpeed * Time.deltaTime);
                //transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f,1f,1f), CrouchingSpeed * Time.deltaTime);
            }   
        }
        
        // Verify if character is on a slope
        private bool OnSlope()
        {
            // If we are on a slope, stick to it (stairs)
            if (!Physics.Raycast(transform.position, Vector3.down, out var hit,
                    _characterController.height / 2 * PlayerProperties.SlopeForceRayLength)) return false;
            
            return hit.normal != Vector3.up;
        }
        
        // If player is on slope, stick him to it
        private void SlopeEvent()
        {
            if (_isJumping || playerState == PlayerStates.InAir) return;
            
            if (_currentDir.y != 0 || _currentDir.x != 0)           // Don't stick player to slope if he is not moving
                _characterController.Move(Vector3.down * _characterController.height / 2 * (PlayerProperties.SlopeForce * Time.deltaTime));
        }
        
        // Disable camera while pause menu is open
        private void CheckPauseMenu()
        {
            _camera.GetComponent<PlayerCameraView>().enabled = !PauseMenu.isPaused;
        }
        
        // Making player interact with physics objects
        private void PushPhysicsObject(ControllerColliderHit hit)
        {
            if(hit.gameObject.CompareTag("Enemy")) return;
            
            var body = hit.collider.attachedRigidbody;
            
            if (body == null || body.isKinematic) return;
            if (hit.moveDirection.y < -0.3f) return;
            
            body.velocity = hit.moveDirection * _velocity.magnitude;
        }
        
        /// Hitting other colliders
        private void OnControllerColliderHit( ControllerColliderHit hit )
        {
            // Check if the object is a box to push it around
            if(hit.gameObject.CompareTag("Box"))
                PushPhysicsObject(hit);
        }
    }
}
