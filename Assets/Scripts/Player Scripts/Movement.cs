using System.Collections;
using Input_Scripts;
using UI_Scripts;
using UnityEngine;

namespace Player_Scripts
{
    /// <summary>
    /// Movement script
    /// </summary>
    public class Movement : MonoBehaviour
    {
        // Variables
        [Header("Movement Settings")] 
        [SerializeField] private bool canMoveInAir;
        [SerializeField] private float velocityY;
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
        [SerializeField] private float crouchHeight;

        [Header("Ground Check")]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private bool isGrounded;

        private const float Gravity = -9.81f;
        private const float CrouchingSpeed = 5f;
        private const float StandingUpSpeed = 5f;
        private const float SlopeForce = 3;
        private const float SlopeForceRayLenght = 1.5f;
        private const float GroundDistance = 0.4f;
        
        [SerializeField] private float _movementSpeed;
        private float _defaultCameraFOV;
        private bool _isJumping;
        private bool _wishJump;
        private bool _isWalking;
        private bool _isRunning;
        private bool _isCrouching;
        
        private RaycastHit _slopeHit;
        private Vector3 _moveDirection;
        private Vector3 _velocity;
        private Vector2 _inputDir;
        private Vector2 _currentDir = Vector2.zero;
        private Vector2 _currentDirVelocity = Vector2.zero;

        private Camera _camera;
        private CharacterController _characterController;
        private Transform _groundCheck;
        private InputManager _inputManager;

