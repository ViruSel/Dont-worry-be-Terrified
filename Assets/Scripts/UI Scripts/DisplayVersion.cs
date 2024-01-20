using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    //Todo: Destroy canvas when showing fps

    public class DisplayVersion : MonoBehaviour
    {
        // Variables
        private Text _text;
        public Animator animator;
        
        // Called before the first frame update
        private void Start()
        {
            Initialize();
        }
        
        // Update is called once per frame
        private void Update()
        {
            ShowVersion();
        }
        
        // Initialize variables
        private void Initialize()
        {
            _text = GetComponent<Text>();
            _text.text = string.Format(UIProperties.VersionString, Application.version);
            _text.enabled = false;
        }
        
        // Enables version text
        private void ShowVersion()
        {
            // If the loading screen animation is not playing, show the version text
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Crossfade_Start"))
            {
                StartCoroutine(DelayTextActivation(UIProperties.DelayInSeconds));
            }
            else
            {
                DestroyText();
            }
        }
        
        // Delay the activation of the text
        private IEnumerator DelayTextActivation(float sec)
        {
            yield return new WaitForSeconds(sec);
            _text.enabled = true;
        }
        
        /// Destroy text & script
        private void DestroyText()
        {
            if (_text.Equals(null)) return;
            
            Destroy(_text);
            Destroy(this);
        }
    }
}
