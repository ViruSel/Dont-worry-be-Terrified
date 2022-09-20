using System.Collections;
using System.Collections.Generic;
using Input_Scripts;
using Player_Scripts;
using Scene_Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle_Scripts
{
    public class Puzzle2SolvingButton : MonoBehaviour
    {
        //TODO: Separate script for each puzzle
        
        /// <summary>
        /// variables
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

        [Header("Password")]
        [SerializeField] private int[] password;

        public static List<int> PasswordReceived;                   // Password received by the puzzle buttons

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
        
        private GameObject[] _triggers;
        private GameObject[] _tunnelObjects;
        private GameObject[] _puzzleObjects;
        private GameObject[] _mirrors;

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

        /// <summary>
        /// initialize variables
        /// </summary>
        private void Initialize()
        {
            _inputManager = InputManager.Instance;

            _renderer = GetComponent<Renderer>();
            _buttonSound = GetComponent<AudioSource>();
            _buttonAnimation = GetComponent<Animation>();

            _openSound = animatedObject.GetComponent<AudioSource>();
            _objectAnimation = animatedObject.GetComponent<Animation>();
            
            _mirrors = GameObject.FindGameObjectsWithTag("Mirror");
            _triggers = GameObject.FindGameObjectsWithTag("Trigger");
            _puzzleObjects = GameObject.FindGameObjectsWithTag("Puzzle");
            _tunnelObjects = GameObject.FindGameObjectsWithTag("Tunnel");

            _canPress = true;
            _renderer.material = offColor;
            _oldCrosshair = crosshair.text;
            
            PasswordReceived = new List<int>();
            
            ObjectManager.DisableObjects(_tunnelObjects);
        }

        /// <summary>
        /// Solved puzzle actions
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        private IEnumerator PuzzleSolvedActions(int delay)
        {
            BreakMirrors();
            
            ObjectManager.DestroyObjects(_triggers);
            ObjectManager.DestroyObjects(_puzzleObjects);
            ObjectManager.ActivateObjects(_tunnelObjects);

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
            
            PasswordReceived.Clear();
            
            _buttonAnimation.Play();
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
                    if (PuzzleManager.CheckIfAllButtonsPressed(buttons) && PuzzleManager.CheckPassword(password, PasswordReceived))
                    {
                        StartCoroutine(PuzzleSolvedActions(0));
                        _canPress = false;
                    }
                    else
                    {
                        StartCoroutine(PuzzleUnSolvedActions());
                    }
                }
            }
        }

        private void BreakMirrors()
        {
            foreach (var mirror in _mirrors)
            {
                mirror.GetComponent<Animation>().Play();
                mirror.GetComponent<AudioSource>().Play();
                    
                mirror.transform.GetChild(0).gameObject.SetActive(false);   // Disable Teleportation collider
                mirror.transform.GetChild(1).gameObject.SetActive(false);   // Disable Non broken Mirror
            }
        }
    }
}