        public States playerState;
        
        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            _groundCheck = transform.Find("GroundCheck");
        }

        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            _inputManager = InputManager.Instance;
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            CheckPauseMenu();
            MovePlayer();
            PlayerMovementSettings();
        }

        /// <summary>
        /// Called every fixed frame
        /// </summary>
        private void FixedUpdate()
        {
            InitializeBindings();
        }

        /// <summary>
        /// Initialize Bindings
        /// </summary>
        private void InitializeBindings()
        {
            _wishJump = _inputManager.GetKey(KeybindingActions.Jump);
            _isWalking = _inputManager.GetKey(KeybindingActions.Walk);
            _isRunning = _inputManager.GetKey(KeybindingActions.Run);
            _isCrouching = _inputManager.GetKey(KeybindingActions.Crouch);
        }

        /// <summary>
        /// Moving the Player
        /// </summary>
        private void MovePlayer()
        {
            // Movement input
            _inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _inputDir.Normalize();

            // Find out if player is moving
            if (_inputDir.x == 0 && _inputDir.y == 0)
                playerState = States.Standing;
            else
                playerState = States.Moving;
            
            // TODO - Block air movement
            // Player movement smoothing
            _currentDir = Vector2.SmoothDamp(_currentDir, _inputDir, ref _currentDirVelocity, moveSmoothTime);
            
            // Resetting velocity if player was grounded
            if (_characterController.isGrounded) velocityY = 0f;

            // Applying gravity
            velocityY += (Gravity-2) * Time.deltaTime;
            
            // Movement math
            var transformNew = transform;
            _velocity = (transformNew.forward * _currentDir.y + transformNew.right * _currentDir.x) * _movementSpeed + Vector3.up * velocityY;
            
            // Move player
            _characterController.Move(_velocity * Time.deltaTime);
        }
        
        /// <summary>
        /// Player Movement Settings
        /// </summary>
        private void PlayerMovementSettings()
        {
            // Ground check
            isGrounded = Physics.CheckSphere(_groundCheck.position, GroundDistance, groundMask);

            if (!isGrounded) playerState = States.InAir;

            // Update movement speed & state based on action
            if (_isCrouching)
            {
                UpdateMovementSpeed(crouchSpeed);
                playerState = States.Crouching;
                isGrounded = true;
            }
            else if (_isRunning)
            {
                // Prevent player from running backwards
                if(_inputDir.x != 0 || _inputDir.y != 0) 
                    if (_inputDir.y > -0f)
                        playerState = States.Running;

                UpdateMovementSpeed(playerState == States.Running ? runSpeed : defaultSpeed);
            }
            else if (_isWalking)
            {
                UpdateMovementSpeed(walkSpeed);
                playerState = States.Walking;
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
                playerState = States.InAir;
                StartCoroutine(JumpEvent());
            }

            // Slope Action
            if (OnSlope()) SlopeEvent();
        }

        /// <summary>
        /// Update movement speed
        /// </summary>
        /// <param name="speed"></param>
        /// <returns> Movement Speed </returns>
        private void UpdateMovementSpeed(float speed)
        {
            _movementSpeed = Mathf.Lerp(_movementSpeed, speed, Time.deltaTime * acceleration);
            
            if (_movementSpeed - 0.025f <= speed) _movementSpeed = speed;
        }

        /// <summary>
        /// Jump Event
        /// </summary>
        private IEnumerator JumpEvent()
        {
            _characterController.slopeLimit = 90f;

            float timeInAir = 0;

            do
            {
                var jumpForce = jumpFallOff.Evaluate(timeInAir);
                _characterController.Move(Vector3.up * (jumpForce * jumpMultiplier * Time.deltaTime));
                timeInAir += Time.deltaTime;
                yield return null;

            } while (!_characterController.isGrounded && _characterController.collisionFlags != CollisionFlags.Above);

            _characterController.slopeLimit = 45f;

            _isJumping = false;
        }

        // TODO - Make transform size small
        /// <summary>
        /// Crouch Event
        /// </summary>
        private void CrouchEvent()
        {
            _characterController.height = 1;
            _characterController.center = new Vector3(0, -0.5f, 0);
            _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, Vector3.up * 0.10f, CrouchingSpeed * Time.deltaTime);
            //transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f,1f/2,1f), CrouchingSpeed * Time.deltaTime);
        }
        
        // TODO - Make transform size big + ceiling detection
        /// <summary>
        /// Stand Up Event
        /// </summary>
        private void StandUpEvent()
        {
            RaycastHit hit;
            if (!Physics.Raycast(_camera.transform.position + Vector3.up, Vector3.up, out hit, 1.0f))
            {
                _characterController.height = 2;
                _characterController.center = new Vector3(0, 0f, 0);
                _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, Vector3.up * 0.66f, CrouchingSpeed * Time.deltaTime);
                //transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f,1f,1f), CrouchingSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// Verify if character is on a slope
        /// </summary>
        /// <returns>
        /// true = on slope
        /// false = not on slope
        /// </returns>
        private bool OnSlope()
        {
            if (!Physics.Raycast(transform.position, Vector3.down, out var hit,
                    _characterController.height / 2 * SlopeForceRayLenght)) return false;
            
            return hit.normal != Vector3.up;
        }

        /// <summary>
        /// If player is on slope, stick him to it
        /// </summary>
        private void SlopeEvent()
        {
            if (_currentDir.y != 0 || _currentDir.x != 0) 
                _characterController.Move(Vector3.down * _characterController.height / 2 * (SlopeForce * Time.deltaTime));
        }

        /// <summary>
        /// Disable camera while pause menu is open
        /// </summary>
        private void CheckPauseMenu()
        {
            _camera.GetComponent<CameraView>().enabled = !PauseMenu.IsPaused;
        }

        /// <summary>
        /// Making player interact with physics objects
        /// </summary>
        /// <param name="hit"> Physics object </param>
        private void PushPhysicsObjects(ControllerColliderHit hit)
        {
            var body = hit.collider.attachedRigidbody;
            
            if (body == null || body.isKinematic) return;
            if (hit.moveDirection.y < -0.3f) return;
            
            body.velocity = hit.moveDirection * _velocity.magnitude;
        }

        /// <summary>
        /// Hitting other colliders
        /// </summary>
        /// <param name="hit"></param>
        private void OnControllerColliderHit( ControllerColliderHit hit )
        {
            PushPhysicsObjects(hit);
        }
    }
}
