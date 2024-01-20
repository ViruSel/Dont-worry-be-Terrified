using System;
using UI_Scripts;
using UnityEngine;

namespace Player_Scripts
{
    // Camera look script
    public class PlayerCameraView : MonoBehaviour
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

        private Vector2 _currentMouseDelta;
        private Vector2 _currentMouseDeltaVelocity;
        
        private Camera _camera;
        private Transform _player;
        private PlayerMovement _playerMovement;
        
        // Called Before Start
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            
            _currentMouseDelta = Vector2.zero;
            _currentMouseDeltaVelocity = Vector2.zero;
            
            isClamped = true;
            defaultFOV = 80f; // To be changed later in settings
            mouseSmoothTime = PlayerProperties.CameraSmoothTime;
            
            _camera.fieldOfView = defaultFOV;
            _player = transform.parent;
            
            _clampAngleUp = PlayerProperties.CameraClampUp;
            _clampAngleDown = PlayerProperties.CameraClampDown;
        }
        
        // Called before the first frame update
        private void Start()
        {
            _playerMovement = _player.GetComponent<PlayerMovement>();
        }
        
        // Called once per frame
        private void Update()
        {
            //CheckFOV();
            CheckClamp();
            CameraMovement();
        }

        private void FixedUpdate()
        {
            CheckFOV();
        }

        // Camera Movement
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
        
        // Change FOV based on movement speed
        private void CheckFOV()
        {
            var walkingFOV = (int)defaultFOV - PlayerProperties.FovDifference / 2;
            var runningFOV = (int)defaultFOV + PlayerProperties.FovDifference;
            var crouchingFOV = (int)defaultFOV - PlayerProperties.FovDifference;

            switch (_playerMovement.playerState)
            {
                case PlayerStates.Crouching:
                    ChangeFOV(crouchingFOV);
                    CorrectHigherFOV(crouchingFOV);
                    break;
                case PlayerStates.Running:
                    ChangeFOV(runningFOV);
                    CorrectLowerFOV(runningFOV);
                    break;
                case PlayerStates.Walking:
                    ChangeFOV(walkingFOV);
                    break;
                default:
                    ChangeFOV(defaultFOV);
                    
                    if (_camera.fieldOfView > defaultFOV)
                        CorrectHigherFOV(defaultFOV);
                    else if (_camera.fieldOfView < defaultFOV)
                        CorrectLowerFOV(defaultFOV);
                    
                    break;
            }
        }
        
        // Change FOV Smoothly
        private void ChangeFOV(float newFOV)
        {
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, newFOV, PlayerProperties.FovChangingSpeed * Time.deltaTime);
        }
        
        // Correct FOV after Running, to be a non decimal value
        private void CorrectLowerFOV(float newFOV)
        {
            if (_camera.fieldOfView + PlayerProperties.FOVCorrection > newFOV)
                _camera.fieldOfView = newFOV;
        }
        
        // Correct FOV after Crouching, to be a non decimal value
        private void CorrectHigherFOV(float newFov)
        {
            if (_camera.fieldOfView - PlayerProperties.FOVCorrection < newFov)
                _camera.fieldOfView = newFov;
        }
        
        // Clamp Easter Egg
        // Makes camera able to loop while looking up / down
        private void CheckClamp()
        {
            // If camera is clamped, lock mouse movement to a certain degree else allow full mouse movement
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
