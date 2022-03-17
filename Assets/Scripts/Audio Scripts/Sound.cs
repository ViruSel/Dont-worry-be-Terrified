using UnityEngine;

namespace Audio_Scripts
{
    /// <summary>
    /// Custom sound object for the Audio Manager script
    /// </summary>
    [System.Serializable]
    public class Sound
    {
        /// <summary>
        /// Variables
        /// </summary>
        public string name;
    
        public AudioClip clip;

        public bool loop;
        [Range(0f, 1f)] public float volume;
        [Range(.1f, 3f)] public float pitch;

        [HideInInspector] public AudioSource source;
    }
}
