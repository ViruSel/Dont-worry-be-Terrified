using UnityEngine;

namespace Main_Menu_Scripts
{
    /// <summary>
    /// Change Main menu button colors
    /// </summary>
    public class MenuButtonColors : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [Header("Button Color")]
        [SerializeField] private Material buttonOnColor;
        [SerializeField] private Material buttonOffColor;

        private readonly Color _textOnColor = new Color(110,0,0); // BLOODY RED
        private readonly Color _textOffColor = new Color(200,200,200); // WHITEY GREY

        private GameObject[] _buttons;

        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            _buttons = GameObject.FindGameObjectsWithTag(gameObject.tag);
        }

        /// <summary>
        /// Changing color on mouse hover
        /// </summary>
        private void OnMouseEnter()
        {
            foreach (var button in _buttons)
            {
                button.GetComponent<Renderer>().material = buttonOnColor;
            }
        }

        /// <summary>
        /// Changing color back to default
        /// </summary>
        private void OnMouseExit()
        {
            foreach (var button in _buttons)
            {
                button.GetComponent<Renderer>().material = buttonOffColor;
            }
        }

        /// <summary>
        /// Changing color when clicked
        /// </summary>
        private void OnMouseDown()
        {
            foreach (var button in _buttons)
            {
                button.GetComponentInChildren<SpriteRenderer>().color = _textOnColor;
            }
        }

        /// <summary>
        /// Changing color when click is lifted
        /// </summary>
        private void OnMouseUp()
        {
            foreach (var button in _buttons)
            {
                button.GetComponentInChildren<SpriteRenderer>().color = _textOffColor;
            }
        }
    }
}
