using System.Collections;
using System.Linq;
using Input_Scripts;
using Player_Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Scene_Scripts
{
    public class OpenDoorButton : MonoBehaviour
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

        private float _distance; // Distance to this object
        private string _oldText;
        private bool _isPressing;
        private bool _canOpen;

        private InputManager _inputManager;
        private Renderer _renderer;
        private AudioSource _creekSound;
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
            _creekSound = animatedObject.GetComponent<AudioSource>();
            _objectAnim = animatedObject.GetComponent<Animation>();

            _renderer.material = offColor;
            _oldText = crosshair.text;
            _canOpen = true;
        }

        /// <summary>
        /// Check if all other buttons are checked
        /// </summary>
        /// <returns></returns>
        private bool CheckAllButtons()
        {
            return buttons.All(button => button.GetComponent<OpenDoorButtonCheck>().isChecked);
        }

        private IEnumerator OpenDoor()
        {
            _renderer.material = onColor;
            crosshair.text = " ";
            
            _buttonAnim.Play();
            _buttonSound.Play();
            
            yield return new WaitForSeconds(2);
            
            _objectAnim.Play();
            _creekSound.Play();
        }

        /// <summary>
        /// Actions to be performed while mouse is over this object
        /// </summary>
        private void OnMouseOver()
        {
            if (_distance < 4 && _canOpen)
            {
                crosshair.text = "E";

                if (_isPressing && CheckAllButtons())
                {
                    _canOpen = false;
                    StartCoroutine(OpenDoor());
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
