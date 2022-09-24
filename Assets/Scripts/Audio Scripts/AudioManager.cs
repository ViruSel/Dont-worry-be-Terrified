using System;
using System.Collections;
using UI_Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio_Scripts
{
    /// <summary>
    /// Audio managing script
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        public Sound[] sounds;

        private static AudioManager _instance;

        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake()
        {
            Initialize();
            DontDestroyOnLoad(this);
            CreateAudioSources();
        }

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            Play("Menu");
        }

        /// <summary>
        /// Initialize object
        /// </summary>
        private void Initialize()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(this);
        }

        /// <summary>
        /// Create audio sources for each sound
        /// </summary>
        private void CreateAudioSources()
        {
            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
            
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
            }
        }
        
        /// <summary>
        /// Play a sound by name
        /// </summary>
        /// <param name="soundName"> Sound name </param>
        private void Play(string soundName)
        {
            var s = Array.Find(sounds, sound => sound.name.Equals(soundName));
            s?.source.Play();
        }
        
        /// <summary>
        /// Stop a sound by name
        /// </summary>
        /// <param name="soundName"> Sound name </param>
        private void Stop(string soundName)
        {
            var s = Array.Find(sounds, sound => sound.name.Equals(soundName));
            s?.source.Stop();
        }
    }
}
