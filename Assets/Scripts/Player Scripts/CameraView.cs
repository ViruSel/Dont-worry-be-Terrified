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
        [Header("Mouse Settings")]
        [SerializeField] private float mouseSensitivity;
        [SerializeField] [Range(80f, 100f)] private float defaultFOV;
        [SerializeField] [Range(0f, 1f)] private float mouseSmoothTime;
        
        [Header("Mouse Clamp")]
        [SerializeField] private bool isClamped;

        private float _mouseClamp;
        private float _clampAngleUp;
        private float _clampAngleDown;

        private Vector2 _currentMouseDelta = Vector2.zero;
        private Vector2 _currentMouseDeltaVelocity = Vector2.zero;
        
        private Camera _camera;
        private Transform _player;
        private Movement _playerMovement;

        /// <summary>
        /// Called Before Start
        /// </summary>
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            
            isClamped = true;
            defaultFOV = 80f; // To be changed later in settings
            mouseSmoothTime = PlayerProperties.CameraSmoothTime;
            
            _camera.fieldOfView = defaultFOV;
            _player = transform.parent;
            
            _clampAngleUp = PlayerProperties.CameraClampUp;
            _clampAngleDown = PlayerProperties.CameraClampDown;
        }
        
        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            _playerMovement = _player.GetComponent<Movement>();
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
        /// Camera Movement
        /// </summary>
        private void CameraMovement()
        {
            // Input
            var targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            // Movement
            _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _currentMouseDeltaVelocity, mouseSmoothTime);

            // Clamp
            _mouseClamp -= _currentMouseDelta.y * mouseSensitivity;
            _mouseClamp = Mathf.Clamp(_mouseClamp, _clampAngleDown, _clampAngleUp);
            
            // Rotate
            _camera.transform.localEulerAngles = Vector3.right * _mouseClamp;
            _player.Rotate(Vector3.up * (_currentMouseDelta.x * mouseSensitivity));
        }

        /// <summary>
        /// Change FOV based on movement speed
        /// </summary>
        private void CheckFOV()
        {
            var crouchFOV = defaultFOV - PlayerProperties.FovDifference;
            var runningFOV = defaultFOV + PlayerProperties.FovDifference;

            if (_playerMovement.playerState == States.Crouching)
            {
                ChangeFOV(crouchFOV);
                CorrectAfterCrouchingFOV(crouchFOV);
            }
            else if (_playerMovement.playerState == States.Running)
            {
                ChangeFOV(runningFOV);
                CorrectAfterRunningFOV(runningFOV);
            }
            else
            {
                ChangeFOV(defaultFOV);
                
                // Correct FOV values to a non decimal value after crouching and running
                if (_camera.fieldOfView > defaultFOV)
                    CorrectAfterCrouchingFOV(defaultFOV);
                else if (_camera.fieldOfView < defaultFOV)
                    CorrectAfterRunningFOV(defaultFOV);
            }
        }
        
        /// <summary>
        /// Change FOV Smoothly
        /// </summary>
        /// <param name="newFOV"> New FOV Value </param>
        private void ChangeFOV(float newFOV)
        {
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, newFOV, PlayerProperties.FovChangingSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Correct FOV after Running, to be a non decimal value
        /// </summary>
        /// <param name="newFOV"></param>
        private void CorrectAfterRunningFOV(float newFOV)
        {
            if (_camera.fieldOfView + PlayerProperties.FOVCorrection > newFOV)
                _camera.fieldOfView = newFOV;
        }

        /// <summary>
        /// Correct FOV after Crouching, to be a non decimal value
        /// </summary>
        /// <param name="newFov"></param>
        private void CorrectAfterCrouchingFOV(float newFov)
        {
            if (_camera.fieldOfView - PlayerProperties.FOVCorrection < newFov)
                _camera.fieldOfView = newFov;
        }

        /// <summary>
        /// Unlocks mouse if game is paused
        /// </summary>
        private void CheckPauseMenu()
        {
            if (PauseMenu.IsPaused)
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
                _clampAngleUp = PlayerProperties.CameraClampUp;
                _clampAngleDown = PlayerProperties.CameraClampDown;
            }
            else
            {
                _clampAngleUp = Mathf.Infinity;
                _clampAngleDown = Mathf.NegativeInfinity;
            }
        }
    }
}
