using System.Collections;
using Menu_Scripts;
using UnityEditor;
using UnityEngine;

namespace Main_Menu_Scripts
{
    // Camera look in menu
    public class MenuCameraView : MonoBehaviour
    {
        // Variables
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
        
        // Called before Start function
        private void Awake()
        {
            Initialize();
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
        
        // Update is called once per frame
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
        
        /// Center mouse position
        private IEnumerator CenterMouse()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            yield return new WaitForSeconds(0.1f);
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        /// Camera Movement
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
        
        // Clamp Easter Egg
        // Makes camera able to loop while looking left / right
        private void CheckClamp()
        {
            // If camera is clamped, lock mouse movement to a certain degree else allow full mouse movement
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
