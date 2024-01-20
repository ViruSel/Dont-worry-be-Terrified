using System;
using UnityEngine;

namespace Mirror_Scripts
{
    // Apply player's camera effects on mirrors
    public class MirrorEffects : MonoBehaviour
    {
        // Variables
        private Camera _playerCamera;
        private Camera _mirrorCamera;

        /*[SerializeField] private Transform mirror;
        [SerializeField] private int fps;
        private float _elapsedTime;*/
        
        // Called before Start function
        private void Awake()
        {
            _mirrorCamera = GetComponent<Camera>();
            _playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }
        
        // Called after all Update functions
        private void LateUpdate()
        {
            ChangeFOV();
        }
        
        private void Update()
        {
            //LimitFPS();
        }

        // Change Mirror's FOV with Player's FOV
        private void ChangeFOV()
        {
            _mirrorCamera.fieldOfView = _playerCamera.fieldOfView;
        }
        
        // Try to detect when mirror is in front of player
        /*private bool IsFacingObject()
        {
            var toPlayer = (_playerCamera.transform.parent.position - mirror.position).normalized;
            var forward = mirror.forward;
            
            var dot = Vector3.Dot(toPlayer, forward);
            
            return dot < 0;
        }*/

        // Limit fps inside mirror
        /*private void LimitFPS()
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
        }*/
    }
}
