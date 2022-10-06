using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    
    //Todo: Destroy canvas when showing fps

    public class DisplayVersion : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        private Text _text;

        private const float Seconds = 1f;
        private const string Display = "Version {0}";

        public Animator animator;
        
        
        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            ShowVersion();
        }

        /// <summary>
        /// Initialize variables
        /// </summary>
        private void Initialize()
        {
            _text = GetComponent<Text>();
            _text.text = string.Format(Display, Application.version);
            _text.enabled = false;
        }
        
        /// <summary>
        /// Enables version text
        /// </summary>
        private void ShowVersion()
        {
            // If the loading screen animation is not playing, show the version text
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Crossfade_Start"))
            {
                StartCoroutine(DelayTextActivation(Seconds));
            }
            else
            {
                DestroyText();
            }
        }

        /// <summary>
        /// Delay the activation of the text
        /// </summary>
        /// <param name="sec"></param>
        /// <returns></returns>
        private IEnumerator DelayTextActivation(float sec)
        {
            yield return new WaitForSeconds(sec);
            _text.enabled = true;
        }

        /// <summary>
        /// Destroy text & script
        /// </summary>
        private void DestroyText()
        {
            if (_text.Equals(null)) return;
            
            Destroy(_text);
            Destroy(this);
        }
    }
}
