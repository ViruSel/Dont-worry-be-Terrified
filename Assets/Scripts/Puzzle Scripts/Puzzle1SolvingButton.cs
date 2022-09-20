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
        /// <summary>
        /// Variables
        /// </summary>
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

        private float _distanceToButton;                            // Distance to this object
        private string _oldCrosshair;
        private bool _isPressing;
        private bool _canPress;
        
        private InputManager _inputManager;
        private Renderer _renderer;
        private AudioSource _openSound;
        private AudioSource _buttonSound;
        private Animation _objectAnimation;
        private Animation _buttonAnimation;
        
        private GameObject[] _teleporters;
        
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
            _distanceToButton = PlayerCastToObject.Distance;
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
            SolvePuzzle();
        }

        /// <summary>
        /// Actions to be performed while mouse is no longer over this object
        /// </summary>
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
            
            _teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
            
            _canPress = true;
            _renderer.material = offColor;
            _oldCrosshair = crosshair.text;
            
            ObjectManager.DisableObjects(_teleporters);
        }
        
        /// <summary>
        /// Solved puzzle actions
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        private IEnumerator PuzzleSolvedActions(int delay)
        {
            ObjectManager.ActivateObjects(_teleporters);

            _renderer.material = onColor;
            crosshair.text = _oldCrosshair;
            
            _buttonAnimation.Play();
            _buttonSound.Play();
            
            yield return new WaitForSeconds(delay);
            
            _objectAnimation.Play();
            _openSound.Play();
        }
        
        /// <summary>
        /// Unsolved puzzle actions
        /// </summary>
        /// <returns></returns>
        private IEnumerator PuzzleUnSolvedActions()
        {
            PuzzleManager.ResetButtons(buttons);
            
            _buttonSound.Play();

            yield return null;
        }
        
        private void SolvePuzzle()
        {
            if (_distanceToButton < PuzzleManager.DistanceToButton && _canPress)
            {
                crosshair.text = "E";

                if (_isPressing)
                {
                    if (PuzzleManager.CheckIfAllButtonsPressed(buttons))
                    {
                        StartCoroutine(PuzzleSolvedActions(1));
                        _canPress = false;
                    }
                    else
                    {
                        StartCoroutine(PuzzleUnSolvedActions());
                    }
                }
            }
        }
    }
}
