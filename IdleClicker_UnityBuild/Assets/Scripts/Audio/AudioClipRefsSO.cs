using UnityEngine;

namespace IdleClicker.Audio
{
    [CreateAssetMenu()]
    public class AudioClipRefsSO : ScriptableObject
    {
        // Variables

        [SerializeField] private AudioClip exchangeMoney;
        [SerializeField] private AudioClip training;
        [SerializeField] private AudioClip upgrade;
        [SerializeField] private AudioClip openTab;
        [SerializeField] private AudioClip closeTab;
        [SerializeField] private AudioClip selectTrainingTool;
        [SerializeField] private AudioClip equipTrainingTool;
        [SerializeField] private AudioClip win;
        [SerializeField] private AudioClip lose;

        // Properties

        public AudioClip ExchangeMoney => exchangeMoney;
        public AudioClip Training => training;
        public AudioClip Upgrade => upgrade;
        public AudioClip OpenTab => openTab;
        public AudioClip CloseTab => closeTab;
        public AudioClip SelectTrainingTool => selectTrainingTool;
        public AudioClip EquipTrainingTool => equipTrainingTool;
        public AudioClip Win => win;
        public AudioClip Lose => lose;

    }
}
