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
        // variables
        [Header("Crosshair")]
        [SerializeField] private Text crosshair;

        [Header("Buttons")] 
        [SerializeField] private GameObject[] buttons;

        [Header("Button Colors")]
        [SerializeField] private Material offColor;
        [SerializeField] private Material onColor;

        [Header("Password")]
        [SerializeField] private int[] password;

        public static List<int> passwordReceived;          // Password received by the puzzle buttons

        private float _distanceToButton;                   // Distance to this object
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
        
        /// initialize variables
        private void Initialize()
        {
            _inputManager = InputManager.Instance;

            _renderer = GetComponent<Renderer>();
            _buttonSound = GetComponent<AudioSource>();
            _buttonAnimation = GetComponent<Animation>();

            _mirrors = GameObject.FindGameObjectsWithTag("Mirror");
            _triggers = GameObject.FindGameObjectsWithTag("Trigger");
            _puzzleObjects = GameObject.FindGameObjectsWithTag("Puzzle");
            _tunnelObjects = GameObject.FindGameObjectsWithTag("Tunnel");

            _canPress = true;
            _renderer.material = offColor;
            _oldCrosshair = crosshair.text;
            
            passwordReceived = new List<int>();
            
            ObjectManager.DisableObjects(_tunnelObjects);
        }
        
        // Solved puzzle actions
        private IEnumerator SolvePuzzle(int delay)
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
        }
        
        // Unsolved puzzle actions
        private IEnumerator UnSolvePuzzle()
        {
            PuzzleManager.ResetButtons(buttons);
            
            passwordReceived.Clear();
            
            _buttonAnimation.Play();
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
                    if (PuzzleManager.CheckIfAllButtonsPressed(buttons) && PuzzleManager.CheckPassword(password, passwordReceived))
                    {
                        StartCoroutine(SolvePuzzle(0));
                        _canPress = false;
                    }
                    else
                    {
                        StartCoroutine(UnSolvePuzzle());
                    }
                }
            }
        }

        // Make mirrors break
        private void BreakMirrors()
        {
            foreach (var mirror in _mirrors)
            {
                //TODO: Store animations in a list
                mirror.GetComponent<Animation>().Play();
                
                //TODO: use same sound for all mirrors
                mirror.GetComponent<AudioSource>().Play();
                    
                mirror.transform.GetChild(0).gameObject.SetActive(false);   // Disable Teleportation collider
                mirror.transform.GetChild(1).gameObject.SetActive(false);   // Disable Non broken Mirror
            }
        }
    }
}
