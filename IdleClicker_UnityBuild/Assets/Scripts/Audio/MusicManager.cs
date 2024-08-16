using UnityEngine;

namespace IdleClicker
{
    public class MusicManager : MonoBehaviour
    {
        // Variables

        private AudioSource musicSource;

        // Properties
        // Methods

        private void Awake()
        {
            musicSource = GetComponent<AudioSource>();
        }

        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }
    }
}
