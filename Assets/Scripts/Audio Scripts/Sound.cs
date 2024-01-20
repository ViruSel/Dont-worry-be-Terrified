using UnityEngine;

namespace Audio_Scripts
{
    // Custom sound object for the Audio Manager script
    [System.Serializable]
    public class Sound
    {
        // Variables
        public string name;
        public AudioClip clip;

        public bool loop;
        [Range(0f, 1f)] public float volume;
        [Range(.1f, 3f)] public float pitch;

        [HideInInspector] public AudioSource source;
    }
}
