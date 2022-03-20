using System.Collections;
using Input_Scripts;
using UI_Scripts;
using UnityEngine;

namespace Player_Scripts
{
    /// <summary>
    /// Movement script - Under heavy maintenance
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

        [Header("Ladder Settings")] 
        [SerializeField] private Transform ladderCheck;
        [SerializeField] private LayerMask ladderMask;
        [SerializeField] private bool isClimbing;
        [SerializeField] private float climbSpeedUp;
        [SerializeField] private float climbSpeedDown;
        [SerializeField] private float ladderDistance;

        [Header("Ground Check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private bool isGrounded;

        private const float CrouchingSpeed = 10f;
        private const float StandingUpSpeed = 10f;
        private float Gravity = -9.81f;
        private const float SlopeForce = 3;
        private const float SlopeForceRayLenght = 1.5f;
        private const float GroundRayDistance = 1;
        private const float StandingHeight = 2f;
        private const float GroundDistance = 0.4f;
        
        private float _movementSpeed;
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
        private InputManager _inputManager;

        public States playerState;
        
        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            _inputManager = InputManager.instance;
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
            CheckLadderEvent();
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
            
            // TODO - Air movement
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
            isGrounded = Physics.CheckSphere(groundCheck.position, GroundDistance, groundMask);

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
                UpdateMovementSpeed(runSpeed);
                
                if(_inputDir.x != 0 || _inputDir.y != 0)
                    playerState = States.Running;
            }
            else if (_isWalking)
            {
                UpdateMovementSpeed(walkSpeed);
                playerState = States.Walking;
            }
            else
            {
                UpdateMovementSpeed(defaultSpeed);
            }

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

            // TODO FIX THIS SHIT - Doesn't do shit
            // Steep Slope Action
            if(OnSteepSlope()) SteepSlopeMovement();
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
            _characterController.slopeLimit = 90;

            float timeInAir = 0;

            do
            {
                var jumpForce = jumpFallOff.Evaluate(timeInAir);
                _characterController.Move(Vector3.up * (jumpForce * jumpMultiplier * Time.deltaTime));
                timeInAir += Time.deltaTime;
                yield return null;

            } while (!_characterController.isGrounded && _characterController.collisionFlags != CollisionFlags.Above);

            _characterController.slopeLimit = 45;

            _isJumping = false;
        }

        /// <summary>
        /// Crouch Event
        /// </summary>
        private void CrouchEvent()
        {
            if (transform.localScale.y <= crouchHeight) return;
            
            _characterController.height = Mathf.Lerp(_characterController.height, crouchHeight, CrouchingSpeed * Time.unscaledDeltaTime);

            if (_characterController.height - 0.05f < crouchHeight) _characterController.height = crouchHeight;
        }
        
        // TODO FIX THIS SHIT - Lag when standing up
        /// <summary>
        /// Stand Up Event
        /// </summary>
        private void StandUpEvent()
        {
            if (_characterController.height >= StandingHeight) return;
            
            _characterController.height = Mathf.Lerp(_characterController.height, StandingHeight, StandingUpSpeed * 4 * Time.unscaledDeltaTime);

            if (_characterController.height + 0.05f > StandingHeight) _characterController.height = StandingHeight;
        }

        private void CheckLadderEvent()
        {
            // Ladder Action
            isClimbing = Physics.CheckSphere(ladderCheck.position, ladderDistance, ladderMask);

            if (isClimbing)
            {
                Gravity = 2f;

                var transformNew = transform;
                var velocity = transformNew.up + Vector3.up * velocityY;
                //_characterController.Move(velocity * Time.deltaTime);
                
                if (Input.GetKey("w"))
                    _characterController.Move(Vector3.up * climbSpeedUp);
                //_characterController.transform.position += Vector3.up * climbSpeedUp;
                else if (Input.GetKey("s"))
                    _characterController.transform.position += Vector3.down * climbSpeedDown;
            }

            Gravity = -9.81f;
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

        // TODO - Doesn't work
        // Verify if character is on steep slope
        private bool OnSteepSlope()
        {
            if (_characterController.isGrounded) return false;

            if (!Physics.Raycast(transform.position, Vector3.down, out _slopeHit,
                    (_characterController.height / 2) + GroundRayDistance)) return false;
            
            float slopeAngle = Vector3.Angle(_slopeHit.normal, Vector3.up);

            return slopeAngle > 45;
        }

        // TODO - Doesn't work
        // Steep slope Event
        private void SteepSlopeMovement()
        {
            var slopeDirection = Vector3.up - _slopeHit.normal * Vector3.Dot(Vector3.up, _slopeHit.normal);
            var slideSpeed = defaultSpeed + 10 + Time.deltaTime;

            _moveDirection = slopeDirection * -slideSpeed;
            _moveDirection.y -= _slopeHit.point.y;
        }
        
        private void CheckPauseMenu()
        {
            _camera.GetComponent<CameraView>().enabled = !PauseMenu.isPaused;
        }
    }
}
