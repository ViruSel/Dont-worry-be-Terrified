using UnityEngine;

namespace Menu_Scripts
{
    // Change Main menu button colors
    public class MenuButtonColors : MonoBehaviour
    {
        // Variables
        [Header("Button Color")]
        [SerializeField] private Material buttonOnColor;
        [SerializeField] private Material buttonOffColor;

        private readonly Color _textOnColor = new Color(110,0,0);       // BLOODY RED
        private readonly Color _textOffColor = new Color(200,200,200);  // WHITEY GREY

        private GameObject[] _buttons;
        
        // Called before Start function
        private void Awake()
        {
            _buttons = GameObject.FindGameObjectsWithTag(gameObject.tag);
        }
        
        // Changing color on mouse hover
        private void OnMouseEnter()
        {
            foreach (var button in _buttons)
            {
                button.GetComponent<Renderer>().material = buttonOnColor;
            }
        }
        
        // Changing color back to default
        private void OnMouseExit()
        {
            foreach (var button in _buttons)
            {
                button.GetComponent<Renderer>().material = buttonOffColor;
            }
        }
        
        // Changing color when clicked
        private void OnMouseDown()
        {
            foreach (var button in _buttons)
            {
                button.GetComponentInChildren<SpriteRenderer>().color = _textOnColor;
            }
        }
        
        // Changing color when click is lifted
        private void OnMouseUp()
        {
            foreach (var button in _buttons)
            {
                button.GetComponentInChildren<SpriteRenderer>().color = _textOffColor;
            }
        }
    }
}
