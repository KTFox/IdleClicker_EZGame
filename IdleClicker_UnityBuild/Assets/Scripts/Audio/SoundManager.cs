using IdleClicker.Audio;
using UnityEngine;

namespace IdleClicker
{
    public class SoundManager : MonoBehaviour
    {
        // Variables

        private const float VOLUME = 1f;

        public static SoundManager Instance { get; private set; }

        [SerializeField] private AudioClipRefsSO audioClipRefs;


        // Methods

        private void Awake()
        {
            Instance = this;
        }

        public void PlayTrainingSound()
        {
            AudioSource.PlayClipAtPoint(audioClipRefs.Training, Vector2.zero, VOLUME);
        }

        public void PlayExchangeMoneySound()
        {
            AudioSource.PlayClipAtPoint(audioClipRefs.ExchangeMoney, Vector2.zero, VOLUME);
        }

        public void PlayOpenTabSound()
        {
            AudioSource.PlayClipAtPoint(audioClipRefs.OpenTab, Vector2.zero, VOLUME);
        }

        public void PlayCloseTabSound()
        {
            AudioSource.PlayClipAtPoint(audioClipRefs.CloseTab, Vector2.zero, VOLUME);
        }

        public void PlayUpgradeSound()
        {
            AudioSource.PlayClipAtPoint(audioClipRefs.Upgrade, Vector2.zero, VOLUME);
        }

        public void PlaySelectTrainingToolSound()
        {
            AudioSource.PlayClipAtPoint(audioClipRefs.SelectTrainingTool, Vector2.zero, VOLUME);
        }

        public void PlayEquipTrainingToolSound()
        {
            AudioSource.PlayClipAtPoint(audioClipRefs.EquipTrainingTool, Vector2.zero, VOLUME);
        }

        public void PlayWinSound()
        {
            AudioSource.PlayClipAtPoint(audioClipRefs.Win, Vector2.zero, VOLUME);
        }

        public void PlayLoseSound()
        {
            AudioSource.PlayClipAtPoint(audioClipRefs.Lose, Vector2.zero, VOLUME);
        }
    }
}
