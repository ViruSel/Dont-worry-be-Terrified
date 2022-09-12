using Input_Scripts;
using Player_Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle_Scripts
{
    public class PuzzleButtonCheck : MonoBehaviour
    {
        /// <summary>
        /// variables
        /// </summary>
        [Header("Crosshair")]
        [SerializeField] private Text crosshair;

        [Header("Button Colors")]
        [SerializeField] private Material offColor;
        [SerializeField] private Material onColor;

        public bool isPressed;
        
        private float _distance; // Distance to this object
        private string _oldText;
        private bool _isPressing;
        private bool _canPress;
        
        private InputManager _inputManager;
        private Renderer _renderer;
        private AudioSource _buttonSound;
        private Animation _animation;

        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            _distance = PlayerCastToObject.Distance;
        }

        /// <summary>
        /// Called every fixed frame
        /// </summary>
        private void FixedUpdate()
        {
            _isPressing = _inputManager.GetKey(KeybindingActions.Use);
        }

        /// <summary>
        /// initialize variables
        /// </summary>
        private void Initialize()
        {
            _inputManager = InputManager.Instance;
            
            _renderer = GetComponent<Renderer>();
            _animation = GetComponent<Animation>();
            _buttonSound = GetComponent<AudioSource>();

            _renderer.material = offColor;
            _oldText = crosshair.text;
            
            isPressed = false;
            _canPress = true;
        }

        public void ResetButton()
        {
            _renderer.material = offColor;
            crosshair.text = _oldText;
            
            isPressed = false;
            _canPress = true;
        }

        /// <summary>
        /// Actions to be performed while mouse is over this object
        /// </summary>
        private void OnMouseOver()
        {
            if (_distance < 4 && _canPress)
            {
                crosshair.text = "E";
                
                if (_isPressing)
                {
                    _canPress = false;
                    isPressed = true;
                    
                    _renderer.material = onColor;
                    crosshair.text = "";
                    
                    _animation.Play();
                    _buttonSound.Play();
                    
                    PuzzleSolvedButton.PasswordReceived.Add(int.Parse(name));
                }
            }
        }

        /// <summary>
        /// Actions to be performed while mouse is no longer over this object
        /// </summary>
        private void OnMouseExit()
        {
            crosshair.text = _oldText;
        }
    }
}
