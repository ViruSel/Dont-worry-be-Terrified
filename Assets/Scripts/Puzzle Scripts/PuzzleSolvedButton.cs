using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Input_Scripts;
using Player_Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle_Scripts
{
    public class PuzzleSolvedButton : MonoBehaviour
    {
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
        [Tooltip("If it's the case")]
        [SerializeField] private int[] password;
        
        public static List<int> PasswordReceived = new List<int>(); // Password received by pressing the buttons
        public static string AnimatedObjReference;
        public static bool IsSolved;

        private float _distance; // Distance to this object
        private string _oldText;
        private bool _isPressing;
        private bool _canPress;

        private InputManager _inputManager;
        private Renderer _renderer;
        private AudioSource _openSound;
        private AudioSource _buttonSound;
        private Animation _objectAnim;
        private Animation _buttonAnim;

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
            _distance = CastToObject.Distance;
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
            _buttonAnim = GetComponent<Animation>();
            _buttonSound = GetComponent<AudioSource>();
            _openSound = animatedObject.GetComponent<AudioSource>();
            _objectAnim = animatedObject.GetComponent<Animation>();

            _renderer.material = offColor;
            _oldText = crosshair.text;
            
            _canPress = true;
            IsSolved = false;

            AnimatedObjReference = animatedObject.tag;
        }

        /// <summary>
        /// Check if all the buttons are checked
        /// </summary>
        /// <returns></returns>
        private bool CheckAllButtons()
        {
            return buttons.All(button => button.GetComponent<PuzzleButtonCheck>().isChecked);
        }

        /// <summary>
        /// Check if buttons are pressed in correct order
        /// </summary>
        /// <returns></returns>
        private bool CheckPassword()
        {
            return !password.Where((t, i) => PasswordReceived[i] != t).Any();
        }

        /// <summary>
        /// Solved puzzle actions
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        private IEnumerator PuzzleSolvedActions(int delay)
        {
            IsSolved = true;
            
            _renderer.material = onColor;
            crosshair.text = _oldText;
            
            _buttonAnim.Play();
            _buttonSound.Play();
            
            yield return new WaitForSeconds(delay);
            
            _objectAnim.Play();
            _openSound.Play();

            IsSolved = false;
        }
        
        /// <summary>
        /// Unsolved puzzle actions
        /// </summary>
        /// <returns></returns>
        private IEnumerator PuzzleUnSolvedActions()
        {
            foreach (var button in buttons)
                button.GetComponent<PuzzleButtonCheck>().ResetButton();
            
            PasswordReceived.Clear();
            
            _buttonAnim.Play();
            _buttonSound.Play();

            yield return null;
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
                    switch (animatedObject.name)
                    {
                        case "Mirror": // Check Mirror object to fix the animation
                        {
                            if (CheckAllButtons() && CheckPassword())
                            {
                                _canPress = false;
                                StartCoroutine(PuzzleSolvedActions(0));
                            }
                            else StartCoroutine(PuzzleUnSolvedActions());
                            break;
                        }
                        case "Door":
                        {
                            if (CheckAllButtons())
                            {
                                _canPress = false;
                                StartCoroutine(PuzzleSolvedActions(1));
                            }
                            else StartCoroutine(PuzzleUnSolvedActions());
                            break;
                        }
                    }
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
