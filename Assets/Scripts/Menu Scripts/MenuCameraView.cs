using System.Collections;
using Menu_Scripts;
using UnityEditor;
using UnityEngine;

namespace Main_Menu_Scripts
{
    /// <summary>
    /// Camera look in menu
    /// </summary>
    public class MenuCameraView : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [Header("Mouse Settings")]
        [SerializeField] private float mouseSensitivity;
        [SerializeField] [Range(0f, 1f)] private float mouseSmoothTime;
    
        [Header("Mouse Clamp")]
        [SerializeField] private bool isClamped;
    
        private float _mouseClampY;
        private float _mouseClampX;
        private float _clampAngleLeft;
        private float _clampAngleRight;

        private Vector2 _currentMouseDelta = Vector2.zero;
        private Vector2 _currentMouseDeltaVelocity = Vector2.zero;
        
        private Camera _menuCamera;
        private Transform _tripod;

        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            CheckClamp();
            CameraMovement();
        }

        private void Initialize()
        {
            _menuCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            _menuCamera.fieldOfView = MenuProperties.FieldOfView;
            
            isClamped = true;
            
            _tripod = transform.parent;

            mouseSensitivity = MenuProperties.MouseSensitivity;
            mouseSmoothTime = MenuProperties.MouseSmoothTime;
            
            _clampAngleLeft = MenuProperties.DefaultClampAngleLeft;
            _clampAngleRight = MenuProperties.DefaultClampAngleRight;

            StartCoroutine(CenterMouse());
        }
        
        /// <summary>
        /// Center mouse position
        /// </summary>
        /// <returns></returns>
        private IEnumerator CenterMouse()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            yield return new WaitForSeconds(0.1f);
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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

            // Camera clamp on Vertical axis
            _mouseClampY -= _currentMouseDelta.y * mouseSensitivity;
            _mouseClampY = Mathf.Clamp(_mouseClampY, MenuProperties.ClampAngleDown, MenuProperties.ClampAngleUp);

            // Camera clamp on Horizontal Axis
            _mouseClampX += _currentMouseDelta.x * mouseSensitivity;
            _mouseClampX = Mathf.Clamp(_mouseClampX, _clampAngleLeft, _clampAngleRight);

            // Camera math
            _menuCamera.transform.localEulerAngles = Vector3.right * _mouseClampY;
            _tripod.rotation = Quaternion.Euler(-10.493f, _mouseClampX, 0f);
        }
    
        /// <summary>
        /// Clamp Easter Egg
        /// Makes camera able to loop while looking left / right
        /// </summary>
        private void CheckClamp()
        {
            // If camera is clamped, lock mouse movement to a certain degree
            // if camera isn't clamped allow full mouse movement
            if (isClamped)
            {
                _clampAngleLeft = MenuProperties.DefaultClampAngleLeft;
                _clampAngleRight = MenuProperties.DefaultClampAngleRight;
            }
            else
            {
                _clampAngleLeft = Mathf.NegativeInfinity;
                _clampAngleRight = Mathf.Infinity;
            }
        }
    }
}
