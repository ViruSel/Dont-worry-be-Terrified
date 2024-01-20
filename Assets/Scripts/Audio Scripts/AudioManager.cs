using System;
using System.Collections;
using UI_Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio_Scripts
{
    // Audio managing script
    public class AudioManager : MonoBehaviour
    {
        // Variables
        public Sound[] sounds;
        private static AudioManager _instance;
        
        // Called before Start function
        private void Awake()
        {
            Initialize();
            DontDestroyOnLoad(this);
            CreateAudioSources();
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            Play("Menu");
        }
        
        // Initialize object
        private void Initialize()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(this);
        }
        
        // Create audio sources for each sound
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
        
        // Play a sound by name
        private void Play(string soundName)
        {
            var s = Array.Find(sounds, sound => sound.name.Equals(soundName));
            s?.source.Play();
        }
        
        // Stop a sound by name
        private void Stop(string soundName)
        {
            var s = Array.Find(sounds, sound => sound.name.Equals(soundName));
            s?.source.Stop();
        }
    }
}
