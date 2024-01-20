using System.Collections;
using Input_Scripts;
using Mirror_Scripts;
using Player_Scripts;
using Scene_Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle_Scripts
{
    //TODO: 2 Puzzle1Buttons send info here and this script will check if the puzzle is solved
    
    public class Puzzle1SolvingButton : MonoBehaviour
    {
        // Variables
        [Header("Crosshair")]
        [SerializeField] private Text crosshair;
        
        [Header("Animated Object")]
        [Tooltip("Object with animation")]
        [SerializeField] private GameObject animatedObject;

        [Header("Buttons")] 
        [SerializeField] private GameObject[] buttons;

        [Header("Button Colors")]
        [SerializeField] private Material offColor;
        [SerializeField] private Material onColor;

        private float _distanceToButton;       // Distance to this object
        private string _oldCrosshair;
        private bool _isPressing;
        private bool _canPress;
        
        private InputManager _inputManager;
        private Renderer _renderer;
        private AudioSource _openSound;
        private AudioSource _buttonSound;
        private Animation _objectAnimation;
        private Animation _buttonAnimation;
        
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
            IsPuzzleSolved();
        }
        
        // Actions to be performed while mouse is no longer over this object
        private void OnMouseExit()
        {
            crosshair.text = _oldCrosshair;
        }
        
        private void Initialize()
        {
            _inputManager = InputManager.Instance;
            
            _renderer = GetComponent<Renderer>();
            _buttonAnimation = GetComponent<Animation>();
            _buttonSound = GetComponent<AudioSource>();
            
            _openSound = animatedObject.GetComponent<AudioSource>();
            _objectAnimation = animatedObject.GetComponent<Animation>();

            _canPress = true;
            _renderer.material = offColor;
            _oldCrosshair = crosshair.text;
        }
        
        // Solved puzzle actions
        private IEnumerator SolvePuzzle(int delay)
        {
            _renderer.material = onColor;
            crosshair.text = _oldCrosshair;
            
            _buttonAnimation.Play();
            _buttonSound.Play();
            
            yield return new WaitForSeconds(delay);
            
            _objectAnimation.Play();
            _openSound.Play();
        }
        
        // Unsolved puzzle actions
        private IEnumerator UnSolvePuzzle()
        {
            PuzzleManager.ResetButtons(buttons);
            
            _buttonSound.Play();

            yield return null;
        }
        
        // Check if puzzle is solved
        private void IsPuzzleSolved()
        {
            if (_distanceToButton < PuzzleManager.DistanceToButton && _canPress)
            {
                crosshair.text = "E";

                if (_isPressing)
                {
                    if (PuzzleManager.CheckIfAllButtonsPressed(buttons))
                    {
                        StartCoroutine(SolvePuzzle(1));
                        _canPress = false;
                    }
                    else
                    {
                        StartCoroutine(UnSolvePuzzle());
                    }
                }
            }
        }
    }
}
