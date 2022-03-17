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
        private Renderer _buttonRenderer;
    
        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            _buttonRenderer = GetComponent<Renderer>();
        }

        /// <summary>
        /// Changing color on mouse hover
        /// </summary>
        private void OnMouseEnter()
        {
            _buttonRenderer.material.color = Color.red;
        }

        /// <summary>
        /// Changing color back to default
        /// </summary>
        private void OnMouseExit()
        {
            _buttonRenderer.material.color = Color.white;
        }
    }
}
