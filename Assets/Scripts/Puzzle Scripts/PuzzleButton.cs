using Input_Scripts;
using Player_Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle_Scripts
{
    public class PuzzleButton : MonoBehaviour
    {
        // variables
        [Header("Crosshair")]
        [SerializeField] private Text crosshair;

        [Header("Button Colors")]
        [SerializeField] private Material offColor;
        [SerializeField] private Material onColor;
        
        [Header("Puzzle Number")]
        [SerializeField] private int puzzleNumber;

        public bool isPressed;
        
        private float _distanceToButton; // Distance to this object
        private string _oldCrosshair;
        private bool _isPressing;
        private bool _canPress;
        
        private InputManager _inputManager;
        private Renderer _renderer;
        private AudioSource _buttonSound;
        private Animation _animation;
        
        // Called before the first frame update
        private void Start()
        {
            Initialize();
        }
        
        // Called once per frame
        private void Update()
        {
            _distanceToButton = PlayerCastToObject.Distance;
        }
        
        // Called every fixed frame
        private void FixedUpdate()
        {
            _isPressing = _inputManager.GetKey(KeybindingActions.Use);
        }
        
        // Actions to be performed while mouse is over this object
        private void OnMouseOver()
        {
            IsPressing();
        }
        
        // Actions to be performed while mouse is no longer over this object
        private void OnMouseExit()
        {
            crosshair.text = _oldCrosshair;
        }
        
        // initialize variables
        private void Initialize()
        {
            _inputManager = InputManager.Instance;
            
            _renderer = GetComponent<Renderer>();
            _animation = GetComponent<Animation>();
            _buttonSound = GetComponent<AudioSource>();

            _renderer.material = offColor;
            _oldCrosshair = crosshair.text;
            
            isPressed = false;
            _canPress = true;
        }
        
        // Reset button state to unpressed
        public void ResetButton()
        {
            _renderer.material = offColor;
            crosshair.text = _oldCrosshair;
            
            isPressed = false;
            _canPress = true;
        }

        // Check if player is pressing the button
        private void IsPressing()
        {
            if (_distanceToButton < 4 && _canPress)
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
                    
                    PuzzleManager.SendPassword(puzzleNumber, int.Parse(name));
                }
            }
        }
    }
}
