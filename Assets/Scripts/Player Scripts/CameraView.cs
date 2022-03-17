using Input_Scripts;
using UI_Scripts;
using UnityEngine;

namespace Player_Scripts
{
    /// <summary>
    /// Camera look script
    /// </summary>
    public class CameraView : MonoBehaviour
    {
        // Variables
        private Transform _player;

        [Header("Mouse Settings")]
        [SerializeField] private float mouseSensitivity;
        [SerializeField] [Range(80f, 100f)] private float defaultFOV;
        [SerializeField] [Range(0f, 1f)] private float mouseSmoothTime;
        
        [Header("Mouse Clamp")]
        [SerializeField] private bool isClamped;

        private float _mouseClamp;
        private float _clampAngleUp;
        private float _clampAngleDown;
        private bool _isRunning;
        private bool _isCrouching;
        
        private const float DefaultClampAngleUp = 89.999f;
        private const float DefaultClampAngleDown = -89.999f;

        private Vector2 _currentMouseDelta = Vector2.zero;
        private Vector2 _currentMouseDeltaVelocity = Vector2.zero;
        
        private InputManager _inputManager;
        private Camera _camera;

        /// <summary>
        /// Called Before Start
        /// </summary>
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            
            isClamped = true;
            defaultFOV = 80f; // To be changed later in settings
            _camera.fieldOfView = defaultFOV;
            _player = transform.parent;
            _clampAngleUp = DefaultClampAngleUp;
            _clampAngleDown = DefaultClampAngleDown;
        }
        
        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            _inputManager = InputManager.instance;
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            CheckFOV();
            CheckClamp();
            CheckPauseMenu();
            CameraMovement();
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
            _isRunning = _inputManager.GetKey(KeybindingActions.Run);
            _isCrouching = _inputManager.GetKey(KeybindingActions.Crouch);
        }

        /// <summary>
        /// Camera Movement
        /// </summary>
        private void CameraMovement()
        {
            // Camera Input
            var targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            // Camera movement
            _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _currentMouseDeltaVelocity, mouseSmoothTime);

            // Camera clamp
            _mouseClamp -= _currentMouseDelta.y * mouseSensitivity;
            _mouseClamp = Mathf.Clamp(_mouseClamp, _clampAngleDown, _clampAngleUp);
            
            // Camera math
            _camera.transform.localEulerAngles = Vector3.right * _mouseClamp;
            _player.Rotate(Vector3.up * _currentMouseDelta.x * mouseSensitivity);
        }

        /// <summary>
        /// Change FOV based on movement speed
        /// </summary>
        private void CheckFOV()
        {
            if(_isCrouching)
                ChangeFOV(defaultFOV - 15f);
            else if(_isRunning)
                ChangeFOV(defaultFOV + 15f);
            else 
                ChangeFOV(defaultFOV);
        }
        
        /// <summary>
        /// Change FOV Smoothly
        /// </summary>
        /// <param name="newFOV"> New FOV Value </param>
        private void ChangeFOV(float newFOV)
        {
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, newFOV, 5f * Time.deltaTime);
        }

        /// <summary>
        /// Unlocks mouse if game is paused
        /// </summary>
        private void CheckPauseMenu()
        {
            if (PauseMenu.isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        /// <summary>
        /// Clamp Easter Egg
        /// Makes camera able to loop while looking up / down
        /// </summary>
        private void CheckClamp()
        {
            // If camera is clamped, lock mouse movement to a certain degree
            // if camera isn't clamped allow full mouse movement
            if (isClamped)
            {
                _clampAngleUp = DefaultClampAngleUp;
                _clampAngleDown = DefaultClampAngleDown;
            }
            else
            {
                _clampAngleUp = Mathf.Infinity;
                _clampAngleDown = Mathf.NegativeInfinity;
            }
        }
    }
}
