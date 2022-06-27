using System;
using UnityEngine;

namespace Mirror_Scripts
{
    /// <summary>
    /// Apply player's camera effects on mirrors
    /// </summary>
    public class MirrorEffects : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        private Camera _playerCamera;
        private Camera _mirrorCamera;

        [SerializeField] private Transform mirror;
        [SerializeField] private int fps;
        
        private float _elapsedTime;

        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            _mirrorCamera = GetComponent<Camera>();
            _playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        private void Start()
        {
            //_mirrorCamera.enabled = false;
        }

        /// <summary>
        /// Called after all Update functions
        /// </summary>
        private void LateUpdate()
        {
            ChangeFOV();
        }
        
        private void Update()
        {
            LimitFPS();
        }

        private bool IsFacingObject()
        {
            var toPlayer = (_playerCamera.transform.parent.position - mirror.position).normalized;
            var forward = mirror.forward;
            
            var dot = Vector3.Dot(toPlayer, forward);
            
            return dot < 0;
        }

        private void LimitFPS()
        {
            if (gameObject.activeSelf && IsFacingObject())
            {
                _elapsedTime += Time.deltaTime;
            
                if (_elapsedTime > 1.0f / fps)
                {
                    _elapsedTime = 0;
                    _mirrorCamera.Render();
                }
            }
            else if (!gameObject.activeSelf && _mirrorCamera.enabled)
            {
                _mirrorCamera.enabled = false;
            }
        }
        
        /// <summary>
        /// Change Mirror's FOV with Player's FOV
        /// </summary>
        private void ChangeFOV()
        {
            _mirrorCamera.fieldOfView = _playerCamera.fieldOfView;
        }
    }
}
