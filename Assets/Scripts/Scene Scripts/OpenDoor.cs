using System;
using Input_Scripts;
using Player_Scripts;
using UnityEngine;

namespace Scene_Scripts
{
    public class OpenDoor : MonoBehaviour
    {
        /// <summary>
        /// variables
        /// </summary>
        [SerializeField] private float distance;

        [SerializeField] private GameObject actionDisplay;
        [SerializeField] private Animation anim;

        [SerializeField] private AudioSource creekSound;

        private InputManager _inputManager;
    
        private bool _isPressing;
    
        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            _inputManager = InputManager.Instance;
            creekSound = GameObject.Find("Door_A").GetComponent<AudioSource>();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            distance = CastToObject.Distance;
        }

        /// <summary>
        /// Called every fixed frame
        /// </summary>
        private void FixedUpdate()
        {
            _isPressing = _inputManager.GetKey(KeybindingActions.Use);
        }

        /// <summary>
        /// Actions to be performed while mouse is over this object
        /// </summary>
        private void OnMouseOver()
        {
            if (distance < 4)
                actionDisplay.SetActive(true);

            if (_isPressing)
            {
                if (distance < 4)
                {
                    actionDisplay.SetActive(false);
                    anim.Play();
                    creekSound.Play();
                }
            }
        }

        /// <summary>
        /// Actions to be performed while mouse is no longer over this object
        /// </summary>
        private void OnMouseExit()
        {
            actionDisplay.SetActive(false);
        }
    }
}
